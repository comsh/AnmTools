using System;
using System.Collections.Generic;
using System.IO;

namespace AnmCommon {
	public class AnmFile : List<AnmBoneEntry> {
		public int format=0;
		public byte[] muneLR={0,0};   // format=1001の場合のみ。2000ではneiに持ってる

		public AnmFile() {}
		public AnmFile(AnmFile af) {
			format=af.format;
            muneLR[0]=af.muneLR[0];
            muneLR[1]=af.muneLR[1];
			foreach (AnmBoneEntry bone in af) Add(new AnmBoneEntry(bone)); // ディープコピー
        }
		public AnmFile(string filename) { read(filename); }   // 例外飛ぶよ

		// 例外キャッチするstatic版。細かい例外処理をしないならこちらでも
		public static AnmFile fromFile(string filename) {
			AnmFile ret = null;
			try { ret=new AnmFile(filename); } catch { }
			return ret;
		}

		// anmファイル読み込み。例外は呼び出し元に任せる
		private void read(string filename) {
			using (BinaryReader r = new BinaryReader(File.OpenRead(filename))) {
				byte[] buf = r.ReadBytes(15);
				format=BitConverter.ToInt32(buf, 11);
				while (r.Read()==1) Add(new AnmBoneEntry(r));
                if(format==1001){
                    if(r.PeekChar()>=0) muneLR[0]= r.ReadByte();
                    if(r.PeekChar()>=0) muneLR[1]= r.ReadByte();
                }
			}
		}

		// anmファイルヘッダ  Encoding.ASCII.GetBytes("\x0aCM3D2_ANIM")
		private static readonly byte[] anmHeader = { 0x0a, 0x43, 0x4D, 0x33, 0x44, 0x32, 0x5f, 0x41, 0x4e, 0x49, 0x4d };

		// anmファイル書き出し。ボーン単位でのフィルタリング付き
		public bool write(String filename, bool[] wblist=null, bool white=false) {
            byte[] hasMnue={0,0};
			try {
				using (BinaryWriter w = new BinaryWriter(File.Create(filename))) {
					w.Write(anmHeader);             // ヘッダ固定文字列
					w.Write(format);                // 形式バージョン

					// 部位フィルタ機能は分割時専用だが、他のケースと共用なので同じ体裁で処理
					// 分割時以外は空のブラックリスト＝全部出力
					bool[] flt = wblist;
					if (flt==null) flt=new bool[AnmBoneName.boneGroup.Length];  // 空(全てfalse)

					foreach (AnmBoneEntry bone in this) {
                        if(bone.Count>0){
                            if(bone.boneId==16) hasMnue[0]=1;
                            if(bone.boneId==22) hasMnue[1]=1;
                        }
						bone.write(w, (flt[bone.boneId]==white));
					}
					w.Write((byte)0);
                    if(format==1001){
                        byte[] muneOut={muneLR[0],muneLR[1]}; // 基本は入力踏襲
                        if(wblist!=null){   // フィルタリング有りで
                            // 胸ボーン除外または胸ボーン無しなら強制off
                            if(flt[16]!=white || hasMnue[0]==0) muneOut[0]=0;
                            if(flt[22]!=white || hasMnue[1]==0) muneOut[1]=0;
                        }
                        w.Write(muneOut);
                    }
				}
			} catch { return false; }
			return true;
		}

		// 複数anm合成時に使う List<AnmBoneEntry> からのstatic出力関数 
		public static bool joinAnm(List<AnmFile> files, String filename) {

			bool has2000mune=false;
			// 各ボーン名ごとにまとめる
			Dictionary<string,AnmBoneEntry> bonedic = new Dictionary<string, AnmBoneEntry>();
			foreach (AnmFile file in files) {
				foreach (AnmBoneEntry bone in file) {
					if(file.format>1001 && (bone.boneId==16 || bone.boneId==22)
                        && bone.boneName.Contains("Bip01") && bone.Count>0) has2000mune=true;
					string sortkey = bone.getSortkey();
					if (bonedic.ContainsKey(sortkey)) mergeBone(bonedic[sortkey], bone);
					else {
						// 新規要素について整列済を担保。念のため
						bone.inOrder();
						bonedic.Add(sortkey, bone);
					}
				}
			}
			// ボーン単位で書き出し順にソート
			List<string> dKeys=new List<string>(bonedic.Keys);
			dKeys.Sort();

			// 書き出し用AnmFileインスタンス
			AnmFile af = new AnmFile();
			af.format=1001;
            if(has2000mune) af.muneLR[0]=af.muneLR[1]=1; // format=2000で胸モーション持ちがいたらon
            else foreach (AnmFile file in files){
                if(file.format==1001){                   // fomat=1001ならanmファイルに持っているものを使う
                    af.muneLR[0]|=file.muneLR[0];
                    af.muneLR[1]|=file.muneLR[1];
                }
            }
			foreach (string key in dKeys) af.Add(bonedic[key]);
			return af.write(filename);
		}

