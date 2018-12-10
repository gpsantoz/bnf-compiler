namespace Analisador.Forms
{
    partial class TablesForm
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
            this.rbLL = new System.Windows.Forms.RadioButton();
            this.rbLALR = new System.Windows.Forms.RadioButton();
            this.rbLR = new System.Windows.Forms.RadioButton();
            this.rbSLR = new System.Windows.Forms.RadioButton();
            this.lbCadeia = new System.Windows.Forms.Label();
            this.txtString = new System.Windows.Forms.TextBox();
            this.btnAcceptance = new System.Windows.Forms.Button();
            this.gridTables = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lalrLabel = new System.Windows.Forms.Label();
            this.lrLabel = new System.Windows.Forms.Label();
            this.slrLabel = new System.Windows.Forms.Label();
            this.llLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridTables)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbLL
            // 
            this.rbLL.AutoSize = true;
            this.rbLL.Location = new System.Drawing.Point(7, 22);
            this.rbLL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLL.Name = "rbLL";
            this.rbLL.Size = new System.Drawing.Size(63, 21);
            this.rbLL.TabIndex = 0;
            this.rbLL.TabStop = true;
            this.rbLL.Text = "LL(1)";
            this.rbLL.UseVisualStyleBackColor = true;
            this.rbLL.CheckedChanged += new System.EventHandler(this.rbLL_CheckedChanged);
            // 
            // rbLALR
            // 
            this.rbLALR.AutoSize = true;
            this.rbLALR.Location = new System.Drawing.Point(7, 94);
            this.rbLALR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLALR.Name = "rbLALR";
            this.rbLALR.Size = new System.Drawing.Size(82, 21);
            this.rbLALR.TabIndex = 2;
            this.rbLALR.TabStop = true;
            this.rbLALR.Text = "LALR(1)";
            this.rbLALR.UseVisualStyleBackColor = true;
            this.rbLALR.CheckedChanged += new System.EventHandler(this.rbLALR_CheckedChanged);
            // 
            // rbLR
            // 
            this.rbLR.AutoSize = true;
            this.rbLR.Location = new System.Drawing.Point(7, 70);
            this.rbLR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLR.Name = "rbLR";
            this.rbLR.Size = new System.Drawing.Size(65, 21);
            this.rbLR.TabIndex = 3;
            this.rbLR.TabStop = true;
            this.rbLR.Text = "LR(1)";
            this.rbLR.UseVisualStyleBackColor = true;
            this.rbLR.CheckedChanged += new System.EventHandler(this.rbLR_CheckedChanged);
            // 
            // rbSLR
            // 
            this.rbSLR.AutoSize = true;
            this.rbSLR.Location = new System.Drawing.Point(7, 46);
            this.rbSLR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSLR.Name = "rbSLR";
            this.rbSLR.Size = new System.Drawing.Size(74, 21);
            this.rbSLR.TabIndex = 4;
            this.rbSLR.TabStop = true;
            this.rbSLR.Text = "SLR(1)";
            this.rbSLR.UseVisualStyleBackColor = true;
            this.rbSLR.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // lbCadeia
            // 
            this.lbCadeia.AutoSize = true;
            this.lbCadeia.Location = new System.Drawing.Point(14, 663);
            this.lbCadeia.Name = "lbCadeia";
            this.lbCadeia.Size = new System.Drawing.Size(78, 17);
            this.lbCadeia.TabIndex = 5;
            this.lbCadeia.Text = "Input string";
            // 
            // txtString
            // 
            this.txtString.Location = new System.Drawing.Point(155, 658);
            this.txtString.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(433, 22);
            this.txtString.TabIndex = 6;
            // 
            // btnAcceptance
            // 
            this.btnAcceptance.Location = new System.Drawing.Point(595, 658);
            this.btnAcceptance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAcceptance.Name = "btnAcceptance";
            this.btnAcceptance.Size = new System.Drawing.Size(132, 23);
            this.btnAcceptance.TabIndex = 7;
            this.btnAcceptance.Text = "Start Analysis";
            this.btnAcceptance.UseVisualStyleBackColor = true;
            // 
            // gridTables
            // 
            this.gridTables.AllowUserToAddRows = false;
            this.gridTables.AllowUserToDeleteRows = false;
            this.gridTables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTables.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridTables.BackgroundColor = System.Drawing.Color.White;
            this.gridTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTables.Location = new System.Drawing.Point(19, 11);
            this.gridTables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridTables.Name = "gridTables";
            this.gridTables.ReadOnly = true;
            this.gridTables.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.gridTables.RowTemplate.Height = 28;
            this.gridTables.Size = new System.Drawing.Size(708, 642);
            this.gridTables.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lalrLabel);
            this.groupBox1.Controls.Add(this.lrLabel);
            this.groupBox1.Controls.Add(this.slrLabel);
            this.groupBox1.Controls.Add(this.llLabel);
            this.groupBox1.Controls.Add(this.rbLL);
            this.groupBox1.Controls.Add(this.rbLALR);
            this.groupBox1.Controls.Add(this.rbLR);
            this.groupBox1.Controls.Add(this.rbSLR);
            this.groupBox1.Location = new System.Drawing.Point(733, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(296, 174);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Syntactic Analyser";
            // 
            // lalrLabel
            // 
            this.lalrLabel.AutoSize = true;
            this.lalrLabel.Location = new System.Drawing.Point(99, 98);
            this.lalrLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lalrLabel.Name = "lalrLabel";
            this.lalrLabel.Size = new System.Drawing.Size(46, 17);
            this.lalrLabel.TabIndex = 14;
            this.lalrLabel.Text = "label4";
            // 
            // lrLabel
            // 
            this.lrLabel.AutoSize = true;
            this.lrLabel.Location = new System.Drawing.Point(78, 73);
            this.lrLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lrLabel.Name = "lrLabel";
            this.lrLabel.Size = new System.Drawing.Size(46, 17);
            this.lrLabel.TabIndex = 13;
            this.lrLabel.Text = "label3";
            this.lrLabel.Click += new System.EventHandler(this.lrLabel_Click);
            // 
            // slrLabel
            // 
            this.slrLabel.AutoSize = true;
            this.slrLabel.Location = new System.Drawing.Point(78, 50);
            this.slrLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.slrLabel.Name = "slrLabel";
            this.slrLabel.Size = new System.Drawing.Size(46, 17);
            this.slrLabel.TabIndex = 12;
            this.slrLabel.Text = "label2";
            // 
            // llLabel
            // 
            this.llLabel.AutoSize = true;
            this.llLabel.Location = new System.Drawing.Point(67, 25);
            this.llLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llLabel.Name = "llLabel";
            this.llLabel.Size = new System.Drawing.Size(0, 17);
            this.llLabel.TabIndex = 11;
            // 
            // TablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gridTables);
            this.Controls.Add(this.btnAcceptance);
            this.Controls.Add(this.txtString);
            this.Controls.Add(this.lbCadeia);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TablesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Syntactic Analytical Tables";
            this.Load += new System.EventHandler(this.TablesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTables)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbLL;
        private System.Windows.Forms.RadioButton rbLALR;
        private System.Windows.Forms.RadioButton rbLR;
        private System.Windows.Forms.RadioButton rbSLR;
        private System.Windows.Forms.Label lbCadeia;
        private System.Windows.Forms.TextBox txtString;
        private System.Windows.Forms.Button btnAcceptance;
        private System.Windows.Forms.DataGridView gridTables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lalrLabel;
        private System.Windows.Forms.Label lrLabel;
        private System.Windows.Forms.Label slrLabel;
        private System.Windows.Forms.Label llLabel;
    }
}