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
		}

		private AnmFile af = null;              // 読み込んだanmファイル

		// 分割実行
		private void btnSplit_Click(object sender, EventArgs e) {
			string ofile1 = filenameOntheSamePath(txtChecked.Text, txtInput.Text);
			string ofile2 = filenameOntheSamePath(txtUnchecked.Text, txtInput.Text);
			if (ofile1.Length == 0 && ofile2.Length == 0) return;
			bool[] flt = new bool[AnmBoneName.boneGroup.Length];
			for (int i = 0; i < flt.Length; i++) {
				flt[i]=lstRegion.GetItemChecked(i);
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

			af=AnmFile.fromFile(fname);
			if (af==null) return;

			SortedSet<int> ts = af.getTimeSet();
			lblAnmInfo.Text = $"時間範囲: {ts.Min}ms - {ts.Max}ms";

			chkListUnckeckAll(lstRegion);
			for (int i = 0; i<af.Count; i++) {
				if (af[i].Count>0){
                    lstRegion.SetItemChecked(af[i].boneId, true);
                }
			}
		}
		private string fileDialog(){
			string fname = "";
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Title = "入力anmファイル選択";
			dialog.Filter = "anmファイル|*.anm";
			dialog.RestoreDirectory = true;
			if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
			dialog.Dispose();
			return fname;
		}
		private void chkListUnckeckAll(CheckedListBox cl) {
			for (int i = 0; i < cl.Items.Count; i++) cl.SetItemChecked(i, false);
		}
		private string filenameOntheSamePath(string fname,string path){
			if (fname.Length==0 || path.Length==0 || !path.Contains("\\")) return "";
			if (fname.Contains("\\")) return fname;
			return Path.GetDirectoryName(path) +"\\"+ fname;
        }
	}
}

