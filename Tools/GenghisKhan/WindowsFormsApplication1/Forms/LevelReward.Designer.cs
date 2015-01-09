namespace WindowsFormsApplication1.Forms
{
    partial class LevelReward
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_Type1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_ID1 = new System.Windows.Forms.TextBox();
            this.TB_ID2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CB_Type2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.LB_Name1 = new System.Windows.Forms.Label();
            this.LB_Name2 = new System.Windows.Forms.Label();
            this.CB_Count1 = new System.Windows.Forms.NumericUpDown();
            this.CB_Count2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TB_Money = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TB_EXP = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CB_Count1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CB_Count2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CB_Count1);
            this.groupBox1.Controls.Add(this.LB_Name1);
            this.groupBox1.Controls.Add(this.TB_ID1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CB_Type1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(19, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "奖励1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CB_Count2);
            this.groupBox2.Controls.Add(this.LB_Name2);
            this.groupBox2.Controls.Add(this.TB_ID2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.CB_Type2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(19, 304);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 139);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "奖励2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数量";
            // 
            // CB_Type1
            // 
            this.CB_Type1.FormattingEnabled = true;
            this.CB_Type1.Location = new System.Drawing.Point(64, 27);
            this.CB_Type1.Name = "CB_Type1";
            this.CB_Type1.Size = new System.Drawing.Size(121, 20);
            this.CB_Type1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "ID";
            // 
            // TB_ID1
            // 
            this.TB_ID1.Location = new System.Drawing.Point(64, 61);
            this.TB_ID1.Name = "TB_ID1";
            this.TB_ID1.Size = new System.Drawing.Size(121, 21);
            this.TB_ID1.TabIndex = 7;
            this.TB_ID1.TextChanged += new System.EventHandler(this.TB_ID1_TextChanged);
            // 
            // TB_ID2
            // 
            this.TB_ID2.Location = new System.Drawing.Point(64, 67);
            this.TB_ID2.Name = "TB_ID2";
            this.TB_ID2.Size = new System.Drawing.Size(121, 21);
            this.TB_ID2.TabIndex = 13;
            this.TB_ID2.TextChanged += new System.EventHandler(this.TB_ID2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "ID";
            // 
            // CB_Type2
            // 
            this.CB_Type2.FormattingEnabled = true;
            this.CB_Type2.Location = new System.Drawing.Point(64, 33);
            this.CB_Type2.Name = "CB_Type2";
            this.CB_Type2.Size = new System.Drawing.Size(121, 20);
            this.CB_Type2.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "数量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "类型";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LB_Name1
            // 
            this.LB_Name1.AutoSize = true;
            this.LB_Name1.Location = new System.Drawing.Point(192, 69);
            this.LB_Name1.Name = "LB_Name1";
            this.LB_Name1.Size = new System.Drawing.Size(29, 12);
            this.LB_Name1.TabIndex = 8;
            this.LB_Name1.Text = "空的";
            // 
            // LB_Name2
            // 
            this.LB_Name2.AutoSize = true;
            this.LB_Name2.Location = new System.Drawing.Point(192, 70);
            this.LB_Name2.Name = "LB_Name2";
            this.LB_Name2.Size = new System.Drawing.Size(29, 12);
            this.LB_Name2.TabIndex = 14;
            this.LB_Name2.Text = "空的";
            // 
            // CB_Count1
            // 
            this.CB_Count1.Location = new System.Drawing.Point(64, 94);
            this.CB_Count1.Name = "CB_Count1";
            this.CB_Count1.Size = new System.Drawing.Size(120, 21);
            this.CB_Count1.TabIndex = 9;
            // 
            // CB_Count2
            // 
            this.CB_Count2.Location = new System.Drawing.Point(65, 98);
            this.CB_Count2.Name = "CB_Count2";
            this.CB_Count2.Size = new System.Drawing.Size(120, 21);
            this.CB_Count2.TabIndex = 15;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TB_EXP);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.TB_Money);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(19, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 108);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "基础";
            // 
            // TB_Money
            // 
            this.TB_Money.Location = new System.Drawing.Point(76, 29);
            this.TB_Money.Name = "TB_Money";
            this.TB_Money.Size = new System.Drawing.Size(121, 21);
            this.TB_Money.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "金币";
            // 
            // TB_EXP
            // 
            this.TB_EXP.Location = new System.Drawing.Point(77, 66);
            this.TB_EXP.Name = "TB_EXP";
            this.TB_EXP.Size = new System.Drawing.Size(121, 21);
            this.TB_EXP.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "武将经验";
            // 
            // LevelReward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 513);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LevelReward";
            this.ShowIcon = false;
            this.Text = "LevelReward";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CB_Count1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CB_Count2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CB_Type1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_ID1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_ID2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CB_Type2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LB_Name1;
        private System.Windows.Forms.Label LB_Name2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown CB_Count1;
        private System.Windows.Forms.NumericUpDown CB_Count2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TB_EXP;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TB_Money;
        private System.Windows.Forms.Label label9;
    }
}