		private static void mergeBone(AnmBoneEntry to, AnmBoneEntry from) {
			foreach(AnmFrameList fl in from) mergeFrameList(to,fl);
		}
		private static void mergeFrameList(AnmBoneEntry bone, AnmFrameList ml) {
			for (int i = 0; i<bone.Count; i++) {
				int c = bone[i].type-ml.type;
				if (c<0) continue;
				else if (c==0) {        // 存在するのでマージ
					foreach (AnmFrame f in ml) mergeFrame(bone[i],f);
					return;
				} else if (c>0) {       // 挿入位置
					ml.inOrder();      // 新規は整列済担保
					bone.Insert(i, ml);
					return;
				}
			}
			ml.inOrder();				// 新規なら整列済担保
			bone.Add(ml);               // 最大要素。末尾に追加
		}
		private static void mergeFrame(AnmFrameList fl, AnmFrame f) {
			for (int i = 0; i<fl.Count; i++) {
				float c = fl[i].time-f.time;
				if (c<0) continue;
				else if (c==0) {        // 存在するので上書き
					fl[i]=f;
					return;
				} else if (c>0) {       // 挿入位置
					fl.Insert(i, f);
					return;
				}
			}
			fl.Add(f);                  // 最大要素。末尾に追加
		}

		// フレーム時刻の一覧を作る
		public SortedSet<int> getTimeSet() {
			SortedSet<int> rslt = new SortedSet<int>();
			foreach (AnmBoneEntry b in this)
				foreach (AnmFrameList fl in b)
					foreach (AnmFrame fr in fl)
						rslt.Add((int)(fr.time * 1000));
			return rslt;
		}
        public int getGender(){
            foreach(AnmBoneEntry abe in this){
                if(abe.boneName.StartsWith("ManBip",StringComparison.Ordinal)) return 1;
                else if(abe.boneName.StartsWith("Bip01",StringComparison.Ordinal)) return 0;
            }
            return -1;
        }
	}

	public class AnmBoneEntry : List<AnmFrameList> {
		public string boneName = "";    // ボーン名(完全)
		public int boneId = -1;         // ボーンID

		public AnmBoneEntry() { }
		public AnmBoneEntry(string name) { rename(name); }
		public AnmBoneEntry(AnmBoneEntry be) {
			boneName=be.boneName;
			boneId=be.boneId;
			foreach (AnmFrameList fl in be) Add(new AnmFrameList(fl));
		}
		public AnmBoneEntry(BinaryReader r) { read(r); }

		public void rename(string name) {
			boneName=name;
			boneId = AnmBoneName.getBoneId(boneName);
		}
		public void read(BinaryReader r, bool recursiveq = true) {
			// ボーン名は文字列長(LEB128)＋文字列という形式だが、ReadString()なら１発
			rename(r.ReadString());
			if (recursiveq) {
				int t;
				while ((t=r.PeekChar())>=0) {
					if (t==1) break;
					else if (t>=100) Add(new AnmFrameList(r));
					else break;
				}
			}
		}
		public void write(BinaryWriter w, bool recursiveq = true) {
			w.Write((byte)1);
			w.Write(boneName);	// 書き出しもWrite(string)なら１発
			if(recursiveq) foreach(AnmFrameList fl in this) fl.write(w,true);
			return;
        }

		public string getSortkey() {
			string[] part = boneName.Split('/');
			string shortName = part[part.Length-1];

			return String.Join("", new string[] {
				boneId.ToString("00"),
				part.Length.ToString("00"),     // ボーン名の階層の深さ
				shortName
			});
		}

		// フレームリストをtypeでソート
		// どのデータもソート済だとは思うけど、保証はないから必要ならやる
		public void inOrder(bool recursiveq=true) {
			Sort((a, b) => a.type-b.type);
			if(recursiveq) foreach (AnmFrameList fl in this) fl.inOrder();
		}

	}
	public class AnmFrameList : List<AnmFrame> {
		public byte type = 0;   // 100-106 4元数＋移動xyzの7種類

