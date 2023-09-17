using System.Text.RegularExpressions;
using System.Collections.Generic;
using AnmCommon;
using static System.StringComparison;
using System;

namespace AnmCnv {
public static class AnmCnv {
    public static AnmFile Conv(AnmFile afw,int fm,int newMaxTime,int delay,bool mirrorq){
        if(fm>=0){  // fm: -1=nop / 0=f / 1=m / 2=toggle
            int orig=afw.getGender(); // orig: 0=f / 1=m / -1:no clue
            if(orig!=fm) ChgGender(afw);
        }
        if(newMaxTime>0) SpeedChg(afw,newMaxTime);
        if(delay>0) Delay(afw,delay);
        if(mirrorq) Mirror(afw);
        return afw;
    }
    public static AnmFile SpeedChg(AnmFile afw,int newMaxTime){
        SortedSet<int> ts = afw.getTimeSet();
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
        return afw;
    }
    public static AnmFile Delay(AnmFile afw,int delay){
        // モーション開始遅延(AnmJoinでの結合用)
        foreach (AnmBoneEntry bone in afw)
            foreach (AnmFrameList fl in bone)
                foreach (AnmFrame f in fl) f.time=(f.time*1000+delay)/1000;
        return afw;
    }
    public static AnmFile Mirror(AnmFile afw){
        foreach (AnmBoneEntry bone in afw){
            bone.inOrder();
            bone.rename(mirrorBoneName(bone.boneName));
            // 反転すると補完値が流用できないので、一度1/60秒ごとの値の羅列にする
            for(int i=0; i<bone.Count; i++){
                bone[i]=EveryFrameList(bone[i]);
                AnmFrameList.Linear(bone[i]); // 線形補完しておく(要らないかも)
            }
        }
        foreach (AnmBoneEntry bone in afw){
            if(bone.boneId==0){ // ルート直下
                if(bone.boneName=="Bip01"||bone.boneName=="ManBip")
                    negFrameList2(bone,AXIS.Y,AXIS.X);
                else negFrameList(bone,AXIS.X);
            }else if(bone.boneId==1){
                if(bone.boneName.EndsWith("Pelvis",Ordinal)) negFrameList2(bone,AXIS.Z); // 骨盤
                else if(bone.boneName.EndsWith("chinkoCenter",Ordinal)) negFrameList3(bone,AXIS.Z); // chinkoCenter
                else negFrameList(bone,AXIS.Z);
            }else if(bone.boneId==10) negFrameList2(bone,AXIS.Z); // 背骨1
            else if(bone.boneId==16||bone.boneId==22) negFrameListMune(bone); // 胸
            else negFrameList(bone,AXIS.Z);

            for(int i=0; i<bone.Count; i++)
                AnmFrameList.CatmullRom(bone[i]); // Reduce()はcatmullromが前提なので。
        }
        // 波形にあまり影響のない範囲でキーフレームを減らす
        AnmReduce.AnmReduce.Reduce(afw,1);
        return afw;
    }
    public static AnmFrameList EveryFrameList(AnmFrameList fl){
        if(fl.Count<3) return fl;
        AnmFrameList fl2=new AnmFrameList(fl.type);
        float len=fl[fl.Count-1].time;
        int frames=(int)(len*60);
        for(int i=0,fidx=0; i<frames; i++){
            float t=i/60f;
            float d=t-fl[fidx+1].time;
            if(d>-0.00001) fidx++;      // 2つ以上とばす事はないはず
            var fr=HermiteIp(t,fl[fidx],fl[fidx+1]);
            fl2.Add(fr);
        }
        fl2.Add(fl[fl.Count-1]);
        return fl2;
    }
    // 左右反転
    // 指定した軸方向(その部位で左右にあたる方向)について
    //   移動: 移動量 * -1
    //   回転: 中心軸の指定軸方向成分 * -1、および回転角 * -1
    private static bool[] negarr(AXIS ax,AXIS axm=AXIS.N){
        bool[] neg=new bool[7];

        neg[3]=true;
        if(ax==AXIS.X) neg[0]=true; else if(ax==AXIS.Y) neg[1]=true; else neg[2]=true;

        AXIS ax2=(axm==AXIS.N)?ax:axm;
        if(ax2==AXIS.X) neg[4]=true; else if(ax2==AXIS.Y) neg[5]=true; else neg[6]=true;

        return neg;
    }
    enum AXIS{ X,Y,Z,N };
    private static void negFrameList(AnmBoneEntry abe,AXIS ax){
        bool[] neg=negarr(ax);
        foreach (AnmFrameList fl in abe){
            if(neg[fl.type-100]) foreach (AnmFrame f in fl){
                f.value=-f.value;
                //f.tan1=-f.tan1;f.tan2=-f.tan2;
            }
        }
    }
    // 胸用
    private static void negFrameListMune(AnmBoneEntry abe){
        // 胸は左右のボーンがローカルx軸方向に180度ねじれた関係
        // (上腕を前に出した姿勢におけるClavicle＋UpperArmの回転をMune_L/R１つでやる感じ?)
        // なので右から{1,0,0,0}をかけるが、数式上結果は{w,z,-y,-x}になる
        // モーションを左右反転(親z軸方向)するためにzとwを-1倍するので
        // {-w,-z,-y,-x}が答え
        foreach (AnmFrameList fl in abe){
            if(fl.type<104) fl.type=(byte)(203-fl.type); // 入れ替え
            if(fl.type<104||fl.type==106) foreach (AnmFrame f in fl){
                f.value=-f.value;
                //f.tan1=-f.tan1; f.tan2=-f.tan2;
            }
        }
        abe.inOrder(); // fl.typeを付け替えたのでソート
    }
    // 組み立て変換込みの反転
    // 組み立て変換をQ、ポーズ正味のポーズ部分をPとすると、Qはそのまま、Pのみ左右反転する。
    // anmファイル上のデータはQPの値。左から~QをかけてPを取り出し(~Q(QP)=(~QQ)P=P)、
    // Pの左右を反転した後、左からQをかけてQPに戻す
    private static void negFrameList2(AnmBoneEntry abe,AXIS ax,AXIS axm=AXIS.N){
        negFrameListSub(abe,ax,axm,kumitate,rKumitate);
    }
    // chinkoCenter用
    private static void negFrameList3(AnmBoneEntry abe,AXIS ax,AXIS axm=AXIS.N){
        negFrameListSub(abe,ax,axm,chinK,rChinK);
    }
    private static void negFrameListSub(AnmBoneEntry abe,AXIS ax,AXIS axm,float[] kumitate,float[] rkumitate){
        bool[] neg=negarr(ax,axm);
        List<float[]> ql=new List<float[]>();   // 四元数
        foreach (AnmFrameList fl in abe){
            if(fl.type<104){
                // 回転は一旦、四元数の形にまとめる
                for(int i=0; i<fl.Count; i++){
                    if(ql.Count==i) ql.Add(new float[4]);
                    ql[i][fl.type-100]=fl[i].value;
                }
            }else if(neg[fl.type-100]) foreach (AnmFrame f in fl){
                // 移動は普通に済ませる
                f.value=-f.value; // f.tan1=-f.tan1;f.tan2=-f.tan2;
            }
        }
        foreach(float[] q in ql){
            Quaternion.mul(q,rkumitate,q);
            // 正味部分(P)を左右反転
            if(neg[0]) q[0]*=-1;
            if(neg[1]) q[1]*=-1;
            if(neg[2]) q[2]*=-1;
            if(neg[3]) q[3]*=-1;
            // 組み立て変換を戻す
            Quaternion.mul(q,kumitate,q);
        }

        // 四元数の値を戻す。補間は後でまとめてやるので要らない
        foreach (AnmFrameList fl in abe){
            if(fl.type>=104) continue;
            int ty=fl.type-100;
            for(int i=0; i<fl.Count; i++) fl[i].value=ql[i][ty];
        }
    }
    private static bool isZero(float f){ return f>-0.00000001f &&f<0.00000001f; }
    private static float[] kumitate={0.5f,-0.5f,-0.5f,-0.5f};    // Bip01
    private static float[] rKumitate={-0.5f,0.5f,0.5f,-0.5f};
    private static float[] chinK={0.7071068f,-0.7071068f,0f,0f}; // chinkoCenter
    private static float[] rChinK={-0.7071068f,0.7071068f,0f,0f};
    private static string mirrorBoneName(string bname){
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

    // 性転換
    private static void ChgGender(AnmFile afw){
        string[][] f2m = {      // Spineの構成が男女で違う
            new string[]{"Bip01 Spine0a/Bip01 Spine1","ManBip Spine1"},
            new string[]{"Spine1a","Spine2"},
            new string[]{"Bip01","ManBip"},
        };
        int fm = afw.getGender();  // 男性->女性なら逆順
        if(fm==-1) return;        // 性別不明

        // ボーン名変更
        int insPos = -1;
        bool has0a=false;
        SortedSet<int> ts = afw.getTimeSet();
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
