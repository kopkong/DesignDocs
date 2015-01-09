namespace WindowsFormsApplication1.Forms
{
    partial class RenameModel
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
            this.TB_SourcePath = new System.Windows.Forms.TextBox();
            this.TB_TargetPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_ID = new System.Windows.Forms.TextBox();
            this.RB_General = new System.Windows.Forms.RadioButton();
            this.RB_Soldier = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_DestPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_BatchDestPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_BatchTargetPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_BatchGeneralSource = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_BatchSoldierSource = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_SourcePath
            // 
            this.TB_SourcePath.Location = new System.Drawing.Point(26, 36);
            this.TB_SourcePath.Name = "TB_SourcePath";
            this.TB_SourcePath.Size = new System.Drawing.Size(225, 21);
            this.TB_SourcePath.TabIndex = 0;
            // 
            // TB_TargetPath
            // 
            this.TB_TargetPath.Location = new System.Drawing.Point(31, 95);
            this.TB_TargetPath.Name = "TB_TargetPath";
            this.TB_TargetPath.Size = new System.Drawing.Size(196, 21);
            this.TB_TargetPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "改名png输出路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "来源路径";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 39);
            this.button1.TabIndex = 4;
            this.button1.Text = "处理一个";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TB_ID
            // 
            this.TB_ID.Location = new System.Drawing.Point(54, 43);
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(142, 21);
            this.TB_ID.TabIndex = 5;
            // 
            // RB_General
            // 
            this.RB_General.AutoSize = true;
            this.RB_General.Location = new System.Drawing.Point(33, 21);
            this.RB_General.Name = "RB_General";
            this.RB_General.Size = new System.Drawing.Size(47, 16);
            this.RB_General.TabIndex = 7;
            this.RB_General.TabStop = true;
            this.RB_General.Text = "武将";
            this.RB_General.UseVisualStyleBackColor = true;
            this.RB_General.CheckedChanged += new System.EventHandler(this.RB_General_CheckedChanged);
            // 
            // RB_Soldier
            // 
            this.RB_Soldier.AutoSize = true;
            this.RB_Soldier.Location = new System.Drawing.Point(134, 21);
            this.RB_Soldier.Name = "RB_Soldier";
            this.RB_Soldier.Size = new System.Drawing.Size(47, 16);
            this.RB_Soldier.TabIndex = 8;
            this.RB_Soldier.TabStop = true;
            this.RB_Soldier.Text = "部队";
            this.RB_Soldier.UseVisualStyleBackColor = true;
            this.RB_Soldier.CheckedChanged += new System.EventHandler(this.RB_Soldier_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_DestPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.TB_TargetPath);
            this.groupBox1.Controls.Add(this.RB_Soldier);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RB_General);
            this.groupBox1.Controls.Add(this.TB_ID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 234);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出参数";
            // 
            // TB_DestPath
            // 
            this.TB_DestPath.Location = new System.Drawing.Point(29, 147);
            this.TB_DestPath.Name = "TB_DestPath";
            this.TB_DestPath.Size = new System.Drawing.Size(196, 21);
            this.TB_DestPath.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "plist输出路径";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 39);
            this.button2.TabIndex = 10;
            this.button2.Text = "处理大量";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.TB_BatchSoldierSource);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.TB_BatchGeneralSource);
            this.groupBox2.Controls.Add(this.TB_BatchDestPath);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TB_BatchTargetPath);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(338, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 311);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出参数";
            // 
            // TB_BatchDestPath
            // 
            this.TB_BatchDestPath.Location = new System.Drawing.Point(30, 224);
            this.TB_BatchDestPath.Name = "TB_BatchDestPath";
            this.TB_BatchDestPath.Size = new System.Drawing.Size(196, 21);
            this.TB_BatchDestPath.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "plist输出路径";
            // 
            // TB_BatchTargetPath
            // 
            this.TB_BatchTargetPath.Location = new System.Drawing.Point(32, 172);
            this.TB_BatchTargetPath.Name = "TB_BatchTargetPath";
            this.TB_BatchTargetPath.Size = new System.Drawing.Size(196, 21);
            this.TB_BatchTargetPath.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "改名png输出路径";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "武将源文件地址";
            // 
            // TB_BatchGeneralSource
            // 
            this.TB_BatchGeneralSource.Location = new System.Drawing.Point(25, 38);
            this.TB_BatchGeneralSource.Name = "TB_BatchGeneralSource";
            this.TB_BatchGeneralSource.Size = new System.Drawing.Size(225, 21);
            this.TB_BatchGeneralSource.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "士兵源文件地址";
            // 
            // TB_BatchSoldierSource
            // 
            this.TB_BatchSoldierSource.Location = new System.Drawing.Point(25, 97);
            this.TB_BatchSoldierSource.Name = "TB_BatchSoldierSource";
            this.TB_BatchSoldierSource.Size = new System.Drawing.Size(225, 21);
            this.TB_BatchSoldierSource.TabIndex = 13;
            // 
            // RenameModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 380);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_SourcePath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenameModel";
            this.ShowIcon = false;
            this.Text = "RenameModel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_SourcePath;
        private System.Windows.Forms.TextBox TB_TargetPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_ID;
        private System.Windows.Forms.RadioButton RB_General;
        private System.Windows.Forms.RadioButton RB_Soldier;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TB_DestPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TB_BatchSoldierSource;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TB_BatchGeneralSource;
        private System.Windows.Forms.TextBox TB_BatchDestPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_BatchTargetPath;
        private System.Windows.Forms.Label label6;
    }
}