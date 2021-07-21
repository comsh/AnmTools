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
            StringBuilder sb = new StringBuilder();
            StringWriter sw=new StringWriter(sb);
            if(DmpPmd.Dmp(fname,sw)<0) return;
            sw.Close();
            textBox1.Enabled=true;
            textBox1.ReadOnly=false;
            textBox1.Text=sb.ToString();
            currentFilename=fname;
        }
        private void 保存ToolStripMenuItem_Click(object sender,EventArgs e) {
            string outfilename = outFileDialog();
            if (outfilename==null) return;
            DmpPmd.Pmd(textBox1.Text,outfilename);
        } 
        private string fileDialog() {
            string fname = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "入力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            dialog.RestoreDirectory = true;
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
            dialog.FileName = System.IO.Path.GetFileNameWithoutExtension(currentFilename)+"_undumped.anm";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }
    }
}
