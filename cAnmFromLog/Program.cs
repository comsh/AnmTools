using System;
using AnmCommon;

namespace cAnmFromLog {
class Program {
    const string usage= "使い方: cAnmFromLog 入力ファイル名 出力ファイル名";
    private static int Usage(){ Console.WriteLine(usage); return 0; }
    private static int NG(string msg){ Console.WriteLine(msg); return -1; }

    static int Main(string[] args) {
        if(args.Length<2) return Usage();
        string ifile,ofile,opt;
        int mode=0;
        if(args.Length==3){
            if(args[0].Length>=2 && args[0][0]=='/'){
                opt=args[0]; ifile=args[1]; ofile=args[2];
            }else if(args[2].Length>=2 && args[2][0]=='/'){
                ifile=args[0]; ofile=args[1]; opt=args[2];
            }else return NG("引数に誤りがあります");
            if(opt=="/S") mode=1;
            else return NG("無効なオプションです");
        }else{
            ifile=args[0]; ofile=args[1];
        }
        if(ifile=="" || ofile=="") return Usage();

        AnmFile af=Import.Load(ifile);
        if(af==null) return NG(Import.error);
        AnmReduce.AnmReduce.Reduce(af,mode);
        af.write(ofile);
        return 0;
    }
}
}
