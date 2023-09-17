using System;
using System.Collections.Generic;
using AnmCommon;

namespace AnmReduce {
public static class AnmReduce {

    public static void Reduce(AnmFile af,int mode){
        foreach(var b in af){
            for(int fli=0; fli<b.Count; fli++){
                var fl=b[fli];
                if(fl.Count==0) continue;
                b[fli]=ReduceFrameList(fl,mode);
            }
        }
    }
    public static AnmFrameList ReduceFrameList(AnmFrameList fl,int mode=1){
        var fl2=new AnmFrameList(fl.type);
        int i,cur;
        for(cur=0; cur<fl.Count; cur++){
            fl2.Add(fl[cur]);
            for(i=cur+1; i<fl.Count; i++) if(!fcmp(fl[cur].tan2,fl[i].tan2)) break;
            i--;
            if(i>cur){ fl2.Add(fl[i]); cur=i;}
        }
        AnmFrameList.CatmullRom(fl2);
        if(mode==0) return fl2;

        var p=new List<int>();
        p.Add(0);
        bool incq=(fl2[1].tan2-fl2[0].tan2>=0);
        for(i=2; i<fl2.Count; i++){
            bool q=(fl2[i].tan2-fl2[i-1].tan2>=0);
            if(q!=incq){incq=q; p.Add(i);}
        }
        p.Add(fl2.Count);

        var fl3=new AnmFrameList(fl2.type);
        for(int pi=1; pi<p.Count; pi++){
            for(cur=p[pi-1]; cur<p[pi]; cur++){
                fl3.Add(fl2[cur]);
                for(i=cur+1; i<p[pi]; i++){
                    float otan=fl2[cur].tan2,itan=fl2[i].tan1;
                    double c=(1+otan*itan)/Math.Sqrt((1+otan*otan)*(1+itan*itan));
                    if(c<0.99) break;  // 0.99=cos(約8度)
                }
                i--;
                if(i>cur){fl3.Add(fl2[i]); cur=i;}
            }
        }
        return fl3;
    }  
    private static bool fcmp(float a,float b){
        float d=a-b;
        return (-0.000001<d && d<0.000001);
    }
}
}
