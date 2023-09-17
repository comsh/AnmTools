using System;
using System.Collections.Generic;

namespace cAnmSlice {
class Program {
    const string usage="使い方: cAnmSlice 開始時刻 終了時刻 ループ戻り時間 入力anmファイル名 [出力anmファイル名]";
    private static int Usage(){ Console.WriteLine(usage); return 0; }
    private static int NG(string msg){ Console.WriteLine(msg); return -1; }

    static int Main(string[] args) {
        if(args.Length==1){
            var af=AnmCommon.AnmFile.fromFile(args[0]);
            if(af==null) return NG("ファイルの読み込みに失敗しました");
			SortedSet<int> ts = af.getTimeSet();
            foreach(float t in ts) Console.WriteLine(t.ToString());
            return 0;
        }else if(args.Length==4||args.Length==5){
            int stime,etime,looptime;
            if(!int.TryParse(args[0],out stime)||stime<0) return NG("開始時刻が不正です");
            if(!int.TryParse(args[1],out etime)||etime<stime) return NG("終了時刻が不正です");
            if(!int.TryParse(args[2],out looptime)||looptime<0) return NG("ループ戻り時間が不正です");

            var af=AnmCommon.AnmFile.fromFile(args[3]);
            if(af==null) return NG("ファイルの読み込みに失敗しました");
            string ofile=(args.Length==5)?args[4]:args[3];
            if(AnmSlice.AnmSlice.Slice(ofile,af,stime,etime,looptime)<0) return NG("ファイルの書き込みに失敗しました");
            return 0;
        }else return Usage();

    }
}
}
