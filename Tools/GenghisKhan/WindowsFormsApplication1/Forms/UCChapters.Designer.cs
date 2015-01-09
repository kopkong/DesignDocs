namespace WindowsFormsApplication1.Forms
{
    partial class UCChapters
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.配置怪物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置精英关ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置剧情对话ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置奖励ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置精英关奖励ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TB_LevelName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_EliteEnemyAngel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_EnemyAngel = new System.Windows.Forms.TextBox();
            this.CB_StrongEnemy = new System.Windows.Forms.CheckBox();
            this.LB_RefSquads2 = new System.Windows.Forms.Label();
            this.LB_RefSquads1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_EliteEnemy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_Enemy = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TB_RefEliteLevel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_RefLevel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(14, 333);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(246, 263);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "章节ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 66;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "章节名字";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column3,
            this.Column4,
            this.Column8,
            this.Column9,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView2.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView2.Location = new System.Drawing.Point(14, 34);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(794, 290);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "关卡编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 61;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "关卡名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 61;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "普通/参考战力";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "普通/参考人数";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "精英/参考战力";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 78;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "精英/参考人数";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 78;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "剧情";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 51;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "普通关奖励";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 72;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "精英关奖励";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 72;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.配置怪物ToolStripMenuItem,
            this.配置精英关ToolStripMenuItem,
            this.配置剧情对话ToolStripMenuItem,
            this.配置奖励ToolStripMenuItem,
            this.配置精英关奖励ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 114);
            // 
            // 配置怪物ToolStripMenuItem
            // 
            this.配置怪物ToolStripMenuItem.Name = "配置怪物ToolStripMenuItem";
            this.配置怪物ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置怪物ToolStripMenuItem.Text = "配置普通关";
            this.配置怪物ToolStripMenuItem.Click += new System.EventHandler(this.配置怪物ToolStripMenuItem_Click);
            // 
            // 配置精英关ToolStripMenuItem
            // 
            this.配置精英关ToolStripMenuItem.Name = "配置精英关ToolStripMenuItem";
            this.配置精英关ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置精英关ToolStripMenuItem.Text = "配置精英关";
            this.配置精英关ToolStripMenuItem.Click += new System.EventHandler(this.配置精英关ToolStripMenuItem_Click);
            // 
            // 配置剧情对话ToolStripMenuItem
            // 
            this.配置剧情对话ToolStripMenuItem.Name = "配置剧情对话ToolStripMenuItem";
            this.配置剧情对话ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置剧情对话ToolStripMenuItem.Text = "配置剧情对话";
            this.配置剧情对话ToolStripMenuItem.Click += new System.EventHandler(this.配置剧情对话ToolStripMenuItem_Click);
            // 
            // 配置奖励ToolStripMenuItem
            // 
            this.配置奖励ToolStripMenuItem.Name = "配置奖励ToolStripMenuItem";
            this.配置奖励ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置奖励ToolStripMenuItem.Text = "配置普通关奖励";
            this.配置奖励ToolStripMenuItem.Click += new System.EventHandler(this.配置奖励ToolStripMenuItem_Click);
            // 
            // 配置精英关奖励ToolStripMenuItem
            // 
            this.配置精英关奖励ToolStripMenuItem.Name = "配置精英关奖励ToolStripMenuItem";
            this.配置精英关奖励ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.配置精英关奖励ToolStripMenuItem.Text = "配置精英关奖励";
            this.配置精英关奖励ToolStripMenuItem.Click += new System.EventHandler(this.配置精英关奖励ToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(266, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(858, 269);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关卡详情";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel2.Location = new System.Drawing.Point(443, 20);
            this.panel2.Name = "panel2";
            this.panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel2.Size = new System.Drawing.Size(400, 247);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.Location = new System.Drawing.Point(18, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 247);
            this.panel1.TabIndex = 0;
            // 
            // TB_LevelName
            // 
            this.TB_LevelName.Location = new System.Drawing.Point(68, 38);
            this.TB_LevelName.Name = "TB_LevelName";
            this.TB_LevelName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_LevelName.Size = new System.Drawing.Size(100, 21);
            this.TB_LevelName.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TB_EliteEnemyAngel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TB_EnemyAngel);
            this.groupBox2.Controls.Add(this.CB_StrongEnemy);
            this.groupBox2.Controls.Add(this.LB_RefSquads2);
            this.groupBox2.Controls.Add(this.LB_RefSquads1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.TB_EliteEnemy);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.TB_Enemy);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.TB_RefEliteLevel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.TB_RefLevel);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TB_LevelName);
            this.groupBox2.Location = new System.Drawing.Point(814, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 295);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "关卡信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "精天使";
            // 
            // TB_EliteEnemyAngel
            // 
            this.TB_EliteEnemyAngel.Location = new System.Drawing.Point(68, 201);
            this.TB_EliteEnemyAngel.Name = "TB_EliteEnemyAngel";
            this.TB_EliteEnemyAngel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_EliteEnemyAngel.Size = new System.Drawing.Size(236, 21);
            this.TB_EliteEnemyAngel.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "普天使";
            // 
            // TB_EnemyAngel
            // 
            this.TB_EnemyAngel.Location = new System.Drawing.Point(68, 174);
            this.TB_EnemyAngel.Name = "TB_EnemyAngel";
            this.TB_EnemyAngel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_EnemyAngel.Size = new System.Drawing.Size(236, 21);
            this.TB_EnemyAngel.TabIndex = 21;
            // 
            // CB_StrongEnemy
            // 
            this.CB_StrongEnemy.AutoSize = true;
            this.CB_StrongEnemy.Location = new System.Drawing.Point(195, 40);
            this.CB_StrongEnemy.Name = "CB_StrongEnemy";
            this.CB_StrongEnemy.Size = new System.Drawing.Size(48, 16);
            this.CB_StrongEnemy.TabIndex = 20;
            this.CB_StrongEnemy.Text = "强敌";
            this.CB_StrongEnemy.UseVisualStyleBackColor = true;
            // 
            // LB_RefSquads2
            // 
            this.LB_RefSquads2.AutoSize = true;
            this.LB_RefSquads2.Location = new System.Drawing.Point(174, 95);
            this.LB_RefSquads2.Name = "LB_RefSquads2";
            this.LB_RefSquads2.Size = new System.Drawing.Size(53, 12);
            this.LB_RefSquads2.TabIndex = 17;
            this.LB_RefSquads2.Text = "参考人数";
            // 
            // LB_RefSquads1
            // 
            this.LB_RefSquads1.AutoSize = true;
            this.LB_RefSquads1.Location = new System.Drawing.Point(174, 68);
            this.LB_RefSquads1.Name = "LB_RefSquads1";
            this.LB_RefSquads1.Size = new System.Drawing.Size(53, 12);
            this.LB_RefSquads1.TabIndex = 16;
            this.LB_RefSquads1.Text = "参考人数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "精怪";
            // 
            // TB_EliteEnemy
            // 
            this.TB_EliteEnemy.Location = new System.Drawing.Point(68, 146);
            this.TB_EliteEnemy.Name = "TB_EliteEnemy";
            this.TB_EliteEnemy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_EliteEnemy.Size = new System.Drawing.Size(236, 21);
            this.TB_EliteEnemy.TabIndex = 14;
            this.TB_EliteEnemy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_EliteEnemy_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "普怪";
            // 
            // TB_Enemy
            // 
            this.TB_Enemy.Location = new System.Drawing.Point(68, 119);
            this.TB_Enemy.Name = "TB_Enemy";
            this.TB_Enemy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_Enemy.Size = new System.Drawing.Size(236, 21);
            this.TB_Enemy.TabIndex = 12;
            this.TB_Enemy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_Enemy_MouseDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "精参";
            // 
            // TB_RefEliteLevel
            // 
            this.TB_RefEliteLevel.Location = new System.Drawing.Point(68, 92);
            this.TB_RefEliteLevel.Name = "TB_RefEliteLevel";
            this.TB_RefEliteLevel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_RefEliteLevel.Size = new System.Drawing.Size(100, 21);
            this.TB_RefEliteLevel.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "普参";
            // 
            // TB_RefLevel
            // 
            this.TB_RefLevel.Location = new System.Drawing.Point(68, 65);
            this.TB_RefLevel.Name = "TB_RefLevel";
            this.TB_RefLevel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TB_RefLevel.Size = new System.Drawing.Size(100, 21);
            this.TB_RefLevel.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "名字";
            // 
            // UCChapters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UCChapters";
            this.Size = new System.Drawing.Size(1144, 618);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 配置怪物ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置精英关ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TextBox TB_LevelName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TB_RefEliteLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_RefLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_EliteEnemy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_Enemy;
        private System.Windows.Forms.Label LB_RefSquads2;
        private System.Windows.Forms.Label LB_RefSquads1;
        private System.Windows.Forms.ToolStripMenuItem 配置剧情对话ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置奖励ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置精英关奖励ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.CheckBox CB_StrongEnemy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_EliteEnemyAngel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_EnemyAngel;
    }
}
