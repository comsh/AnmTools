using AnmCommon;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnmJoin {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
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

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            addFiles(files);
        }

        private void btnAddFile_Click(object sender, EventArgs e) {
            string[] files = inFileDialog();
            if (files != null) addFiles(files);
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            if (lstFiles.SelectedItems.Count==0) return;
            while (lstFiles.SelectedIndices.Count>0) {
                lstFiles.Items.RemoveAt(lstFiles.SelectedIndices[0]);
            }
        }

        private void btnJoin_Click(object sender, EventArgs e) {
            string outname = outFileDialog();
            if (outname != null) {
                List<AnmFile> files = new List<AnmFile>();
                try {
                    foreach (string fname in lstFiles.Items) files.Add(new AnmFile(fname));
                    AnmFile.joinAnm(files, outname);
                } catch {
                    MessageBox.Show("出力に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 下請け

        private void addFiles(string[] files) {  // 重複を省きつつリストにファイルを追加
            foreach(string file in files) {
                if(!lstFiles.Items.Contains(file)) lstFiles.Items.Add(file);
            }
        }
        private string[] inFileDialog() {
            string[] fname = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "入力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            dialog.Multiselect=true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileNames;
            dialog.Dispose();
            return fname;
        }
        private string outFileDialog() {
            string fname = null;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "出力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }
    }
}
