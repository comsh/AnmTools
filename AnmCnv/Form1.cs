using AnmCommon;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AnmCnv {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private AnmFile af = null;      // 読み込んだanmファイル

        // 変換実行
        private void btnCnv_Click(object sender, EventArgs e) {
            if (txtInput.Text=="") return;

            // バリデーション
            int newMaxTime=0,delay=0;
            if (chkSpeed.Checked) {
                if(!int.TryParse(txtSpeed.Text,out newMaxTime)||newMaxTime<=0){
                    MessageBox.Show("最終フレーム時刻は正の整数で指定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (chkDelay.Checked) {
                if(!int.TryParse(txtDelay.Text,out delay)||delay<=0){
                    MessageBox.Show("遅延時間は正の整数で指定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string outfilename = outFileDialog();
            if (outfilename==null) return;

            AnmFile afw = new AnmFile(af);     // 変更＆書き出し用コピー

            // 性転換
            if (chkGender.Checked) ChgGender(afw);

            // モーション速度変更
            if (chkSpeed.Checked) {
                SortedSet<int> ts = af.getTimeSet();
                if (ts.Max!=0 && newMaxTime!=ts.Max) {
                    float speed = (float)newMaxTime/ts.Max;
                    foreach (AnmBoneEntry bone in afw)
                        foreach (AnmFrameList fl in bone)
                            foreach (AnmFrame f in fl){
                                f.time*=speed;
                                if(speed!=0){
                                    f.tan1/=speed;
                                    f.tan2/=speed;
                                }
                            }
                }
            }

            // モーション開始遅延(AnmJoinでの結合用)
            if (chkDelay.Checked) {
                foreach (AnmBoneEntry bone in afw)
                    foreach (AnmFrameList fl in bone)
                        foreach (AnmFrame f in fl) f.time=(f.time*1000+delay)/1000;
            }

            if(chkMirror.Checked){
                foreach (AnmBoneEntry bone in afw){
                    bone.inOrder();
                    bone.rename(mirrorBoneName(bone.boneName));
                    if(bone.boneId==0){ // ルート直下
                        if(bone.boneName=="Bip01"||bone.boneName=="ManBip")
                            negFrameList2(bone,AXIS.Y,AXIS.X);
                        else negFrameList(bone,AXIS.X);
                    }else if(bone.boneId==1) negFrameList2(bone,AXIS.Z); // 骨盤
                    else if(bone.boneId==10) negFrameList2(bone,AXIS.Z); // 背骨1
                    else if(bone.boneId==16||bone.boneId==22) negFrameListMune(bone); // 胸
                    else negFrameList(bone,AXIS.Z);
                }
            }

            // １つもチェックなくても出力しちゃう

            // 書き出し
            afw.write(outfilename);
        }
        // 左右反転の考え方
        // 指定した軸方向(その部位で左右にあたる方向)について
        //   移動: 移動量 * -1
        //   回転: 中心軸の指定軸方向成分 * -1、および回転角 * -1
        private bool[] negarr(AXIS ax,AXIS axm=AXIS.N){
            bool[] neg=new bool[7];

            neg[3]=true;
            if(ax==AXIS.X) neg[0]=true; else if(ax==AXIS.Y) neg[1]=true; else neg[2]=true;

            AXIS ax2=(axm==AXIS.N)?ax:axm;
            if(ax2==AXIS.X) neg[4]=true; else if(ax2==AXIS.Y) neg[5]=true; else neg[6]=true;

            return neg;
        }
        enum AXIS{ X,Y,Z,N };
        private void negFrameList(AnmBoneEntry abe,AXIS ax){
            bool[] neg=negarr(ax);
            foreach (AnmFrameList fl in abe){
                if(neg[fl.type-100]) foreach (AnmFrame f in fl){
                    f.value=-f.value;
                    f.tan1=-f.tan1;
                    f.tan2=-f.tan2;
                }
            }
        }
        // 胸用
        private void negFrameListMune(AnmBoneEntry abe){
            // 胸は左右のボーンがローカルx軸方向に180度ねじれた関係
            // (上腕を前に出した姿勢におけるClavicle＋UpperArmの回転をMune_L/R１つでやる感じ?)
            // なので右から{1,0,0,0}をかけるが、数式上結果は{w,z,-y,-x}になる
            // モーションを左右反転(親z軸方向)するためにzとwを-1倍するので
            // {-w,-z,-y,-x}が答え
            foreach (AnmFrameList fl in abe){
                if(fl.type<104) fl.type=(byte)(203-fl.type); // 入れ替え
                if(fl.type<104||fl.type==106) foreach (AnmFrame f in fl){
                    f.value=-f.value;
                    f.tan1=-f.tan1;
                    f.tan2=-f.tan2;
                }
            }
            abe.inOrder(); // fl.typeを付け替えたのでソート
        }
        // 組み立て変換込みの反転
        // 組み立て変換をQ、ポーズ正味のポーズ部分をPとすると、Qはそのまま、Pのみ左右反転する。
        // anmファイル上のデータはQPの値。左から~QをかけてPを取り出し(~Q(QP)=(~QQ)P=P)、
        // Pの左右を反転した後、左からQをかけてQPに戻す
        private void negFrameList2(AnmBoneEntry abe,AXIS ax,AXIS axm=AXIS.N){
            bool[] neg=negarr(ax,axm);
            List<float[]> ql=new List<float[]>();   // 四元数
            // これはさすがに、回転は一度四元数の形にまとめないと処理できない
            // 移動は普通に済ませる
            foreach (AnmFrameList fl in abe){
                if(fl.type<104){
                    for(int i=0; i<fl.Count; i++){
                        if(ql.Count==i) ql.Add(new float[4]);
                        ql[i][fl.type-100]=fl[i].value;
                    }
                }else if(neg[fl.type-100]) foreach (AnmFrame f in fl){
                    f.value=-f.value;f.tan1=-f.tan1;f.tan2=-f.tan2;
                }
            }
            foreach(float[] q in ql){
                Quaternion.mul(q,rKumitate,q);
                // 正味部分(P)を左右反転
                if(neg[0]) q[0]*=-1;
                if(neg[1]) q[1]*=-1;
                if(neg[2]) q[2]*=-1;
                if(neg[3]) q[3]*=-1;
                // 組み立て変換を戻す
                Quaternion.mul(q,kumitate,q);
            }
            foreach (AnmFrameList fl in abe){
                if(fl.type>=104) continue;
                int ty=fl.type-100;
                var old=new float[fl.Count];
                for(int i=0; i<fl.Count; i++){ // 値のセット
                    old[i]=fl[i].value;
                    fl[i].value=ql[i][ty];
                }
                
                int last=fl.Count-1;
                float d;
                if(!isZero(d=old[1]-old[0])) fl[0].tan2*=(ql[1][ty]-ql[0][ty])/d;
                if(!isZero(d=old[last]-old[last-1])) fl[last].tan1*=(ql[last][ty]-ql[last-1][ty])/d;
                fl[0].tan1=fl[0].tan2;
                fl[last].tan2=fl[last].tan1;

                for(int i=1; i<fl.Count-1; i++){ // 補間値
                    if(fl[i].tan1!=fl[i].tan2){     // 線形補間など
                        // 補間値を値の差の比例計算で修正
                        if(!isZero(d=old[i]-old[i-1])) fl[i].tan1*=(ql[i][ty]-ql[i-1][ty])/d;
                        if(!isZero(d=old[i+1]-old[i])) fl[i].tan2*=(ql[i+1][ty]-ql[i][ty])/d;
                    }else{  // 曲線補間
                        fl[i].tan1=fl[i].tan2=(fl[i+1].value-fl[i-1].value)/(fl[i+1].time-fl[i-1].time);
                    }
                }
            }
        }
        private bool isZero(float f){ return f>-0.00000001f &&f<0.00000001f; }
        private static float[] kumitate={0.5f,-0.5f,-0.5f,-0.5f};    // Bip01
        private static float[] rKumitate={-0.5f,0.5f,0.5f,-0.5f};
        private string mirrorBoneName(string bname){
            string t=Regex.Replace(bname,@"L\b","R"); // ほとんどこれで拾えるけど
            if(t==bname){
                t=Regex.Replace(bname,@"R\b","L");
                return t.Replace("_R_","_L_");        // これだけ例外。Mune_L_subみたいなパターン
                // Mune_L_sub型は同じ行にMune_Lがあって\bのヤツに引っかかってるはずだから
                // LかRかの判断をやり直す必要はない
            }else{
                return t.Replace("_L_","_R_");
            }
        }

        // UI連動
        private void chkSpeed_CheckedChanged(object sender, EventArgs e) {
            txtSpeed.Enabled = chkSpeed.Checked;
        }
        private void chkDelay_CheckedChanged(object sender, EventArgs e) {
            txtDelay.Enabled = chkDelay.Checked;
        }

        // ファイル選択
        private void btnInput_Click(object sender, EventArgs e) {
            string fname = fileDialog();
            if (fname != "") handleInputFileSelected(fname);
        }

        // D&D
        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            handleInputFileSelected(files[0]);
        }
        private void Form1_DragEnter(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            bool acceptq = true;
            for (int i = 0; i < files.Length; i++) {
                if (!files[i].EndsWith(".anm")) {
                    acceptq = false;
                    break;
                }
            }
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) acceptq = false;
            e.Effect = acceptq ? DragDropEffects.All : DragDropEffects.None;
        }

        // 下請け
        private void handleInputFileSelected(string fname) {
            if (!fname.EndsWith(".anm")) return;
            txtInput.Text = fname;
            lastPath=System.IO.Path.GetDirectoryName(fname);

            af=AnmFile.fromFile(fname);
            if (af==null) return;
            SortedSet<int> ts = af.getTimeSet();

            int gi=af.getGender();
            string gender = "性別不明";
            chkGender.Enabled = true;
            if(gi==0) gender="女性用";
            else if(gi==1) gender="男性用";
            else chkGender.Enabled = false;

            lblAnmInfo.Text = $"{gender}   時間範囲: {ts.Min}ms - {ts.Max}ms";

            txtSpeed.Text=ts.Max.ToString();
            txtDelay.Text="0";
            chkGender.Checked = false;
            chkSpeed.Checked = false;
            chkDelay.Checked = false;
        }
        private string lastPath="";
        private string fileDialog() {
            string fname = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "入力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            if(lastPath!="") dialog.InitialDirectory=lastPath;
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }
        private string outFileDialog() {
            string fname = null;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = System.IO.Path.GetFileNameWithoutExtension(txtInput.Text)+"_modified.anm";
            dialog.Title = "出力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            dialog.InitialDirectory=System.IO.Path.GetDirectoryName(txtInput.Text);
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }

        // 性転換
        private void ChgGender(AnmFile afw){
            string[][] f2m = {      // Spineの構成が男女で違う
                new string[]{"Bip01 Spine0a/Bip01 Spine1","ManBip Spine1"},
                new string[]{"Spine1a","Spine2"},
                new string[]{"Bip01","ManBip"},
            };
            int fm = af.getGender();  // 男性->女性なら逆順
            if(fm==-1) return;        // 性別不明

            // ボーン名変更
            int insPos = -1;
            bool has0a=false;
            SortedSet<int> ts = af.getTimeSet();
            for (int bi=0; bi<afw.Count; bi++){
                AnmBoneEntry bone = afw[bi];
                string name = bone.boneName;
                foreach(string[] rep in f2m) name=name.Replace(rep[fm],rep[fm^1]);

                // 女性->男性->女性とやっている場合は男性にSpine0aがあるかもなのであったらそのまま使う
                if (fm==1 && name.EndsWith("Spine0a") && bone.Count>0 && bone[0].type==100) has0a=true;

                // 男性->女性のとき、有効なSpine1の直前にSpine0aを作る。今は挿入位置だけ覚えておく
                if (fm==1 && has0a==false && name.EndsWith("Spine1") && bone.Count>0 && bone[0].type==100) insPos=bi;
                bone.rename(name);
            }
            if (insPos>=0) {    // Spine1の直前にSpine0aを作って挿入
                AnmBoneEntry be = new AnmBoneEntry("Bip01/Bip01 Spine/Bip01 Spine0a");
                AnmFrameList fl;
                for (int i = 100; i<103; i++) {
                    fl = new AnmFrameList((byte)i);
                    fl.Add(new AnmFrame(0));
                    fl.Add(new AnmFrame((float)ts.Max/1000));
                    be.Add(fl);
                }
                // qwはデフォルト1.0
                fl = new AnmFrameList((byte)103);
                fl.Add(new AnmFrame(0){value=1});
                fl.Add(new AnmFrame((float)ts.Max/1000){value=1});
                be.Add(fl);
                afw.Insert(insPos, be);
            }
        }
    }
}
