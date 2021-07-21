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
            this.lstRegion = new System.Windows.Forms.CheckedListBox();
            this.lblAnmInfo = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUnchecked = new System.Windows.Forms.TextBox();
            this.txtChecked = new System.Windows.Forms.TextBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(263, 261);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(131, 23);
            this.btnSplit.TabIndex = 6;
            this.btnSplit.Text = "分割";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // lstRegion
            // 
            this.lstRegion.CheckOnClick = true;
            this.lstRegion.FormattingEnabled = true;
            this.lstRegion.Items.AddRange(new object[] {
            "中心",
            "骨盤",
            "足付根(左)",
            "膝(左)",
            "足首(左)",
            "足指(左)",
            "足付根(右)",
            "膝(右)",
            "足首(右)",
            "足指(右)",
            "背骨4",
            "背骨3",
            "背骨2",
            "背骨1",
            "首",
            "頭",
            "胸(左)",
            "鎖骨(左)",
            "肩(左)",
            "肘(左)",
            "手首(左)",
            "手指(左)",
            "胸(右)",
            "鎖骨(右)",
            "肩(右)",
            "肘(右)",
            "手首(右)",
            "手指(右)"});
            this.lstRegion.Location = new System.Drawing.Point(17, 68);
            this.lstRegion.Name = "lstRegion";
            this.lstRegion.Size = new System.Drawing.Size(146, 326);
            this.lstRegion.TabIndex = 3;
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
            this.btnInput.Location = new System.Drawing.Point(422, 20);
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
            this.label7.Location = new System.Drawing.Point(202, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "非チック部位出力ファイル";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "チェック部位出力ファイル";
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
            this.txtUnchecked.Location = new System.Drawing.Point(218, 187);
            this.txtUnchecked.Name = "txtUnchecked";
            this.txtUnchecked.Size = new System.Drawing.Size(236, 19);
            this.txtUnchecked.TabIndex = 5;
            // 
            // txtChecked
            // 
            this.txtChecked.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtChecked.Location = new System.Drawing.Point(218, 108);
            this.txtChecked.Name = "txtChecked";
            this.txtChecked.Size = new System.Drawing.Size(236, 19);
            this.txtChecked.TabIndex = 4;
            // 
            // txtInput
            // 
            this.txtInput.Enabled = false;
            this.txtInput.Location = new System.Drawing.Point(84, 22);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(332, 19);
            this.txtInput.TabIndex = 1;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 407);
            this.Controls.Add(this.lstRegion);
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
            this.MinimumSize = new System.Drawing.Size(500, 450);
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
        private System.Windows.Forms.CheckedListBox lstRegion;
    }
}

