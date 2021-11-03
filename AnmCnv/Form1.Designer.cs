namespace AnmCnv {
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
            if (disposing && (components != null)) {
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
            this.btnCnv = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.chkDelay = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.chkSpeed = new System.Windows.Forms.CheckBox();
            this.chkGender = new System.Windows.Forms.CheckBox();
            this.lblAnmInfo = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkMirror = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCnv
            // 
            this.btnCnv.Location = new System.Drawing.Point(151, 333);
            this.btnCnv.Name = "btnCnv";
            this.btnCnv.Size = new System.Drawing.Size(135, 23);
            this.btnCnv.TabIndex = 8;
            this.btnCnv.Text = "変換";
            this.btnCnv.UseVisualStyleBackColor = true;
            this.btnCnv.Click += new System.EventHandler(this.btnCnv_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(245, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 12);
            this.label9.TabIndex = 47;
            this.label9.Text = "ms を加算";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 12);
            this.label8.TabIndex = 46;
            this.label8.Text = "全フレーム時刻に";
            // 
            // txtDelay
            // 
            this.txtDelay.Enabled = false;
            this.txtDelay.Location = new System.Drawing.Point(173, 185);
            this.txtDelay.MaxLength = 9;
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(66, 19);
            this.txtDelay.TabIndex = 6;
            this.txtDelay.Text = "0";
            this.txtDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkDelay
            // 
            this.chkDelay.AutoSize = true;
            this.chkDelay.Location = new System.Drawing.Point(45, 165);
            this.chkDelay.Name = "chkDelay";
            this.chkDelay.Size = new System.Drawing.Size(116, 16);
            this.chkDelay.TabIndex = 5;
            this.chkDelay.Text = "モーション開始遅延";
            this.chkDelay.UseVisualStyleBackColor = true;
            this.chkDelay.CheckedChanged += new System.EventHandler(this.chkDelay_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(245, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 12);
            this.label6.TabIndex = 45;
            this.label6.Text = "ms となるようにモーション速度を変更";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 12);
            this.label5.TabIndex = 44;
            this.label5.Text = "最終フレーム時刻が";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Enabled = false;
            this.txtSpeed.Location = new System.Drawing.Point(173, 131);
            this.txtSpeed.MaxLength = 9;
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(66, 19);
            this.txtSpeed.TabIndex = 4;
            this.txtSpeed.Text = "0";
            this.txtSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkSpeed
            // 
            this.chkSpeed.AutoSize = true;
            this.chkSpeed.Location = new System.Drawing.Point(45, 109);
            this.chkSpeed.Name = "chkSpeed";
            this.chkSpeed.Size = new System.Drawing.Size(116, 16);
            this.chkSpeed.TabIndex = 3;
            this.chkSpeed.Text = "モーション速度変更";
            this.chkSpeed.UseVisualStyleBackColor = true;
            this.chkSpeed.CheckedChanged += new System.EventHandler(this.chkSpeed_CheckedChanged);
            // 
            // chkGender
            // 
            this.chkGender.AutoSize = true;
            this.chkGender.Location = new System.Drawing.Point(45, 76);
            this.chkGender.Name = "chkGender";
            this.chkGender.Size = new System.Drawing.Size(324, 16);
            this.chkGender.TabIndex = 2;
            this.chkGender.Text = "男性用モーションを女性用に、女性用モーションを男性用に変換";
            this.chkGender.UseVisualStyleBackColor = true;
            // 
            // lblAnmInfo
            // 
            this.lblAnmInfo.AutoSize = true;
            this.lblAnmInfo.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.lblAnmInfo.Location = new System.Drawing.Point(268, 44);
            this.lblAnmInfo.Name = "lblAnmInfo";
            this.lblAnmInfo.Size = new System.Drawing.Size(0, 11);
            this.lblAnmInfo.TabIndex = 43;
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(428, 20);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(39, 22);
            this.btnInput.TabIndex = 1;
            this.btnInput.Text = "選択";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "入力ファイル";
            // 
            // txtInput
            // 
            this.txtInput.Enabled = false;
            this.txtInput.Location = new System.Drawing.Point(90, 22);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(332, 19);
            this.txtInput.TabIndex = 999;
            this.txtInput.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 217);
            this.label1.MaximumSize = new System.Drawing.Size(350, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 36);
            this.label1.TabIndex = 1000;
            this.label1.Text = "「モーション速度変更」と「モーション開始遅延」を同時に行う場合、速度変更後に遅延分を加算します。開始遅延時間には、速度変更による倍率はかかりません。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 1001;
            this.label2.Text = "※";
            // 
            // chkMirror
            // 
            this.chkMirror.AutoSize = true;
            this.chkMirror.Location = new System.Drawing.Point(45, 279);
            this.chkMirror.Name = "chkMirror";
            this.chkMirror.Size = new System.Drawing.Size(154, 16);
            this.chkMirror.TabIndex = 1007;
            this.chkMirror.Text = "モーションの左右を反転する";
            this.chkMirror.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.chkMirror);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCnv);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDelay);
            this.Controls.Add(this.chkDelay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.chkSpeed);
            this.Controls.Add(this.chkGender);
            this.Controls.Add(this.lblAnmInfo);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInput);
            this.MaximumSize = new System.Drawing.Size(500, 450);
            this.MinimumSize = new System.Drawing.Size(500, 450);
            this.Name = "Form1";
            this.Text = "anmファイル変換";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCnv;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.CheckBox chkDelay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.CheckBox chkSpeed;
        private System.Windows.Forms.CheckBox chkGender;
        private System.Windows.Forms.Label lblAnmInfo;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkMirror;
    }
}

