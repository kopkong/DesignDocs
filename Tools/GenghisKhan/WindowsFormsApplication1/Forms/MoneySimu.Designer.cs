namespace WindowsFormsApplication1.Forms
{
    partial class MoneySimu
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
            this.button1 = new System.Windows.Forms.Button();
            this.CB_SoldierUp = new System.Windows.Forms.CheckBox();
            this.CB_ArmorUp = new System.Windows.Forms.CheckBox();
            this.CB_SkillUp = new System.Windows.Forms.CheckBox();
            this.CB_SoldierCount = new System.Windows.Forms.CheckBox();
            this.CB_AngelSKill = new System.Windows.Forms.CheckBox();
            this.TB_Output = new System.Windows.Forms.TextBox();
            this.NUD_Level = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Level)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "查看";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CB_SoldierUp
            // 
            this.CB_SoldierUp.AutoSize = true;
            this.CB_SoldierUp.Location = new System.Drawing.Point(28, 30);
            this.CB_SoldierUp.Name = "CB_SoldierUp";
            this.CB_SoldierUp.Size = new System.Drawing.Size(72, 16);
            this.CB_SoldierUp.TabIndex = 1;
            this.CB_SoldierUp.Text = "部队升级";
            this.CB_SoldierUp.UseVisualStyleBackColor = true;
            // 
            // CB_ArmorUp
            // 
            this.CB_ArmorUp.AutoSize = true;
            this.CB_ArmorUp.Location = new System.Drawing.Point(112, 30);
            this.CB_ArmorUp.Name = "CB_ArmorUp";
            this.CB_ArmorUp.Size = new System.Drawing.Size(72, 16);
            this.CB_ArmorUp.TabIndex = 2;
            this.CB_ArmorUp.Text = "装备升级";
            this.CB_ArmorUp.UseVisualStyleBackColor = true;
            // 
            // CB_SkillUp
            // 
            this.CB_SkillUp.AutoSize = true;
            this.CB_SkillUp.Location = new System.Drawing.Point(196, 30);
            this.CB_SkillUp.Name = "CB_SkillUp";
            this.CB_SkillUp.Size = new System.Drawing.Size(72, 16);
            this.CB_SkillUp.TabIndex = 3;
            this.CB_SkillUp.Text = "技能升级";
            this.CB_SkillUp.UseVisualStyleBackColor = true;
            // 
            // CB_SoldierCount
            // 
            this.CB_SoldierCount.AutoSize = true;
            this.CB_SoldierCount.Location = new System.Drawing.Point(280, 30);
            this.CB_SoldierCount.Name = "CB_SoldierCount";
            this.CB_SoldierCount.Size = new System.Drawing.Size(96, 16);
            this.CB_SoldierCount.TabIndex = 4;
            this.CB_SoldierCount.Text = "部队人数扩充";
            this.CB_SoldierCount.UseVisualStyleBackColor = true;
            // 
            // CB_AngelSKill
            // 
            this.CB_AngelSKill.AutoSize = true;
            this.CB_AngelSKill.Location = new System.Drawing.Point(382, 30);
            this.CB_AngelSKill.Name = "CB_AngelSKill";
            this.CB_AngelSKill.Size = new System.Drawing.Size(108, 16);
            this.CB_AngelSKill.TabIndex = 5;
            this.CB_AngelSKill.Text = "守护神技能升级";
            this.CB_AngelSKill.UseVisualStyleBackColor = true;
            // 
            // TB_Output
            // 
            this.TB_Output.Location = new System.Drawing.Point(28, 168);
            this.TB_Output.Multiline = true;
            this.TB_Output.Name = "TB_Output";
            this.TB_Output.Size = new System.Drawing.Size(462, 293);
            this.TB_Output.TabIndex = 6;
            // 
            // NUD_Level
            // 
            this.NUD_Level.Location = new System.Drawing.Point(9, 38);
            this.NUD_Level.Name = "NUD_Level";
            this.NUD_Level.Size = new System.Drawing.Size(120, 21);
            this.NUD_Level.TabIndex = 7;
            this.NUD_Level.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "主角等级";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.NUD_Level);
            this.groupBox1.Location = new System.Drawing.Point(28, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 73);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(270, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 42);
            this.button2.TabIndex = 9;
            this.button2.Text = "按天查看";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MoneySimu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 479);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TB_Output);
            this.Controls.Add(this.CB_AngelSKill);
            this.Controls.Add(this.CB_SoldierCount);
            this.Controls.Add(this.CB_SkillUp);
            this.Controls.Add(this.CB_ArmorUp);
            this.Controls.Add(this.CB_SoldierUp);
            this.MaximizeBox = false;
            this.Name = "MoneySimu";
            this.ShowIcon = false;
            this.Text = "MoneySimu";
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Level)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox CB_SoldierUp;
        private System.Windows.Forms.CheckBox CB_ArmorUp;
        private System.Windows.Forms.CheckBox CB_SkillUp;
        private System.Windows.Forms.CheckBox CB_SoldierCount;
        private System.Windows.Forms.CheckBox CB_AngelSKill;
        private System.Windows.Forms.TextBox TB_Output;
        private System.Windows.Forms.NumericUpDown NUD_Level;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
    }
}