namespace WindowsFormsApplication1.Forms
{
    partial class BattleSimu
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
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CB_SimLevel60 = new System.Windows.Forms.CheckBox();
            this.CB_SimLevel20 = new System.Windows.Forms.CheckBox();
            this.CB_SimLevel40 = new System.Windows.Forms.CheckBox();
            this.CB_SimLevel1 = new System.Windows.Forms.CheckBox();
            this.CB_DetailInfo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NUD_TestCount = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_TestCount)).BeginInit();
            this.SuspendLayout();
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(14, 150);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(110, 42);
            this.button9.TabIndex = 15;
            this.button9.Text = "属性基础测试";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.CB_SimLevel60);
            this.groupBox2.Controls.Add(this.CB_SimLevel20);
            this.groupBox2.Controls.Add(this.CB_SimLevel40);
            this.groupBox2.Controls.Add(this.CB_SimLevel1);
            this.groupBox2.Controls.Add(this.CB_DetailInfo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.NUD_TestCount);
            this.groupBox2.Location = new System.Drawing.Point(12, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(984, 103);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "战斗模拟";
            // 
            // CB_SimLevel60
            // 
            this.CB_SimLevel60.AutoSize = true;
            this.CB_SimLevel60.Location = new System.Drawing.Point(290, 53);
            this.CB_SimLevel60.Name = "CB_SimLevel60";
            this.CB_SimLevel60.Size = new System.Drawing.Size(48, 16);
            this.CB_SimLevel60.TabIndex = 27;
            this.CB_SimLevel60.Text = "60级";
            this.CB_SimLevel60.UseVisualStyleBackColor = true;
            // 
            // CB_SimLevel20
            // 
            this.CB_SimLevel20.AutoSize = true;
            this.CB_SimLevel20.Location = new System.Drawing.Point(290, 21);
            this.CB_SimLevel20.Name = "CB_SimLevel20";
            this.CB_SimLevel20.Size = new System.Drawing.Size(48, 16);
            this.CB_SimLevel20.TabIndex = 26;
            this.CB_SimLevel20.Text = "20级";
            this.CB_SimLevel20.UseVisualStyleBackColor = true;
            // 
            // CB_SimLevel40
            // 
            this.CB_SimLevel40.AutoSize = true;
            this.CB_SimLevel40.Location = new System.Drawing.Point(206, 53);
            this.CB_SimLevel40.Name = "CB_SimLevel40";
            this.CB_SimLevel40.Size = new System.Drawing.Size(48, 16);
            this.CB_SimLevel40.TabIndex = 25;
            this.CB_SimLevel40.Text = "40级";
            this.CB_SimLevel40.UseVisualStyleBackColor = true;
            // 
            // CB_SimLevel1
            // 
            this.CB_SimLevel1.AutoSize = true;
            this.CB_SimLevel1.Location = new System.Drawing.Point(206, 21);
            this.CB_SimLevel1.Name = "CB_SimLevel1";
            this.CB_SimLevel1.Size = new System.Drawing.Size(42, 16);
            this.CB_SimLevel1.TabIndex = 24;
            this.CB_SimLevel1.Text = "1级";
            this.CB_SimLevel1.UseVisualStyleBackColor = true;
            // 
            // CB_DetailInfo
            // 
            this.CB_DetailInfo.AutoSize = true;
            this.CB_DetailInfo.Location = new System.Drawing.Point(102, 56);
            this.CB_DetailInfo.Name = "CB_DetailInfo";
            this.CB_DetailInfo.Size = new System.Drawing.Size(72, 16);
            this.CB_DetailInfo.TabIndex = 23;
            this.CB_DetailInfo.Text = "详细信息";
            this.CB_DetailInfo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "单个测试次数";
            // 
            // NUD_TestCount
            // 
            this.NUD_TestCount.Location = new System.Drawing.Point(17, 52);
            this.NUD_TestCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NUD_TestCount.Name = "NUD_TestCount";
            this.NUD_TestCount.Size = new System.Drawing.Size(78, 21);
            this.NUD_TestCount.TabIndex = 20;
            this.NUD_TestCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(246, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 42);
            this.button2.TabIndex = 19;
            this.button2.Text = "真实武将带士兵测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(12, 214);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(973, 424);
            this.textBox1.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 42);
            this.button1.TabIndex = 22;
            this.button1.Text = "兵种基础测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(809, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 66);
            this.button3.TabIndex = 23;
            this.button3.Text = "生成模拟用配置";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BattleSimu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 671);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button2);
            this.MaximizeBox = false;
            this.Name = "BattleSimu";
            this.ShowIcon = false;
            this.Text = "战斗测试工具";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_TestCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUD_TestCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox CB_DetailInfo;
        private System.Windows.Forms.CheckBox CB_SimLevel60;
        private System.Windows.Forms.CheckBox CB_SimLevel20;
        private System.Windows.Forms.CheckBox CB_SimLevel40;
        private System.Windows.Forms.CheckBox CB_SimLevel1;
        private System.Windows.Forms.Button button3;


    }
}