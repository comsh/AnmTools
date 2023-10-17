using AnmCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace cAnmFromLog {
public static class Import {
    private struct Node {
        public string name;
        public int parent;
        public Node(string s,int i){name=s; parent=i;}
    }
    private static Node[] bonenodes={
        new Node("Bip01",-1),
        new Node("BPelvis",0),  // 1
        new Node("BSpine",0),
        new Node("BSpine0a",2),
        new Node("BSpine1",3),
        new Node("BSpine1a",4), // 5
        new Node("BNeck",5),
        new Node("BHead",6),
        new Node("Mune_L",5),
        new Node("Mune_R",5),
        new Node("BRClavicle",5),
        new Node("BRUpperArm",10),
        new Node("BRForearm",11),
        new Node("BRHand",12),  // 13
        new Node("BRFinger0",13),
        new Node("BRFinger01",14),
        new Node("BRFinger02",15),
        new Node("BRFinger1",13),
        new Node("BRFinger11",17),
        new Node("BRFinger12",18),
        new Node("BRFinger2",13),
        new Node("BRFinger21",20),
        new Node("BRFinger22",21),
        new Node("BRFinger3",13),
        new Node("BRFinger31",23),
        new Node("BRFinger32",24),
        new Node("BRFinger4",13),
        new Node("BRFinger41",26),
        new Node("BRFinger42",27),
        new Node("BLClavicle",5),
        new Node("BLUpperArm",29),
        new Node("BLForearm",30),
        new Node("BLHand",31),
        new Node("BLFinger0",32),
        new Node("BLFinger01",33),
        new Node("BLFinger02",34),
        new Node("BLFinger1",32),
        new Node("BLFinger11",36),
        new Node("BLFinger12",37),
        new Node("BLFinger2",32),
        new Node("BLFinger21",39),
        new Node("BLFinger22",40),
        new Node("BLFinger3",32),
        new Node("BLFinger31",42),
        new Node("BLFinger32",43),
        new Node("BLFinger4",32),
        new Node("BLFinger41",45),
        new Node("BLFinger42",46),
        new Node("BRThigh",1),      // 48
        new Node("BRCalf",48),
        new Node("BRFoot",49),
        new Node("BRToe0",50),
        new Node("BRToe01",51),
        new Node("BRToe1",50),
        new Node("BRToe11",53),
        new Node("BRToe2",50),
        new Node("BRToe21",55),
        new Node("BLThigh",1),      // 57
        new Node("BLCalf",57),
        new Node("BLFoot",58),
        new Node("BLToe0",59),
        new Node("BLToe01",60),
        new Node("BLToe1",59),
        new Node("BLToe11",62),
        new Node("BLToe2",59),
        new Node("BLToe21",64),
        new Node("chinkoCenter",1), // 66
        new Node("chinko1",66),
        new Node("chinko2",67)
    };

    public static string[] GetBoneNameList(bool manq){
        string[] bones=new string[bonenodes.Length];
        for(int i=1; i<bonenodes.Length; i++) bones[i]=CompleteBoneName(bonenodes[i].name,manq);
        if(manq){
            bones[0]="ManBip";
            bones[5]="ManBip Spine2";
            bones[8]=bones[9]=bones[61]=bones[63]=bones[64]=bones[65]=bones[3]="";
        }else{
            bones[0]="Bip01";
            bones[66]=bones[67]=bones[68]="";
        }
        for(int i=0; i<bonenodes.Length; i++){
            if(bones[i]=="") continue;
            int pi=bonenodes[i].parent;
            if(pi<0) continue;
            while(pi>=0 && bones[pi]==""){ pi=bonenodes[pi].parent;}
            if(pi<0) continue;
            bones[i]=bones[pi]+"/"+bones[i];
        }
        return bones;
    }
    private static string CompleteBoneName(string shortname,bool manq, bool nayose=true){
        if(shortname.Length>1 && char.IsUpper(shortname[1])){
            string root,lr="",uname;
            int n=1;
            if(shortname[0]!='B') return shortname;
            root=manq?"ManBip":"Bip01";
            if(shortname[1]=='L'){ lr=" L"; n++; }
            else if(shortname[1]=='R'){ lr=" R"; n++; }
            uname=shortname.Substring(n);
            if(manq && nayose){
                if(uname=="Spine0a") uname="Spine1";        // Spine0aはないのでSpine1に名寄せ
                else if(uname=="Spine1a") uname="Spine2";   // Spine1aはSpine2に該当
                else if(uname.StartsWith("Toe",StringComparison.Ordinal)){
                    if(uname[3]=='2') uname="Toe1";         // Toe2はないのでToe1に名寄せ
                    else if(uname.Length==5) uname=uname.Substring(0,4); // Toe\d\dはないのでToe\dに名寄せ
                }
            }
            return $"{root}{lr} {uname}";
        }else return shortname;
    }

    public static string error;
    private static AnmFile LoadErr(string msg){ error=msg; return null; }
    public static AnmFile Load(string ifile){
        try{
            using(StreamReader sr=new StreamReader(ifile, System.Text.Encoding.UTF8)){
                string line=sr.ReadLine();
                if(line.Length!=1||(line[0]!='f' && line[0]!='m')) return LoadErr("ファイル形式が不正です");
                char mf=line[0];

                List<AnmBoneEntry> lb=new List<AnmBoneEntry>(); // 余計なものも作るので一旦Listに
                string[] bnl=GetBoneNameList(mf=='m');
                var ab=new AnmBoneEntry(bnl[0]){
                    new AnmFrameList(100),new AnmFrameList(101),new AnmFrameList(102),new AnmFrameList(103),
                    new AnmFrameList(104),new AnmFrameList(105),new AnmFrameList(106)
                };
                lb.Add(ab);
                for(int i=1; i<bnl.Length; i++){
                    ab=new AnmBoneEntry(bnl[i]){
                        new AnmFrameList(100),new AnmFrameList(101),new AnmFrameList(102),new AnmFrameList(103)
                    };
                    lb.Add(ab);
                }

                int lno=2;
                float t=0;
                while(sr.Peek()>-1){
                    line=sr.ReadLine();
                    string[] sa=line.Split(' ');
                    if(sa.Length<3) return LoadErr("ファイル形式が不正です");
                    if(!float.TryParse(sa[0],out t)) return LoadErr($"{lno}行: 時刻がありません");
                    t/=1000;

                    // Bip01 回転
                    string[] sa2=sa[2].Split(',');
                    if(sa2.Length!=4) return LoadErr($"{lno}行2列: 四元数が不正です");
                    for(int j=0; j<4; j++){
                        if(!float.TryParse(sa2[j],out float f)) return LoadErr($"{lno}行2列: 四元数が不正です");
                        var fl=lb[0][j];
                        fl.Add(new AnmFrame(t){value=f});
                    }
                    // Bip01 移動
                    sa2=sa[1].Split(',');
                    if(sa2.Length!=3) return LoadErr($"{lno}行1列: 座標が不正です");
                    for(int j=0; j<3; j++){
                        if(!float.TryParse(sa2[j],out float f)) return LoadErr($"{lno}行1列: 座標が不正です");
                        var fl=lb[0][4+j];
                        fl.Add(new AnmFrame(t){value=f});
                    }

                    // Bip01以外
                    for(int i=3; i<sa.Length; i++){
                        if(bnl[i-2]=="") continue;
                        if(sa[i]=="") continue;
                        sa2=sa[i].Split(',');
                        if(sa2.Length!=4) return LoadErr($"{lno}行{i}列: 四元数が不正です");
                        for(int j=0; j<4; j++){
                            if(!float.TryParse(sa2[j],out float f)) return LoadErr($"{lno}行{i}列: 四元数が不正です");
                            var fl=lb[i-2][j];
                            fl.Add(new AnmFrame(t){value=f});
                        }
                    }
                    lno++;
                }

                foreach(var be in lb) foreach(var fl in be) AnmFrameList.Linear(fl);

                AnmFile af=new AnmFile();
                af.muneLR=new byte[]{ MuneRotq(lb,8), MuneRotq(lb,9)};
                for(int i=0; i<bnl.Length; i++) if(bnl[i]!="") af.Add(lb[i]);
                return af;
            }
        }catch{
            return LoadErr("ファイルの読み込みに失敗しました");
        }
    }
    private static byte MuneRotq(List<AnmBoneEntry> af,int no){
        foreach(var fl in af[no]) if(fl.Count>=2){
            int i; for(i=1; i<fl.Count; i++) if(fl[i].value!=fl[i-1].value) break;
            if(i<fl.Count) return 1;
        }
        return 0;
    }
    private static bool fcmp(float a,float b){
        float d=a-b;
        return (-0.000001<d && d<0.000001);
    }
}}
