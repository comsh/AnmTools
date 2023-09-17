using System.Collections.Generic;
using AnmCommon;

namespace AnmSlice {
public static class AnmSlice {
    public static int Slice(string fname,AnmFile af,int stime,int etime,int looptime){
        var afw=new AnmFile(af);
        float fst=stime/1000f, fet=etime/1000f;
        foreach(var bone in afw)
            foreach(var fl in bone){
                int sidx=-1,eidx=-1;
                for(int i=0; i<fl.Count; i++){ // 範囲内最初と最後の添字取得
                    if(CmpTime(fl[i].time,fst)<0) continue;
                    if(sidx<0) sidx=i;
                    if(CmpTime(fl[i].time,fet)>=0){ eidx=i; break; }
                }
                if(sidx<0){fl.Clear(); continue;}
                if(eidx<0) eidx=fl.Count-1;

                var flnew=new List<AnmFrame>();
                if(CmpTime(fl[sidx].time,fst)!=0){             // 開始時刻がキーフレームでなければ
                    if(sidx>0){ // IKボーン等、部位によっては0msフレームのないものもある
                        var sf=HermiteIp(fst,fl[sidx-1],fl[sidx]); // 最初のフレームを補間で求める
                        flnew.Add(sf);
                    }
                }
                for(int i=sidx; i<eidx; i++) flnew.Add(fl[i]);
                if(CmpTime(fl[eidx].time,fet)==0) flnew.Add(fl[eidx]);
                else{
                    if(eidx>0){
                        var ef=HermiteIp(fet,fl[eidx-1],fl[eidx]); // 最後のフレームを補間で求める
                        flnew.Add(ef);
                    }
                }
                
                fl.Clear(); // フレーム入れ替え
                if(flnew.Count<2) continue; // 最低限最初と最後の２フレームなければ要らない
                if(looptime>0){ // 強引ループ
                    var f=new AnmFrame(flnew[0]);
                    var last=flnew.Count-1;
                    var flt=looptime/1000f;
                    f.time=flnew[last].time+flt;
                    f.tan1=f.tan2=(flnew[1].value-flnew[last].value)/(flt+flnew[1].time-flnew[0].time);
                    flnew.Add(f);
                }
                float t0=flnew[0].time; // fstでもいいけどなんとなく
                foreach(var f in flnew){
                    f.time-=t0;
                    fl.Add(f);
                }
            }
        return afw.write(fname)?0:-1;
    }
    private static int CmpTime(float t1,float t2){ // 1ms単位で時刻比較
        float dt=t1-t2;
        float s=(dt<0)?-1:1;
        float l=dt*s;
        if(l<0.001) return 0;
        return (int)s;
    }
    private static AnmFrame HermiteIp(float ft,AnmFrame f1,AnmFrame f2){ // エルミート補間
        float dt=f2.time-f1.time, dv=f2.value-f1.value;
        float s=(ft-f1.time)/dt;
        float tan1=f1.tan2*dt, tan2=f2.tan1*dt;
        var ret=new AnmFrame(ft);
        float k1=tan1+tan2-2*dv, k2=3*dv-2*tan1-tan2;
        ret.value=((k1*s+k2)*s+tan1)*s+f1.value;
        ret.tan1=ret.tan2=(3*k1*s+2*k2)*s+tan1; // 上の微分
        return ret;
    }
}
}
