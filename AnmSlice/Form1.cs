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

            AnmSlice.Slice(fname,af,stime,etime,looptime);
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
            lstTimes.Items.Clear();
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
