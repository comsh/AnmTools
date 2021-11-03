using AnmCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AnmSplit
{
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
            lbs=new CheckedListBox[] { lstSpine,lstRArm,lstLArm,lstRLeg,lstLLeg };
		}
        private CheckedListBox[] lbs;
        private static int[] boneId2lstIdx={
            6,7,                    // 中心、骨盤
            4000,4001,4002,4003,    // 左足付根～左足指
            3000,3001,3002,3003,    // 右足付根～右足指
            5,4,3,2,1,0,            // 背骨4～頭
            2000,2001,2002,2003,2004,2005,  // 左胸～左手指
            1000,1001,1002,1003,1004,1005   // 右胸～右手指
        };

		private AnmFile af = null;              // 読み込んだanmファイル

		// 分割実行
		private void btnSplit_Click(object sender, EventArgs e) {
			string ofile1 = filenameOntheSamePath(txtChecked.Text, txtInput.Text);
			string ofile2 = filenameOntheSamePath(txtUnchecked.Text, txtInput.Text);
			if (ofile1.Length == 0 && ofile2.Length == 0) return;
			bool[] flt = new bool[AnmBoneName.boneGroup.Length];
			for (int i = 0; i < flt.Length; i++) {
                int lstidx=boneId2lstIdx[i];
				flt[i]=lbs[lstidx/1000].GetItemChecked(lstidx%1000);
			}
			if (ofile1.Length>0 && !af.write(ofile1, flt, true)) return;
			if (ofile2.Length>0) af.write(ofile2, flt, false);
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

			txtInput.Text = fname;
			txtChecked.Text = Path.GetFileNameWithoutExtension(fname)+"_A.anm";
			txtUnchecked.Text = Path.GetFileNameWithoutExtension(fname) + "_B.anm";

            lastPath=Path.GetDirectoryName(Path.GetFullPath(fname));

			af=AnmFile.fromFile(fname);
			if (af==null) return;

			SortedSet<int> ts = af.getTimeSet();
			lblAnmInfo.Text = $"時間範囲: {ts.Min}ms - {ts.Max}ms";

            DeselectAll();
			for (int i = 0; i<af.Count; i++) if (af[i].Count>0){
                int lidx=boneId2lstIdx[af[i].boneId];
                lbs[lidx/1000].SetItemChecked(lidx%1000,true);
			}
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
		private void DeselectAll() {
            lstSpine.ClearSelected();
            lstRArm.ClearSelected();
            lstLArm.ClearSelected();
            lstRLeg.ClearSelected();
            lstLLeg.ClearSelected();
            CListSetAll(lstSpine,false);
            CListSetAll(lstRArm,false);
            CListSetAll(lstLArm,false);
            CListSetAll(lstRLeg,false);
            CListSetAll(lstLLeg,false);
		}
		private string filenameOntheSamePath(string fname,string path){
			if (fname.Length==0 || path.Length==0 || !path.Contains("\\")) return "";
			if (fname.Contains("\\")) return fname;
			return Path.GetDirectoryName(path) +"\\"+ fname;
        }

        private void CListSetAll(CheckedListBox lst,bool onoff){
            for(int i=0; i<lst.Items.Count; i++) lst.SetItemChecked(i,onoff);
        }
        private void btnRAall_Click(object sender,EventArgs e) { CListSetAll(lstRArm,true); }
        private void btnRAclr_Click(object sender,EventArgs e) { CListSetAll(lstRArm,false); }
        private void btnSPall_Click(object sender,EventArgs e) { CListSetAll(lstSpine,true); }
        private void btnSPclr_Click(object sender,EventArgs e) { CListSetAll(lstSpine,false); }
        private void btnLAall_Click(object sender,EventArgs e) { CListSetAll(lstLArm,true); }
        private void btnLAclr_Click(object sender,EventArgs e) { CListSetAll(lstLArm,false); }
        private void btnRLall_Click(object sender,EventArgs e) { CListSetAll(lstRLeg,true); }
        private void btnRLclr_Click(object sender,EventArgs e) { CListSetAll(lstRLeg,false); }
        private void btnLLall_Click(object sender,EventArgs e) { CListSetAll(lstLLeg,true); }
        private void btnLLclr_Click(object sender,EventArgs e) { CListSetAll(lstLLeg,false); }

        // チェックボックスより選択中の背景色の方が目立つのはどうなの
        private void lstSpine_MouseLeave(object sender,EventArgs e) { lstSpine.ClearSelected(); }
        private void lstRArm_MouseLeave(object sender,EventArgs e) { lstRArm.ClearSelected(); }
        private void lstLArm_MouseLeave(object sender,EventArgs e) { lstLArm.ClearSelected(); }
        private void lstRLeg_MouseLeave(object sender,EventArgs e) { lstRLeg.ClearSelected(); }
        private void lstLLeg_MouseLeave(object sender,EventArgs e) { lstLLeg.ClearSelected(); }
        private void lstRArm_Leave(object sender,EventArgs e) { lstSpine.ClearSelected(); }
        private void lstSpine_Leave(object sender,EventArgs e) { lstRArm.ClearSelected(); }
        private void lstLArm_Leave(object sender,EventArgs e) { lstLArm.ClearSelected(); }
        private void lstRLeg_Leave(object sender,EventArgs e) { lstRLeg.ClearSelected(); }
        private void lstLLeg_Leave(object sender,EventArgs e) { lstLLeg.ClearSelected(); }
    }
}