		public AnmFrameList() {}
		public AnmFrameList(byte type) { this.type=type; }
		public AnmFrameList(AnmFrameList fl) {
			type=fl.type;
			foreach (AnmFrame f in fl) Add(new AnmFrame(f));
		}
		public AnmFrameList(BinaryReader r) { read(r); }

		public void read(BinaryReader r, bool recursiveq = true) {
			type=r.ReadByte();
			int n = r.ReadInt32();
            if(recursiveq) for(int i=0; i<n; i++) Add(new AnmFrame(r));
		}
		public void write(BinaryWriter w, bool recursiveq = true) {
			w.Write(type);
			w.Write(Count);
			if (recursiveq) foreach(AnmFrame f in this) f.write(w);
		}

		// フレームを時間でソート
		// どのデータもソート済だとは思うけど、保証はないから必要ならやる
		public void inOrder() {
			Sort((a, b) => Math.Sign(a.time-b.time));
		}

	}
	public class AnmFrame {     // キーフレーム。UnityのKeyframe(WeightedMode.None)
		public float time = 0;      // フレーム時刻(1/1000ms)
		public float value = 0;		// パラメータ値(意味はAnmFrameListのtypeによる)
		public float tan1 = 0;		// 補間用。１つ前の値との間の３次曲線の接線
		public float tan2 = 0;      // 補間用。次の値との間の３次曲線の接線

		public AnmFrame() {}
		public AnmFrame(float time) { this.time=time; }
		public AnmFrame(AnmFrame f) {
			time=f.time;
			value=f.value;
			tan1=f.tan1;
			tan2=f.tan2;
		}
		public AnmFrame(BinaryReader r) { read(r); }

		public void read(BinaryReader r) {
			time=r.ReadSingle();
			value=r.ReadSingle();
			tan1=r.ReadSingle();
			tan2=r.ReadSingle();
		}
		public void write(BinaryWriter w) {
			w.Write(time);
			w.Write(value);
			w.Write(tan1);
			w.Write(tan2);
		}
	}

	// 全ボーン名を網羅していたら面倒なので(momotwistやらFootStepsやら何やら)
	// 必要なボーン名だけ定義。それ以外は親を辿って親と同一部位とみなす。そのための定義と処理
	public static class AnmBoneName {
		public static string[][] boneGroup ={
			new string[]{ "Bip01","ManBip","ST_Root" },		// 男性の場合ManBip
			new string[]{ "Pelvis" },

			new string[]{ "L Thigh" }, //2
			new string[]{ "L Calf" },
			new string[]{ "L Foot" },
			new string[]{ "L Toe0", "L Toe1", "L Toe2" },

			new string[]{ "R Thigh" }, //6
			new string[]{ "R Calf" },
			new string[]{ "R Foot" },
			new string[]{ "R Toe0","R Toe1","R Toe2" },

			new string[]{ "Spine" }, //10
			new string[]{ "Spine0a" },						// 男性の場合無い。というかSpineシリーズが１つ少ない
			new string[]{ "Spine1" },
			new string[]{ "Spine1a","Spine2" },				// 男性の場合Spine2
			new string[]{ "Neck" },
			new string[]{ "Head" },

			new string[]{ "Mune_L" }, //16
			new string[]{ "L Clavicle" },
			new string[]{ "L UpperArm" },
			new string[]{ "L Forearm" },
			new string[]{ "L Hand" },
			new string[]{ "L Finger0", "L Finger1", "L Finger2", "L Finger3", "L Finger4" },

			new string[]{ "Mune_R" }, //22
			new string[]{ "R Clavicle" },
			new string[]{ "R UpperArm" },
			new string[]{ "R Forearm" },
			new string[]{ "R Hand" },
			new string[]{ "R Finger0","R Finger1","R Finger2","R Finger3","R Finger4" }
		};

		// ボーン識別用のID(数値)を得る
		public static int getBoneId(string bonename) {
            string[] fragment=bonename.Split('/');
            for(int f=fragment.Length-1; f>=0; f--)
                for(int i=0; i<boneGroup.Length; i++)
                    for(int j=0; j<boneGroup[i].Length; j++)
                        if(fragment[f].EndsWith(boneGroup[i][j],StringComparison.Ordinal)) return i;
			return 0;   // 未定義で親もないということなのでルートに分類
		}
	}
}
