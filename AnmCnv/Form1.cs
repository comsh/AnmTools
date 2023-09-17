using AnmCommon;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnmCnv {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private AnmFile af = null;      // 読み込んだanmファイル

        // 変換実行
        private void btnCnv_Click(object sender, EventArgs e) {
            if (txtInput.Text=="") return;

            // バリデーション
            int newMaxTime=0,delay=0;
            if (chkSpeed.Checked) {
                if(!int.TryParse(txtSpeed.Text,out newMaxTime)||newMaxTime<=0){
                    MessageBox.Show("最終フレーム時刻は正の整数で指定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (chkDelay.Checked) {
                if(!int.TryParse(txtDelay.Text,out delay)||delay<=0){
                    MessageBox.Show("遅延時間は正の整数で指定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string outfilename = outFileDialog();
            if (outfilename==null) return;

            AnmFile afw = new AnmFile(af);     // 変更＆書き出し用コピー
            AnmCnv.Conv(afw,chkGender.Checked?2:-1,newMaxTime,delay,chkMirror.Checked);

            // 書き出し
            afw.write(outfilename);
        }

        // UI連動
        private void chkSpeed_CheckedChanged(object sender, EventArgs e) {
            txtSpeed.Enabled = chkSpeed.Checked;
        }
        private void chkDelay_CheckedChanged(object sender, EventArgs e) {
            txtDelay.Enabled = chkDelay.Checked;
        }

        // ファイル選択
        private void btnInput_Click(object sender, EventArgs e) {
            string fname = fileDialog();
            if (fname != "") handleInputFileSelected(fname);
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
        private void handleInputFileSelected(string fname) {
            if (!fname.EndsWith(".anm")) return;
            txtInput.Text = fname;
            lastPath=System.IO.Path.GetDirectoryName(fname);

            af=AnmFile.fromFile(fname);
            if (af==null) return;
            SortedSet<int> ts = af.getTimeSet();

            int gi=af.getGender();
            string gender = "性別不明";
            chkGender.Enabled = true;
            if(gi==0) gender="女性用";
            else if(gi==1) gender="男性用";
            else chkGender.Enabled = false;

            lblAnmInfo.Text = $"{gender}   時間範囲: {ts.Min}ms - {ts.Max}ms";

            txtSpeed.Text=ts.Max.ToString();
            txtDelay.Text="0";
            chkGender.Checked = false;
            chkSpeed.Checked = false;
            chkDelay.Checked = false;
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
