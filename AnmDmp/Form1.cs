using AnmCommon;
using System;
using AnmDmpCommon;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AnmDmp {
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
            if (!files[0].EndsWith(".anm")) return;
            handleInputFileSelected(files[0]);
        }
        private void 読込ToolStripMenuItem_Click(object sender,EventArgs e) {
            string fname = fileDialog();
            if (fname != "") handleInputFileSelected(fname);
        }
        private void handleInputFileSelected(string fname){
            string tmp=Path.GetTempFileName();
            try{
                using(StreamWriter sw=File.CreateText(tmp)){
                    if(DmpPmd.Dmp(fname,sw)<0){
                        File.Delete(tmp);
                        MessageBox.Show(DmpPmd.error,"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                textBox1.Text=File.ReadAllText(tmp);
                textBox1.Enabled=true;
                textBox1.ReadOnly=false;
                File.Delete(tmp);
            }catch{
                MessageBox.Show("ファイル読込に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            currentFilename=fname;
            lastPath=Path.GetDirectoryName(fname);
        }
        private void 別名保存ToolStripMenuItem_Click(object sender,EventArgs e) {
            string outfilename = outFileDialog();
            if (outfilename==null) return;
            DmpPmd.Pmd(textBox1.Text,outfilename);
        } 
        private void 保存ToolStripMenuItem_Click(object sender,EventArgs e) {
            DmpPmd.Pmd(textBox1.Text,currentFilename);
        }
        private string lastPath="";
        private string fileDialog() {
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
        private string currentFilename;
        private string outFileDialog() {
            if(currentFilename==null) return null;
            string fname = null;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "出力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            dialog.InitialDirectory=Path.GetDirectoryName(currentFilename);
            dialog.FileName = Path.GetFileNameWithoutExtension(currentFilename)+"_undumped.anm";
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }

        private void Form1_ResizeBegin(object sender,EventArgs e) { SuspendLayout(); }
        private void Form1_ResizeEnd(object sender,EventArgs e) { ResumeLayout(); }
    }
}
