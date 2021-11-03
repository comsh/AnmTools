using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using AnmCommon;

namespace AnmSlice {
    public partial class form1:Form {
        public form1() {
            InitializeComponent();
        }
	    private AnmFile af;
        private int mintime=0;
        private int maxtime=0;
		// 切り出し実行
        private void btnSlice_Click(object sender,EventArgs e) {
            if(af==null) return;
            if(lstTimes.Items.Count==0) return;
            if(txtStime.Text==""||txtEtime.Text=="") return;
            int stime,etime,looptime=0;
            if(!int.TryParse(txtStime.Text,out stime)||stime<mintime
             ||!int.TryParse(txtEtime.Text,out etime)||etime<0||etime>maxtime||etime<=stime){
                MessageBox.Show("時刻範囲が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(chkLoop.Checked){
                if(!int.TryParse(txtLoop.Text,out looptime)||looptime<=0){
                    MessageBox.Show("ループ移行時間が不正です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
          	string fname = outFileDialog();
            if(string.IsNullOrEmpty(fname)) return;

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
            afw.write(fname);
        }
        private int CmpTime(float t1,float t2){ // 1ms単位で時刻比較
            float dt=t1-t2;
            float s=(dt<0)?-1:1;
            float l=dt*s;
            if(l<0.001) return 0;
            return (int)s;
        }
        private AnmFrame HermiteIp(float ft,AnmFrame f1,AnmFrame f2){ // エルミート補間
            float dt=f2.time-f1.time, dv=f2.value-f1.value;
            float s=(ft-f1.time)/dt;
            float tan1=f1.tan2, tan2=f2.tan1;
            var ret=new AnmFrame(ft);
            float k1=-tan1+tan2-2*dv, k2=3*dv-2*tan1-tan2;
            ret.value=((k1*s+k2)*s+tan1)*s+f1.value;
            ret.tan1=ret.tan2=(3*k1*s+2*k2)*s+tan1; // 上の微分
            return ret;
        }
        // UI連動
        private void lstTimes_SelectedIndexChanged(object sender,EventArgs e) {
            if(lstTimes.SelectedItems.Count==0) return;
            if(lstTimes.SelectedItems.Count==1){
                if(!int.TryParse(txtEtime.Text,out int etime)||etime<=0) return;
                int v=(int)lstTimes.SelectedItems[0];
                if(v<etime) txtStime.Text=v.ToString();
                return;
            }
            int min=int.MaxValue, max=0;
            for(int i=0; i<lstTimes.SelectedItems.Count; i++){
                int v=(int)lstTimes.SelectedItems[i];
                if(v<min) min=v;
                if(v>max) max=v;
            }
            txtStime.Text=min.ToString();
            txtEtime.Text=max.ToString();
        }
        private void chkLoop_CheckedChanged(object sender,EventArgs e) {
            txtLoop.Enabled=chkLoop.Checked;
        }

		// ファイル選択
		private void btnInput_Click(object sender, EventArgs e) {
			string fname = fileDialog();
			if (fname!="") handleInputFileSelected(fname);
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
		private void handleInputFileSelected(string fname){
			if (!fname.EndsWith(".anm")) return;
            txtInput.Text=fname;
            txtInput.ScrollToCaret();
            lastPath=Path.GetDirectoryName(Path.GetFullPath(fname));
			af=AnmFile.fromFile(fname);
			if (af==null) return;
            txtStime.Text="";
            txtEtime.Text="";
			SortedSet<int> ts = af.getTimeSet();
            if(ts.Count==0) return;
            foreach(int t in ts) lstTimes.Items.Add(t);
            mintime=(int)lstTimes.Items[0];
            maxtime=(int)lstTimes.Items[lstTimes.Items.Count-1];
            txtStime.Text=mintime.ToString();
            txtEtime.Text=maxtime.ToString();
		}
        private string lastPath="";  // D&Dで指定されたファイルのパスも覚える
		private string fileDialog(){
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


    }
}
