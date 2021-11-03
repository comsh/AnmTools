namespace AnmSlice {
    partial class form1 {
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
            this.lstTimes = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStime = new System.Windows.Forms.TextBox();
            this.txtEtime = new System.Windows.Forms.TextBox();
            this.btnSlice = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.txtLoop = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstTimes
            // 
            this.lstTimes.ColumnWidth = 65;
            this.lstTimes.FormatString = "00000000";
            this.lstTimes.FormattingEnabled = true;
            this.lstTimes.ItemHeight = 12;
            this.lstTimes.Location = new System.Drawing.Point(15, 61);
            this.lstTimes.MultiColumn = true;
            this.lstTimes.Name = "lstTimes";
            this.lstTimes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTimes.Size = new System.Drawing.Size(140, 280);
            this.lstTimes.TabIndex = 0;
            this.lstTimes.TabStop = false;
            this.lstTimes.SelectedIndexChanged += new System.EventHandler(this.lstTimes_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "選択";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // txtInput
            // 
            this.txtInput.CausesValidation = false;
            this.txtInput.Location = new System.Drawing.Point(69, 9);
            this.txtInput.Name = "txtInput";
            this.txtInput.ReadOnly = true;
            this.txtInput.Size = new System.Drawing.Size(253, 19);
            this.txtInput.TabIndex = 2;
            this.txtInput.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ファイル名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "キーフレームの時刻";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "切取開始時刻";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "切取終了時刻";
            // 
            // txtStime
            // 
            this.txtStime.Location = new System.Drawing.Point(256, 94);
            this.txtStime.Name = "txtStime";
            this.txtStime.Size = new System.Drawing.Size(90, 19);
            this.txtStime.TabIndex = 2;
            this.txtStime.Text = "0";
            this.txtStime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEtime
            // 
            this.txtEtime.Location = new System.Drawing.Point(256, 136);
            this.txtEtime.Name = "txtEtime";
            this.txtEtime.Size = new System.Drawing.Size(90, 19);
            this.txtEtime.TabIndex = 3;
            this.txtEtime.Text = "0";
            this.txtEtime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSlice
            // 
            this.btnSlice.Location = new System.Drawing.Point(221, 292);
            this.btnSlice.Name = "btnSlice";
            this.btnSlice.Size = new System.Drawing.Size(90, 23);
            this.btnSlice.TabIndex = 4;
            this.btnSlice.Text = "切り取り";
            this.btnSlice.UseVisualStyleBackColor = true;
            this.btnSlice.Click += new System.EventHandler(this.btnSlice_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(350, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "ms";
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Location = new System.Drawing.Point(175, 194);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(80, 16);
            this.chkLoop.TabIndex = 15;
            this.chkLoop.Text = "ループさせる";
            this.chkLoop.UseVisualStyleBackColor = true;
            this.chkLoop.CheckedChanged += new System.EventHandler(this.chkLoop_CheckedChanged);
            // 
            // txtLoop
            // 
            this.txtLoop.Enabled = false;
            this.txtLoop.Location = new System.Drawing.Point(278, 212);
            this.txtLoop.Name = "txtLoop";
            this.txtLoop.Size = new System.Drawing.Size(68, 19);
            this.txtLoop.TabIndex = 16;
            this.txtLoop.Text = "300";
            this.txtLoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(188, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "最終フレームから";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(189, 234);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "かけて最初のフレームに遷移";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(350, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "ms";
            // 
            // form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLoop);
            this.Controls.Add(this.chkLoop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSlice);
            this.Controls.Add(this.txtEtime);
            this.Controls.Add(this.txtStime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstTimes);
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "form1";
            this.Text = "anmファイル時間範囲切りとり";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstTimes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStime;
        private System.Windows.Forms.TextBox txtEtime;
        private System.Windows.Forms.Button btnSlice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.TextBox txtLoop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

