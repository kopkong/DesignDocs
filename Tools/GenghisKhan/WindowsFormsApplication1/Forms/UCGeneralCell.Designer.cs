namespace WindowsFormsApplication1
{
    partial class UCGeneralCell
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BTN_General = new System.Windows.Forms.Button();
            this.BTN_Sell = new System.Windows.Forms.Button();
            this.BTN_Train = new System.Windows.Forms.Button();
            this.BTN_Promote = new System.Windows.Forms.Button();
            this.LB_Lv = new System.Windows.Forms.Label();
            this.LB_Rank = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTN_General
            // 
            this.BTN_General.Location = new System.Drawing.Point(25, 29);
            this.BTN_General.Name = "BTN_General";
            this.BTN_General.Size = new System.Drawing.Size(75, 73);
            this.BTN_General.TabIndex = 0;
            this.BTN_General.Text = "武将名字";
            this.BTN_General.UseVisualStyleBackColor = true;
            // 
            // BTN_Sell
            // 
            this.BTN_Sell.Location = new System.Drawing.Point(25, 123);
            this.BTN_Sell.Name = "BTN_Sell";
            this.BTN_Sell.Size = new System.Drawing.Size(75, 23);
            this.BTN_Sell.TabIndex = 1;
            this.BTN_Sell.Text = "解雇";
            this.BTN_Sell.UseVisualStyleBackColor = true;
            // 
            // BTN_Train
            // 
            this.BTN_Train.Location = new System.Drawing.Point(106, 123);
            this.BTN_Train.Name = "BTN_Train";
            this.BTN_Train.Size = new System.Drawing.Size(75, 23);
            this.BTN_Train.TabIndex = 2;
            this.BTN_Train.Text = "训练";
            this.BTN_Train.UseVisualStyleBackColor = true;
            // 
            // BTN_Promote
            // 
            this.BTN_Promote.Location = new System.Drawing.Point(187, 123);
            this.BTN_Promote.Name = "BTN_Promote";
            this.BTN_Promote.Size = new System.Drawing.Size(75, 23);
            this.BTN_Promote.TabIndex = 3;
            this.BTN_Promote.Text = "晋升";
            this.BTN_Promote.UseVisualStyleBackColor = true;
            // 
            // LB_Lv
            // 
            this.LB_Lv.AutoSize = true;
            this.LB_Lv.Location = new System.Drawing.Point(188, 40);
            this.LB_Lv.Name = "LB_Lv";
            this.LB_Lv.Size = new System.Drawing.Size(41, 12);
            this.LB_Lv.TabIndex = 4;
            this.LB_Lv.Text = "label1";
            // 
            // LB_Rank
            // 
            this.LB_Rank.AutoSize = true;
            this.LB_Rank.Location = new System.Drawing.Point(188, 78);
            this.LB_Rank.Name = "LB_Rank";
            this.LB_Rank.Size = new System.Drawing.Size(41, 12);
            this.LB_Rank.TabIndex = 5;
            this.LB_Rank.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "阶级";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "等级";
            // 
            // UCGeneralCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LB_Rank);
            this.Controls.Add(this.LB_Lv);
            this.Controls.Add(this.BTN_Promote);
            this.Controls.Add(this.BTN_Train);
            this.Controls.Add(this.BTN_Sell);
            this.Controls.Add(this.BTN_General);
            this.Name = "UCGeneralCell";
            this.Size = new System.Drawing.Size(301, 166);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_General;
        private System.Windows.Forms.Button BTN_Sell;
        private System.Windows.Forms.Button BTN_Train;
        private System.Windows.Forms.Button BTN_Promote;
        private System.Windows.Forms.Label LB_Lv;
        private System.Windows.Forms.Label LB_Rank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
