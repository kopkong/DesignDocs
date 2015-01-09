namespace WindowsFormsApplication1.Forms
{
    partial class LevelPlot
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TB_W1 = new System.Windows.Forms.TextBox();
            this.TB_ID1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TB_Words1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.TB_Words2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.TB_W2 = new System.Windows.Forms.TextBox();
            this.TB_ID2 = new System.Windows.Forms.TextBox();
            this.TB_Words3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.TB_W3 = new System.Windows.Forms.TextBox();
            this.TB_ID3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_Words1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.TB_W1);
            this.groupBox1.Controls.Add(this.TB_ID1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 270);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "战斗前对话";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 189);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "说的话";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 189);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "角色ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "添加一条新的";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TB_W1
            // 
            this.TB_W1.Location = new System.Drawing.Point(132, 204);
            this.TB_W1.Name = "TB_W1";
            this.TB_W1.Size = new System.Drawing.Size(295, 21);
            this.TB_W1.TabIndex = 2;
            // 
            // TB_ID1
            // 
            this.TB_ID1.Location = new System.Drawing.Point(20, 204);
            this.TB_ID1.Name = "TB_ID1";
            this.TB_ID1.Size = new System.Drawing.Size(92, 21);
            this.TB_ID1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_Words2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.TB_W2);
            this.groupBox2.Controls.Add(this.TB_ID2);
            this.groupBox2.Location = new System.Drawing.Point(476, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 270);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "战斗中对话";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TB_Words3);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.TB_W3);
            this.groupBox3.Controls.Add(this.TB_ID3);
            this.groupBox3.Location = new System.Drawing.Point(13, 303);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 300);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "战斗胜利对话";
            // 
            // TB_Words1
            // 
            this.TB_Words1.Location = new System.Drawing.Point(20, 21);
            this.TB_Words1.Multiline = true;
            this.TB_Words1.Name = "TB_Words1";
            this.TB_Words1.Size = new System.Drawing.Size(407, 159);
            this.TB_Words1.TabIndex = 7;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(476, 316);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(365, 154);
            this.button4.TabIndex = 3;
            this.button4.Text = "保存数据";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TB_Words2
            // 
            this.TB_Words2.Location = new System.Drawing.Point(20, 19);
            this.TB_Words2.Multiline = true;
            this.TB_Words2.Name = "TB_Words2";
            this.TB_Words2.Size = new System.Drawing.Size(407, 159);
            this.TB_Words2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 187);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "说的话";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 187);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "角色ID";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 229);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "添加一条新的";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TB_W2
            // 
            this.TB_W2.Location = new System.Drawing.Point(132, 202);
            this.TB_W2.Name = "TB_W2";
            this.TB_W2.Size = new System.Drawing.Size(295, 21);
            this.TB_W2.TabIndex = 9;
            // 
            // TB_ID2
            // 
            this.TB_ID2.Location = new System.Drawing.Point(20, 202);
            this.TB_ID2.Name = "TB_ID2";
            this.TB_ID2.Size = new System.Drawing.Size(92, 21);
            this.TB_ID2.TabIndex = 8;
            // 
            // TB_Words3
            // 
            this.TB_Words3.Location = new System.Drawing.Point(20, 34);
            this.TB_Words3.Multiline = true;
            this.TB_Words3.Name = "TB_Words3";
            this.TB_Words3.Size = new System.Drawing.Size(407, 159);
            this.TB_Words3.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 202);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "说的话";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 202);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "角色ID";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(20, 244);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "添加一条新的";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TB_W3
            // 
            this.TB_W3.Location = new System.Drawing.Point(132, 217);
            this.TB_W3.Name = "TB_W3";
            this.TB_W3.Size = new System.Drawing.Size(295, 21);
            this.TB_W3.TabIndex = 9;
            // 
            // TB_ID3
            // 
            this.TB_ID3.Location = new System.Drawing.Point(20, 217);
            this.TB_ID3.Name = "TB_ID3";
            this.TB_ID3.Size = new System.Drawing.Size(92, 21);
            this.TB_ID3.TabIndex = 8;
            // 
            // Plot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 627);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Plot";
            this.ShowIcon = false;
            this.Text = "剧情对话";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TB_W1;
        private System.Windows.Forms.TextBox TB_ID1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Words1;
        private System.Windows.Forms.TextBox TB_Words2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TB_W2;
        private System.Windows.Forms.TextBox TB_ID2;
        private System.Windows.Forms.TextBox TB_Words3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox TB_W3;
        private System.Windows.Forms.TextBox TB_ID3;
        private System.Windows.Forms.Button button4;
    }
}