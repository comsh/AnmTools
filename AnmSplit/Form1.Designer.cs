namespace AnmSplit
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSplit = new System.Windows.Forms.Button();
            this.lblAnmInfo = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUnchecked = new System.Windows.Forms.TextBox();
            this.txtChecked = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRAclr = new System.Windows.Forms.Button();
            this.lstRArm = new System.Windows.Forms.CheckedListBox();
            this.btnRAall = new System.Windows.Forms.Button();
            this.lstRLeg = new System.Windows.Forms.CheckedListBox();
            this.lstSpine = new System.Windows.Forms.CheckedListBox();
            this.lstLArm = new System.Windows.Forms.CheckedListBox();
            this.lstLLeg = new System.Windows.Forms.CheckedListBox();
            this.btnRLall = new System.Windows.Forms.Button();
            this.btnRLclr = new System.Windows.Forms.Button();
            this.btnSPall = new System.Windows.Forms.Button();
            this.btnSPclr = new System.Windows.Forms.Button();
            this.btnLAall = new System.Windows.Forms.Button();
            this.btnLAclr = new System.Windows.Forms.Button();
            this.btnLLall = new System.Windows.Forms.Button();
            this.btnLLclr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(149, 363);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(131, 23);
            this.btnSplit.TabIndex = 10;
            this.btnSplit.Text = "分割";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // lblAnmInfo
            // 
            this.lblAnmInfo.AutoSize = true;
            this.lblAnmInfo.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.lblAnmInfo.Location = new System.Drawing.Point(262, 44);
            this.lblAnmInfo.Name = "lblAnmInfo";
            this.lblAnmInfo.Size = new System.Drawing.Size(0, 11);
            this.lblAnmInfo.TabIndex = 15;
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(379, 20);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(39, 22);
            this.btnInput.TabIndex = 2;
            this.btnInput.Text = "選択";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "非選択部位出力ファイル";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "選択部位出力ファイル";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "入力ファイル";
            // 
            // txtUnchecked
            // 
            this.txtUnchecked.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtUnchecked.Location = new System.Drawing.Point(174, 327);
            this.txtUnchecked.Name = "txtUnchecked";
            this.txtUnchecked.Size = new System.Drawing.Size(193, 19);
            this.txtUnchecked.TabIndex = 8;
            // 
            // txtChecked
            // 
            this.txtChecked.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtChecked.Location = new System.Drawing.Point(174, 296);
            this.txtChecked.Name = "txtChecked";
            this.txtChecked.Size = new System.Drawing.Size(193, 19);
            this.txtChecked.TabIndex = 8;
            // 
            // txtInput
            // 
            this.txtInput.Enabled = false;
            this.txtInput.Location = new System.Drawing.Point(84, 22);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(289, 19);
            this.txtInput.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "右腕";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "脊椎";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "左腕";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "右脚";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "左脚";
            // 
            // btnRAclr
            // 
            this.btnRAclr.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnRAclr.Location = new System.Drawing.Point(109, 61);
            this.btnRAclr.Margin = new System.Windows.Forms.Padding(1);
            this.btnRAclr.Name = "btnRAclr";
            this.btnRAclr.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnRAclr.Size = new System.Drawing.Size(18, 18);
            this.btnRAclr.TabIndex = 33;
            this.btnRAclr.TabStop = false;
            this.btnRAclr.Text = "無";
            this.btnRAclr.UseVisualStyleBackColor = true;
            this.btnRAclr.Click += new System.EventHandler(this.btnRAclr_Click);
            // 
            // lstRArm
            // 
            this.lstRArm.CheckOnClick = true;
            this.lstRArm.FormattingEnabled = true;
            this.lstRArm.Items.AddRange(new object[] {
            "胸(右)",
            "鎖骨(右)",
            "肩(右)",
            "肘(右)",
            "手首(右)",
            "手指(右)"});
            this.lstRArm.Location = new System.Drawing.Point(35, 82);
            this.lstRArm.Name = "lstRArm";
            this.lstRArm.Size = new System.Drawing.Size(90, 88);
            this.lstRArm.TabIndex = 3;
            this.lstRArm.Leave += new System.EventHandler(this.lstRArm_Leave);
            this.lstRArm.MouseLeave += new System.EventHandler(this.lstRArm_MouseLeave);
            // 
            // btnRAall
            // 
            this.btnRAall.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnRAall.Location = new System.Drawing.Point(89, 61);
            this.btnRAall.Margin = new System.Windows.Forms.Padding(1);
            this.btnRAall.Name = "btnRAall";
            this.btnRAall.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnRAall.Size = new System.Drawing.Size(18, 18);
            this.btnRAall.TabIndex = 37;
            this.btnRAall.TabStop = false;
            this.btnRAall.Text = "全";
            this.btnRAall.UseVisualStyleBackColor = true;
            this.btnRAall.Click += new System.EventHandler(this.btnRAall_Click);
            // 
            // lstRLeg
            // 
            this.lstRLeg.CheckOnClick = true;
            this.lstRLeg.FormattingEnabled = true;
            this.lstRLeg.Items.AddRange(new object[] {
            "足付根(右)",
            "膝(右)",
            "足首(右)",
            "足指(右)"});
            this.lstRLeg.Location = new System.Drawing.Point(35, 203);
            this.lstRLeg.Name = "lstRLeg";
            this.lstRLeg.Size = new System.Drawing.Size(90, 60);
            this.lstRLeg.TabIndex = 6;
            this.lstRLeg.Leave += new System.EventHandler(this.lstRLeg_Leave);
            this.lstRLeg.MouseLeave += new System.EventHandler(this.lstRLeg_MouseLeave);
            // 
            // lstSpine
            // 
            this.lstSpine.CheckOnClick = true;
            this.lstSpine.FormattingEnabled = true;
            this.lstSpine.Items.AddRange(new object[] {
            "頭",
            "首",
            "背骨1",
            "背骨2",
            "背骨3",
            "背骨4",
            "中心",
            "骨盤"});
            this.lstSpine.Location = new System.Drawing.Point(168, 83);
            this.lstSpine.Name = "lstSpine";
            this.lstSpine.Size = new System.Drawing.Size(90, 116);
            this.lstSpine.TabIndex = 4;
            this.lstSpine.Leave += new System.EventHandler(this.lstSpine_Leave);
            this.lstSpine.MouseLeave += new System.EventHandler(this.lstSpine_MouseLeave);
            // 
            // lstLArm
            // 
            this.lstLArm.CheckOnClick = true;
            this.lstLArm.FormattingEnabled = true;
            this.lstLArm.Items.AddRange(new object[] {
            "胸(左)",
            "鎖骨(左)",
            "肩(左)",
            "肘(左)",
            "手首(左)",
            "手指(左)"});
            this.lstLArm.Location = new System.Drawing.Point(298, 82);
            this.lstLArm.Name = "lstLArm";
            this.lstLArm.Size = new System.Drawing.Size(90, 88);
            this.lstLArm.TabIndex = 5;
            this.lstLArm.Leave += new System.EventHandler(this.lstLArm_Leave);
            this.lstLArm.MouseLeave += new System.EventHandler(this.lstLArm_MouseLeave);
            // 
            // lstLLeg
            // 
            this.lstLLeg.CheckOnClick = true;
            this.lstLLeg.FormattingEnabled = true;
            this.lstLLeg.Items.AddRange(new object[] {
            "足付根(左)",
            "膝(左)",
            "足首(左)",
            "足指(左)"});
            this.lstLLeg.Location = new System.Drawing.Point(298, 203);
            this.lstLLeg.Name = "lstLLeg";
            this.lstLLeg.Size = new System.Drawing.Size(90, 60);
            this.lstLLeg.TabIndex = 7;
            this.lstLLeg.Leave += new System.EventHandler(this.lstLLeg_Leave);
            this.lstLLeg.MouseLeave += new System.EventHandler(this.lstLLeg_MouseLeave);
            // 
            // btnRLall
            // 
            this.btnRLall.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnRLall.Location = new System.Drawing.Point(89, 183);
            this.btnRLall.Margin = new System.Windows.Forms.Padding(1);
            this.btnRLall.Name = "btnRLall";
            this.btnRLall.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnRLall.Size = new System.Drawing.Size(18, 18);
            this.btnRLall.TabIndex = 43;
            this.btnRLall.TabStop = false;
            this.btnRLall.Text = "全";
            this.btnRLall.UseVisualStyleBackColor = true;
            this.btnRLall.Click += new System.EventHandler(this.btnRLall_Click);
            // 
            // btnRLclr
            // 
            this.btnRLclr.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnRLclr.Location = new System.Drawing.Point(109, 183);
            this.btnRLclr.Margin = new System.Windows.Forms.Padding(1);
            this.btnRLclr.Name = "btnRLclr";
            this.btnRLclr.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnRLclr.Size = new System.Drawing.Size(18, 18);
            this.btnRLclr.TabIndex = 42;
            this.btnRLclr.TabStop = false;
            this.btnRLclr.Text = "無";
            this.btnRLclr.UseVisualStyleBackColor = true;
            this.btnRLclr.Click += new System.EventHandler(this.btnRLclr_Click);
            // 
            // btnSPall
            // 
            this.btnSPall.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnSPall.Location = new System.Drawing.Point(220, 62);
            this.btnSPall.Margin = new System.Windows.Forms.Padding(1);
            this.btnSPall.Name = "btnSPall";
            this.btnSPall.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnSPall.Size = new System.Drawing.Size(18, 18);
            this.btnSPall.TabIndex = 45;
            this.btnSPall.TabStop = false;
            this.btnSPall.Text = "全";
            this.btnSPall.UseVisualStyleBackColor = true;
            this.btnSPall.Click += new System.EventHandler(this.btnSPall_Click);
            // 
            // btnSPclr
            // 
            this.btnSPclr.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnSPclr.Location = new System.Drawing.Point(240, 62);
            this.btnSPclr.Margin = new System.Windows.Forms.Padding(1);
            this.btnSPclr.Name = "btnSPclr";
            this.btnSPclr.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnSPclr.Size = new System.Drawing.Size(18, 18);
            this.btnSPclr.TabIndex = 44;
            this.btnSPclr.TabStop = false;
            this.btnSPclr.Text = "無";
            this.btnSPclr.UseVisualStyleBackColor = true;
            this.btnSPclr.Click += new System.EventHandler(this.btnSPclr_Click);
            // 
            // btnLAall
            // 
            this.btnLAall.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnLAall.Location = new System.Drawing.Point(350, 61);
            this.btnLAall.Margin = new System.Windows.Forms.Padding(1);
            this.btnLAall.Name = "btnLAall";
            this.btnLAall.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnLAall.Size = new System.Drawing.Size(18, 18);
            this.btnLAall.TabIndex = 47;
            this.btnLAall.TabStop = false;
            this.btnLAall.Text = "全";
            this.btnLAall.UseVisualStyleBackColor = true;
            this.btnLAall.Click += new System.EventHandler(this.btnLAall_Click);
            // 
            // btnLAclr
            // 
            this.btnLAclr.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnLAclr.Location = new System.Drawing.Point(370, 61);
            this.btnLAclr.Margin = new System.Windows.Forms.Padding(1);
            this.btnLAclr.Name = "btnLAclr";
            this.btnLAclr.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnLAclr.Size = new System.Drawing.Size(18, 18);
            this.btnLAclr.TabIndex = 46;
            this.btnLAclr.TabStop = false;
            this.btnLAclr.Text = "無";
            this.btnLAclr.UseVisualStyleBackColor = true;
            this.btnLAclr.Click += new System.EventHandler(this.btnLAclr_Click);
            // 
            // btnLLall
            // 
            this.btnLLall.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnLLall.Location = new System.Drawing.Point(350, 183);
            this.btnLLall.Margin = new System.Windows.Forms.Padding(1);
            this.btnLLall.Name = "btnLLall";
            this.btnLLall.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnLLall.Size = new System.Drawing.Size(18, 18);
            this.btnLLall.TabIndex = 49;
            this.btnLLall.TabStop = false;
            this.btnLLall.Text = "全";
            this.btnLLall.UseVisualStyleBackColor = true;
            this.btnLLall.Click += new System.EventHandler(this.btnLLall_Click);
            // 
            // btnLLclr
            // 
            this.btnLLclr.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.btnLLclr.Location = new System.Drawing.Point(370, 183);
            this.btnLLclr.Margin = new System.Windows.Forms.Padding(1);
            this.btnLLclr.Name = "btnLLclr";
            this.btnLLclr.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnLLclr.Size = new System.Drawing.Size(18, 18);
            this.btnLLclr.TabIndex = 48;
            this.btnLLclr.TabStop = false;
            this.btnLLclr.Text = "無";
            this.btnLLclr.UseVisualStyleBackColor = true;
            this.btnLLclr.Click += new System.EventHandler(this.btnLLclr_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 407);
            this.Controls.Add(this.btnLLall);
            this.Controls.Add(this.btnLLclr);
            this.Controls.Add(this.btnLAall);
            this.Controls.Add(this.btnLAclr);
            this.Controls.Add(this.btnSPall);
            this.Controls.Add(this.btnSPclr);
            this.Controls.Add(this.btnRLall);
            this.Controls.Add(this.btnRLclr);
            this.Controls.Add(this.lstLLeg);
            this.Controls.Add(this.lstLArm);
            this.Controls.Add(this.lstSpine);
            this.Controls.Add(this.lstRLeg);
            this.Controls.Add(this.btnRAall);
            this.Controls.Add(this.lstRArm);
            this.Controls.Add(this.btnRAclr);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAnmInfo);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.btnSplit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtChecked);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUnchecked);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(500, 450);
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "Form1";
            this.Text = "anmファイル分割";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnchecked;
        private System.Windows.Forms.TextBox txtChecked;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblAnmInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnRAclr;
        private System.Windows.Forms.CheckedListBox lstRArm;
        private System.Windows.Forms.Button btnRAall;
        private System.Windows.Forms.CheckedListBox lstRLeg;
        private System.Windows.Forms.CheckedListBox lstSpine;
        private System.Windows.Forms.CheckedListBox lstLArm;
        private System.Windows.Forms.CheckedListBox lstLLeg;
        private System.Windows.Forms.Button btnRLall;
        private System.Windows.Forms.Button btnRLclr;
        private System.Windows.Forms.Button btnSPall;
        private System.Windows.Forms.Button btnSPclr;
        private System.Windows.Forms.Button btnLAall;
        private System.Windows.Forms.Button btnLAclr;
        private System.Windows.Forms.Button btnLLall;
        private System.Windows.Forms.Button btnLLclr;
    }
}

