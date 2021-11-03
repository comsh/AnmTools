using AnmCommon;
using System.IO;
using System.Text.RegularExpressions;

namespace AnmDmpCommon {
    public static class DmpPmd {
        public static string error="";

        // anmファイル→テキスト
        public static int Dmp(string fname, StreamWriter tw){
            AnmFile af =AnmFile.fromFile(fname);
            if (af==null){ error="ファイル読込みに失敗しました"; return -1;}

            tw.Write("Filename:"); tw.WriteLine(fname);
            tw.Write("Format:"); tw.Write(af.format);
            if(af.format==1001){
                tw.Write("  MuneL有効:"); tw.Write(af.muneLR[0]==0?"x":"o");
                tw.Write("  MuneR有効:"); tw.Write(af.muneLR[1]==0?"x":"o");
            }
            tw.WriteLine("");
            foreach (AnmBoneEntry bone in af){
                tw.Write("["); tw.Write(bone.boneName); tw.WriteLine("]");

                bone.inOrder();
                var fla=new AnmFrameList[7];
                var flia=new int[7];
                foreach (AnmFrameList fl in bone) fla[fl.type-100]=fl;
                for(int i=0; i<7; i++) if(fla[i]==null||fla[i].Count==0) flia[i]=-1; else flia[i]=0;
                int lastt=-1;
                float mint=0;
                int mini;
                while(true){    // マージソートの要領で時間順に出力
                    for(mini=0; mini<7; mini++) if(flia[mini]>=0){
                        mint=fla[mini][flia[mini]].time;
                        break;
                    }
                    if(mini==7) break;
                    for(int i=mini+1; i<7; i++) if(flia[i]>=0){
                        float t=fla[i][flia[i]].time;
                        if(mint>t){mint=t; mini=i;}
                    }
                    AnmFrame f=fla[mini][flia[mini]];
                    int ms=(int)(f.time*1000);
                    if (ms!=lastt) {
                        tw.Write(ms.ToString("00000000")); 
                        lastt=ms;
                    } else {
                        tw.Write("        ");
                    }
                    tw.Write("    ");
                    tw.Write(types[mini]);
                    tw.Write(f.value.ToString("F9").PadLeft(16));
                    tw.Write(f.tan1.ToString("F9").PadLeft(16));
                    tw.WriteLine(f.tan2.ToString("F9").PadLeft(16));
                    if(flia[mini]==fla[mini].Count-1) flia[mini]=-1; else flia[mini]++;
                }
            }
            return 0;
        }

        private static readonly string[] types={"qx","qy","qz","qw"," x"," y"," z"};
        private static Regex reg1 = new Regex(
            @"Filename:.+?\r?\nFormat:(\d+)(?:\s+MuneL有効:([xo])\s+MuneR有効:([xo]))?\r?\n",RegexOptions.Compiled|RegexOptions.Singleline);
        private static Regex reg2 = new Regex(
            @"\G\[(?<bone>.+?)\]\r?\n(?:(?<time>\d{8}|\s{8})\s+(?<type>"+string.Join("|",types)+@")(?:\s+(?<val>[-\d\.]+)){3}\r?\n)*",
            RegexOptions.Compiled|RegexOptions.Singleline);
        private static int type2int(string type){
            for(int i=0; i<types.Length; i++) if(type==types[i]) return 100+i;
            return -1;
        }
        // テキスト→anmファイル
        public static int Pmd(string text, string filename){
            Match m = reg1.Match(text);
            if (!m.Success){ error="テキストファイルの書式が不正です"; return -1;}

            AnmFile af = new AnmFile();
            af.format = int.Parse(m.Groups[1].Value);
            af.muneLR[0]=(byte)((m.Groups[2].Success && m.Groups[2].Value=="o")?1:0);
            af.muneLR[1]=(byte)((m.Groups[3].Success && m.Groups[3].Value=="o")?1:0);

            m = reg2.Match(text,m.Groups[0].Value.Length);
            while (m.Success) {
                AnmBoneEntry bone = new AnmBoneEntry(m.Groups["bone"].Value);
                af.Add(bone);

                AnmFrameList[] fla = {
                    new AnmFrameList(100), new AnmFrameList(101), new AnmFrameList(102),new AnmFrameList(103),
                    new AnmFrameList(104), new AnmFrameList(105), new AnmFrameList(106)
                };
                int curTime = 0;
                for(int i=0; i<m.Groups["time"].Captures.Count; i++) {
                    AnmFrame f =new AnmFrame();
                    string tstr = m.Groups["time"].Captures[i].Value.Trim();
                    if(tstr!="") curTime=int.Parse(tstr);
                    f.time= curTime/1000f;
                    f.value=float.Parse(m.Groups["val"].Captures[i*3].Value);
                    f.tan1=float.Parse(m.Groups["val"].Captures[i*3+1].Value);
                    f.tan2=float.Parse(m.Groups["val"].Captures[i*3+2].Value);

                    int type = type2int(m.Groups["type"].Captures[i].Value);
                    fla[type-100].Add(f);
                }
                foreach (AnmFrameList fl in fla) if (fl.Count>0) bone.Add(fl);
                m=m.NextMatch();
            }
            if(!af.write(filename)){ error="anmファイルの書き出しに失敗しました"; return -1;}
            return 0;
        }
    }
}
