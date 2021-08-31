namespace AnmInterp {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.txtInFile = new System.Windows.Forms.TextBox();
            this.btnInFile = new System.Windows.Forms.Button();
            this.chkJoin = new System.Windows.Forms.CheckBox();
            this.radInterpMv0 = new System.Windows.Forms.RadioButton();
            this.radInterpMvL = new System.Windows.Forms.RadioButton();
            this.radInterpMvCR = new System.Windows.Forms.RadioButton();
            this.radInterpRot0 = new System.Windows.Forms.RadioButton();
            this.radInterpRotL = new System.Windows.Forms.RadioButton();
            this.radInterpRotCR = new System.Windows.Forms.RadioButton();
            this.btnExec = new System.Windows.Forms.Button();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.btnFileListUpd = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ファイル名";
            // 
            // txtInFile
            // 
            this.txtInFile.Enabled = false;
            this.txtInFile.Location = new System.Drawing.Point(72, 10);
            this.txtInFile.Name = "txtInFile";
            this.txtInFile.Size = new System.Drawing.Size(340, 19);
            this.txtInFile.TabIndex = 1;
            // 
            // btnInFile
            // 
            this.btnInFile.Location = new System.Drawing.Point(418, 8);
            this.btnInFile.Name = "btnInFile";
            this.btnInFile.Size = new System.Drawing.Size(45, 23);
            this.btnInFile.TabIndex = 2;
            this.btnInFile.Text = "選択..";
            this.btnInFile.UseVisualStyleBackColor = true;
            this.btnInFile.Click += new System.EventHandler(this.btnInFile_Click);
            // 
            // chkJoin
            // 
            this.chkJoin.AutoSize = true;
            this.chkJoin.Enabled = false;
            this.chkJoin.Location = new System.Drawing.Point(32, 46);
            this.chkJoin.Name = "chkJoin";
            this.chkJoin.Size = new System.Drawing.Size(267, 16);
            this.chkJoin.TabIndex = 3;
            this.chkJoin.Text = "連番ファイルに対し、PoseStream互換の結合を行う";
            this.chkJoin.UseVisualStyleBackColor = true;
            this.chkJoin.CheckedChanged += new System.EventHandler(this.chkJoin_CheckedChanged);
            // 
            // radInterpMv0
            // 
            this.radInterpMv0.AutoSize = true;
            this.radInterpMv0.Location = new System.Drawing.Point(157, 18);
            this.radInterpMv0.Name = "radInterpMv0";
            this.radInterpMv0.Size = new System.Drawing.Size(65, 16);
            this.radInterpMv0.TabIndex = 4;
            this.radInterpMv0.Text = "補間値0";
            this.radInterpMv0.UseVisualStyleBackColor = true;
            // 
            // radInterpMvL
            // 
            this.radInterpMvL.AutoSize = true;
            this.radInterpMvL.Location = new System.Drawing.Point(260, 18);
            this.radInterpMvL.Name = "radInterpMvL";
            this.radInterpMvL.Size = new System.Drawing.Size(71, 16);
            this.radInterpMvL.TabIndex = 5;
            this.radInterpMvL.Text = "線形補間";
            this.radInterpMvL.UseVisualStyleBackColor = true;
            // 
            // radInterpMvCR
            // 
            this.radInterpMvCR.AutoSize = true;
            this.radInterpMvCR.Checked = true;
            this.radInterpMvCR.Location = new System.Drawing.Point(31, 18);
            this.radInterpMvCR.Name = "radInterpMvCR";
            this.radInterpMvCR.Size = new System.Drawing.Size(103, 16);
            this.radInterpMvCR.TabIndex = 6;
            this.radInterpMvCR.TabStop = true;
            this.radInterpMvCR.Text = "Catmull-Rom風";
            this.radInterpMvCR.UseVisualStyleBackColor = true;
            // 
            // radInterpRot0
            // 
            this.radInterpRot0.AutoSize = true;
            this.radInterpRot0.Location = new System.Drawing.Point(157, 18);
            this.radInterpRot0.Name = "radInterpRot0";
            this.radInterpRot0.Size = new System.Drawing.Size(65, 16);
            this.radInterpRot0.TabIndex = 7;
            this.radInterpRot0.Text = "補間値0";
            this.radInterpRot0.UseVisualStyleBackColor = true;
            // 
            // radInterpRotL
            // 
            this.radInterpRotL.AutoSize = true;
            this.radInterpRotL.Location = new System.Drawing.Point(260, 18);
            this.radInterpRotL.Name = "radInterpRotL";
            this.radInterpRotL.Size = new System.Drawing.Size(71, 16);
            this.radInterpRotL.TabIndex = 8;
            this.radInterpRotL.Text = "線形補間";
            this.radInterpRotL.UseVisualStyleBackColor = true;
            // 
            // radInterpRotCR
            // 
            this.radInterpRotCR.AutoSize = true;
            this.radInterpRotCR.Checked = true;
            this.radInterpRotCR.Location = new System.Drawing.Point(31, 18);
            this.radInterpRotCR.Name = "radInterpRotCR";
            this.radInterpRotCR.Size = new System.Drawing.Size(103, 16);
            this.radInterpRotCR.TabIndex = 9;
            this.radInterpRotCR.TabStop = true;
            this.radInterpRotCR.Text = "Catmull-Rom風";
            this.radInterpRotCR.UseVisualStyleBackColor = true;
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(189, 363);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(118, 23);
            this.btnExec.TabIndex = 10;
            this.btnExec.Text = "実行";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // chkReverse
            // 
            this.chkReverse.AutoSize = true;
            this.chkReverse.Checked = true;
            this.chkReverse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReverse.Location = new System.Drawing.Point(32, 195);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(301, 16);
            this.chkReverse.TabIndex = 11;
            this.chkReverse.Text = "180度を超える回転を、逆方向の180度未満の回転に変換";
            this.chkReverse.UseVisualStyleBackColor = true;
            // 
            // btnFileListUpd
            // 
            this.btnFileListUpd.Enabled = false;
            this.btnFileListUpd.Location = new System.Drawing.Point(327, 162);
            this.btnFileListUpd.Name = "btnFileListUpd";
            this.btnFileListUpd.Size = new System.Drawing.Size(94, 23);
            this.btnFileListUpd.TabIndex = 16;
            this.btnFileListUpd.Text = "一覧再取得";
            this.btnFileListUpd.UseVisualStyleBackColor = true;
            this.btnFileListUpd.Click += new System.EventHandler(this.btnFileListUpd_Click);
            // 
            // lstFiles
            // 
            this.lstFiles.CheckOnClick = true;
            this.lstFiles.Enabled = false;
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Location = new System.Drawing.Point(63, 68);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(358, 88);
            this.lstFiles.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radInterpMv0);
            this.groupBox1.Controls.Add(this.radInterpMvCR);
            this.groupBox1.Controls.Add(this.radInterpMvL);
            this.groupBox1.Location = new System.Drawing.Point(32, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 45);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "移動の補間";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radInterpRotCR);
            this.groupBox2.Controls.Add(this.radInterpRot0);
            this.groupBox2.Controls.Add(this.radInterpRotL);
            this.groupBox2.Location = new System.Drawing.Point(32, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 48);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "回転の補間";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnFileListUpd);
            this.Controls.Add(this.chkReverse);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.chkJoin);
            this.Controls.Add(this.btnInFile);
            this.Controls.Add(this.txtInFile);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(500, 450);
            this.MinimumSize = new System.Drawing.Size(500, 450);
            this.Name = "Form1";
            this.Text = "AnmInterp";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInFile;
        private System.Windows.Forms.Button btnInFile;
        private System.Windows.Forms.CheckBox chkJoin;
        private System.Windows.Forms.RadioButton radInterpMv0;
        private System.Windows.Forms.RadioButton radInterpMvL;
        private System.Windows.Forms.RadioButton radInterpMvCR;
        private System.Windows.Forms.RadioButton radInterpRot0;
        private System.Windows.Forms.RadioButton radInterpRotL;
        private System.Windows.Forms.RadioButton radInterpRotCR;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.CheckBox chkReverse;
        private System.Windows.Forms.Button btnFileListUpd;
        private System.Windows.Forms.CheckedListBox lstFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

