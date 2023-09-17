using System;
using AnmCommon;

namespace cAnmCnv {
class Program {
    const string usage= "使い方: cAnmCnv 入力anmファイル名 /O 出力ファイル名 /F /M /T /R /S 変更後の長さ /D ディレイ";
    private static int Usage(){ Console.WriteLine(usage); return 0; }
    private static int NG(string msg){ Console.WriteLine(msg); return -1; }

    static int Main(string[] args) {
        if(args.Length<=1) return Usage();

        string ifile="",ofile="";
        char opt=' ';
        int gender=-1,maxtime=0,delay=0,mirror=0;
        for(int i=0; i<args.Length; i++){
            if(args[i].Length==0) continue;
            if(args[i][0]=='/'){
                if(args[i].Length==1) continue;
                opt=args[i][1];
                switch(opt){
                case 'F':
                    gender=0;
                    break;
                case 'M':
                    gender=1;
                    break;
                case 'T':
                    gender=2;
                    break;
                case 'R':
                    mirror=1;
                    break;
                case 'S':
                case 'D':
                case 'O':
                    break;
                default:
                    return NG("無効なオプションです");
                }
            }else{
                switch(opt){
                case 'S':
                    if(!int.TryParse(args[i],out maxtime) || maxtime<=0) return NG("長さの指定が不正です");
                    break;
                case 'D':
                    if(!int.TryParse(args[i],out delay) || delay<=0) return NG("ディレイの指定が不正です");
                    break;
                case 'O':
                    ofile=args[i];
                    break;
                default:
                    if(ifile=="") ifile=args[i]; else return NG("入力ファイルの指定が複数あります");
                    break;
                }
            }
        }
        if(ofile=="") ofile=ifile;
        var af=AnmFile.fromFile(ifile);
        if(af==null) return NG("ファイルの読み込みに失敗しました");
        AnmCnv.AnmCnv.Conv(af,gender,maxtime,delay,mirror==1);
        af.write(ofile);
        return 0;
    }
}
}
