using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AnmCommon;

namespace AnmInterp {
    public partial class Form1:Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnExec_Click(object sender,EventArgs e) {
            if (txtInFile.Text=="") return;
            string outfilename = outFileDialog();
            if (outfilename==null) return;

            AnmFile af;
            if(chkJoin.Checked){
                if(lstFiles.CheckedItems.Count<2){
                    MessageBox.Show("結合対象ファイルは２つ以上選択してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string[] fnames=new string[lstFiles.CheckedItems.Count];
                lstFiles.CheckedItems.CopyTo(fnames,0);
                string dir=Path.GetDirectoryName(txtInFile.Text)+"\\";
                for(int i=0; i<fnames.Length; i++) fnames[i]=dir+fnames[i]+".anm";
                af=JoinPs(fnames);
            }else af=AnmFile.fromFile(txtInFile.Text);
            if(af==null){
                MessageBox.Show("ファイルが読めません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int mt=0,rt=0;
            if(radInterpMvL.Checked) mt=1; else if(radInterpMvCR.Checked) mt=2; else if(radInterpMvNop.Checked) mt=3;
            if(radInterpRotL.Checked) rt=1; else if(radInterpRotCR.Checked) rt=2; else if(radInterpRotNop.Checked) rt=3;
            Interp(af,chkReverse.Checked,mt,rt);
            af.write(outfilename);
        }

        private void btnFileListUpd_Click(object sender,EventArgs e) {
            FileListUpd(txtInFile.Text);
        }
        private void btnInFile_Click(object sender,EventArgs e) {
            string fname = fileDialog();
            if (fname != "") handleInputFileSelected(fname);
        }
        private void Form1_DragEnter(object sender,DragEventArgs e) {
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
        private void Form1_DragDrop(object sender,DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            handleInputFileSelected(files[0]);

        }

        // UI連動
        private void chkJoin_CheckedChanged(object sender,EventArgs e) {
            lstFiles.Enabled=chkJoin.Checked;
            btnFileListUpd.Enabled=chkJoin.Checked;
        }

        private Regex reg=new Regex(@"_\d{8}\.anm$",RegexOptions.Compiled);
        private void handleInputFileSelected(string fname) {
            if (!fname.EndsWith(".anm",StringComparison.Ordinal)) return;
            txtInFile.Text = fname;
            lastPath=Path.GetFileName(fname);

            chkJoin.Checked=false;
            chkJoin.Enabled=false;
            lstFiles.Enabled=false;
            btnFileListUpd.Enabled=false;
            lstFiles.Items.Clear();
            if(reg.Match(fname).Success){
                chkJoin.Enabled=chkJoin.Checked=true;
                FileListUpd(fname);
            }
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
            string name=txtInFile.Text;
            Match m=reg.Match(name);
            if(m.Success) name=name.Substring(0,name.Length-13)+".anm"; else name=name.Substring(0,name.Length-4)+"_modified.anm";
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName =Path.GetFileName(name);
            dialog.InitialDirectory=Path.GetDirectoryName(name);
            dialog.Title = "出力anmファイル選択";
            dialog.Filter = "anmファイル|*.anm";
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }
        private void FileListUpd(string name){
            lstFiles.Items.Clear();
            int fnidx=name.LastIndexOf('\\');
            if(fnidx<0) fnidx=name.LastIndexOf('/');
            if(fnidx<0) return;
            string path=name.Substring(0,fnidx);
            string fname=name.Substring(fnidx+1);
            string pfx=fname.Substring(0,fname.Length-13);
            string[] files=Directory.GetFiles(path,pfx+"_????????.anm",SearchOption.TopDirectoryOnly);
            Array.Sort(files);
            for(int i=0; i<files.Length; i++) if(reg.Match(files[i]).Success)
                lstFiles.Items.Add(Path.GetFileNameWithoutExtension(files[i]),true);
        }

        // 結合・変換
        private AnmFile JoinPs(string[] fns){
            bool muneL=false,muneR=false;
            Dictionary<string,AnmBoneEntry> bones=new Dictionary<string,AnmBoneEntry>();
            for(int i=0; i<fns.Length; i++){
                int ms=GetMsSfx(fns[i]);
                AnmFile af=AnmFile.fromFile(fns[i]);
                if(ms<0||af==null) return null;
                muneL|=(af.muneLR[0]==1);
                muneR|=(af.muneLR[1]==1);
                SingleFrame(af,ms);
                foreach(var b in af){
                    string key=b.getSortkey();
                    if(!bones.ContainsKey(key)) bones.Add(key,b);
                    else AnmFile.mergeBone(bones[key],b);
                }
            }
            AnmFile ret=new AnmFile();
            ret.format=1001;
            if(muneL) ret.muneLR[0]=1;
            if(muneR) ret.muneLR[1]=1;
			List<string> dKeys=new List<string>(bones.Keys);
			dKeys.Sort();
            for(int i=0; i<dKeys.Count; i++) ret.Add(bones[dKeys[i]]);
            return ret;
        }
        private int GetMsSfx(string fn){
            if(int.TryParse(fn.Substring(fn.Length-12,8),out int ms)) return ms;
            return -1;
        }
        private void SingleFrame(AnmFile afw,int ms){
            foreach (AnmBoneEntry bone in afw)
                foreach (AnmFrameList fl in bone){
                    fl[0].time=ms/1000f;
                    for(int i=fl.Count-1; i>=1; i--) fl.RemoveAt(i);
                }
        }
        private void Interp(AnmFile af,bool rq,int mtype,int rtype){
            foreach (AnmBoneEntry bone in af){
                bone.inOrder();
                if(rq) quatNormal(bone);

                // 最初と最後のキーフレームが完全に同じならループしてると解釈 -> catmull-romに影響
                bool loop=true;
                foreach (AnmFrameList fl in bone) if(fl[0].value!=fl[fl.Count-1].value) loop=false;

                foreach (AnmFrameList fl in bone){
                    if((fl.type<104 && rtype==0) || (fl.type>=104 && mtype==0))
                        for(int i=0; i<fl.Count; i++) fl[i].tan1=fl[i].tan2=0;
                    else if((fl.type<104 && rtype==1) || (fl.type>=104 && mtype==1)){
                        fl[0].tan1=fl[0].tan2=(fl[1].value-fl[0].value)/(fl[1].time-fl[0].time);
                        for(int i=1; i<fl.Count-1; i++){
                            fl[i].tan1=(fl[i].value-fl[i-1].value)/(fl[i].time-fl[i-1].time);
                            fl[i].tan2=(fl[i+1].value-fl[i].value)/(fl[i+1].time-fl[i].time);
                        }
                        int last=fl.Count-1;
                        fl[last].tan1=fl[last].tan2=(fl[last].value-fl[last-1].value)/(fl[last].time-fl[last-1].time);
                    }else if((fl.type<104 && rtype==2) || (fl.type>=104 && mtype==2)) {
                        int last=fl.Count-1;
                        if(loop && fl.Count>=3){ // [0]と[last]は意味的に同一
                            fl[0].tan1=fl[0].tan2=(fl[1].value-fl[last-1].value)/(fl[1].time+(fl[last].time-fl[last-1].time));
                            fl[last].tan1=fl[last].tan2=(fl[1].value-fl[last-1].value)/(fl[1].time+(fl[last].time-fl[last-1].time));
                        }else{  // loopしてないとき
                            fl[0].tan1=fl[0].tan2=(fl[1].value-fl[0].value)/(fl[1].time-fl[0].time);
                            fl[last].tan1=fl[last].tan2=(fl[last].value-fl[last-1].value)/(fl[last].time-fl[last-1].time);
                        }
                        for(int i=1; i<last; i++)
                            fl[i].tan1=fl[i].tan2=(fl[i+1].value-fl[i-1].value)/(fl[i+1].time-fl[i-1].time);
                    }
                }
            }
        }
        private void quatNormal(AnmBoneEntry bone){
            List<float[]> ql=getRotValues(bone);
            float[] qt=new float[4];
            for(int i=1; i<ql.Count; i++){
                qmul(qt,ql[i],qinv(ql[i-1]));
                if(qt[3]<0){ ql[i][0]*=-1; ql[i][1]*=-1; ql[i][2]*=-1; ql[i][3]*=-1;}
            }
            setRotValues(bone,ql);
        }
        private List<float[]> getRotValues(AnmBoneEntry bone){
            List<float[]> ql=new List<float[]>(); 
            foreach (AnmFrameList fl in bone){
                if(fl.type>=104) continue;
                for(int i=0; i<fl.Count; i++){
                    if(ql.Count==i) ql.Add(new float[4]);
                    ql[i][fl.type-100]=fl[i].value;
                }
            }
            return ql;
        }
        private void setRotValues(AnmBoneEntry bone,List<float[]> ql){
            foreach (AnmFrameList fl in bone){
                if(fl.type>=104) continue;
                for(int i=0; i<fl.Count; i++) fl[i].value=ql[i][fl.type-100];
            }
        }
        private float[] qinv(float[] q){
            float[] ret=new float[4];
            ret[0]=-q[0]; ret[1]=-q[1]; ret[2]=-q[2]; ret[3]=q[3];
            return ret;
        }
        private void qmul(float[] p,float[] q,float[] r){
            float x = q[1]*r[2] - q[2]*r[1] + r[3]*q[0] + q[3]*r[0];
            float y = q[2]*r[0] - q[0]*r[2] + r[3]*q[1] + q[3]*r[1];
            float z = q[0]*r[1] - q[1]*r[0] + r[3]*q[2] + q[3]*r[2];
            float w = q[3]*r[3] - q[0]*r[0] - r[1]*q[1] - q[2]*r[2];
            p[0]=x; p[1]=y; p[2]=z; p[3]=w;
        }
    }
}
