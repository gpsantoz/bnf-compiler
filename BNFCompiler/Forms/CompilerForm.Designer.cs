namespace Analisador
{
    partial class CompilerForm
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
            this.RtxtCompiler = new System.Windows.Forms.RichTextBox();
            this.btnLexical = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLexical = new System.Windows.Forms.Label();
            this.btnSyntatic = new System.Windows.Forms.Button();
            this.lblSyntactic = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnShowFirst = new System.Windows.Forms.Button();
            this.btnFollow = new System.Windows.Forms.Button();
            this.btnTables = new System.Windows.Forms.Button();
            this.rbGrammar1 = new System.Windows.Forms.RadioButton();
            this.rbGrammar2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGrammar4 = new System.Windows.Forms.RadioButton();
            this.rbGrammar3 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RtxtCompiler
            // 
            this.RtxtCompiler.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtCompiler.Location = new System.Drawing.Point(16, 44);
            this.RtxtCompiler.Margin = new System.Windows.Forms.Padding(4);
            this.RtxtCompiler.Name = "RtxtCompiler";
            this.RtxtCompiler.Size = new System.Drawing.Size(721, 536);
            this.RtxtCompiler.TabIndex = 0;
            this.RtxtCompiler.Text = "<E>::=<T><El>\n<T>::=<F><Tl>\n<F>::=\"a\" <E>\"f\"\n<F>::=\"x\"\n<El>::=\"m\"<T><El>\n<El>::=\"" +
    "\"\n<Tl>::=\"v\"<F><Tl>\n<Tl>::=\"\"";
            // 
            // btnLexical
            // 
            this.btnLexical.Location = new System.Drawing.Point(747, 44);
            this.btnLexical.Margin = new System.Windows.Forms.Padding(4);
            this.btnLexical.Name = "btnLexical";
            this.btnLexical.Size = new System.Drawing.Size(283, 28);
            this.btnLexical.TabIndex = 1;
            this.btnLexical.Text = "Perform Lexical Analysis";
            this.btnLexical.UseVisualStyleBackColor = true;
            this.btnLexical.Click += new System.EventHandler(this.btnLexical_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(213, 11);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(127, 31);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Grammar";
            // 
            // lblLexical
            // 
            this.lblLexical.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLexical.AutoSize = true;
            this.lblLexical.Location = new System.Drawing.Point(747, 76);
            this.lblLexical.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLexical.Name = "lblLexical";
            this.lblLexical.Size = new System.Drawing.Size(95, 17);
            this.lblLexical.TabIndex = 3;
            this.lblLexical.Text = "Lexical Result";
            this.lblLexical.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLexical.Click += new System.EventHandler(this.lblLexical_Click);
            // 
            // btnSyntatic
            // 
            this.btnSyntatic.Location = new System.Drawing.Point(747, 129);
            this.btnSyntatic.Margin = new System.Windows.Forms.Padding(4);
            this.btnSyntatic.Name = "btnSyntatic";
            this.btnSyntatic.Size = new System.Drawing.Size(283, 28);
            this.btnSyntatic.TabIndex = 4;
            this.btnSyntatic.Text = "Perform Syntactic Result";
            this.btnSyntatic.UseVisualStyleBackColor = true;
            this.btnSyntatic.Click += new System.EventHandler(this.btnSyntatic_Click);
            // 
            // lblSyntactic
            // 
            this.lblSyntactic.AutoSize = true;
            this.lblSyntactic.Location = new System.Drawing.Point(747, 161);
            this.lblSyntactic.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSyntactic.Name = "lblSyntactic";
            this.lblSyntactic.Size = new System.Drawing.Size(109, 17);
            this.lblSyntactic.TabIndex = 5;
            this.lblSyntactic.Text = "Syntactic Result";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(929, 647);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 28);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnShowFirst
            // 
            this.btnShowFirst.Location = new System.Drawing.Point(747, 239);
            this.btnShowFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowFirst.Name = "btnShowFirst";
            this.btnShowFirst.Size = new System.Drawing.Size(133, 26);
            this.btnShowFirst.TabIndex = 7;
            this.btnShowFirst.Text = "First";
            this.btnShowFirst.UseVisualStyleBackColor = true;
            this.btnShowFirst.Click += new System.EventHandler(this.btnShowFirst_Click);
            // 
            // btnFollow
            // 
            this.btnFollow.Location = new System.Drawing.Point(896, 239);
            this.btnFollow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFollow.Name = "btnFollow";
            this.btnFollow.Size = new System.Drawing.Size(133, 26);
            this.btnFollow.TabIndex = 8;
            this.btnFollow.Text = "Follow";
            this.btnFollow.UseVisualStyleBackColor = true;
            this.btnFollow.Click += new System.EventHandler(this.btnFollow_Click);
            // 
            // btnTables
            // 
            this.btnTables.Location = new System.Drawing.Point(747, 310);
            this.btnTables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTables.Name = "btnTables";
            this.btnTables.Size = new System.Drawing.Size(283, 28);
            this.btnTables.TabIndex = 9;
            this.btnTables.Text = "Tables";
            this.btnTables.UseVisualStyleBackColor = true;
            this.btnTables.Click += new System.EventHandler(this.btnTables_Click);
            // 
            // rbGrammar1
            // 
            this.rbGrammar1.AutoSize = true;
            this.rbGrammar1.Location = new System.Drawing.Point(8, 23);
            this.rbGrammar1.Margin = new System.Windows.Forms.Padding(4);
            this.rbGrammar1.Name = "rbGrammar1";
            this.rbGrammar1.Size = new System.Drawing.Size(100, 21);
            this.rbGrammar1.TabIndex = 10;
            this.rbGrammar1.TabStop = true;
            this.rbGrammar1.Text = "Grammar 1";
            this.rbGrammar1.UseVisualStyleBackColor = true;
            this.rbGrammar1.CheckedChanged += new System.EventHandler(this.rbGrammar1_CheckedChanged);
            // 
            // rbGrammar2
            // 
            this.rbGrammar2.AutoSize = true;
            this.rbGrammar2.Location = new System.Drawing.Point(8, 52);
            this.rbGrammar2.Margin = new System.Windows.Forms.Padding(4);
            this.rbGrammar2.Name = "rbGrammar2";
            this.rbGrammar2.Size = new System.Drawing.Size(100, 21);
            this.rbGrammar2.TabIndex = 11;
            this.rbGrammar2.TabStop = true;
            this.rbGrammar2.Text = "Grammar 2";
            this.rbGrammar2.UseVisualStyleBackColor = true;
            this.rbGrammar2.CheckedChanged += new System.EventHandler(this.rbGrammar2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGrammar4);
            this.groupBox1.Controls.Add(this.rbGrammar3);
            this.groupBox1.Controls.Add(this.rbGrammar1);
            this.groupBox1.Controls.Add(this.rbGrammar2);
            this.groupBox1.Location = new System.Drawing.Point(16, 588);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(276, 87);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Predefined Grammars";
            // 
            // rbGrammar4
            // 
            this.rbGrammar4.AutoSize = true;
            this.rbGrammar4.Location = new System.Drawing.Point(157, 52);
            this.rbGrammar4.Margin = new System.Windows.Forms.Padding(4);
            this.rbGrammar4.Name = "rbGrammar4";
            this.rbGrammar4.Size = new System.Drawing.Size(100, 21);
            this.rbGrammar4.TabIndex = 13;
            this.rbGrammar4.TabStop = true;
            this.rbGrammar4.Text = "Grammar 4";
            this.rbGrammar4.UseVisualStyleBackColor = true;
            // 
            // rbGrammar3
            // 
            this.rbGrammar3.AutoSize = true;
            this.rbGrammar3.Location = new System.Drawing.Point(157, 23);
            this.rbGrammar3.Margin = new System.Windows.Forms.Padding(4);
            this.rbGrammar3.Name = "rbGrammar3";
            this.rbGrammar3.Size = new System.Drawing.Size(100, 21);
            this.rbGrammar3.TabIndex = 12;
            this.rbGrammar3.TabStop = true;
            this.rbGrammar3.Text = "Grammar 3";
            this.rbGrammar3.UseVisualStyleBackColor = true;
            this.rbGrammar3.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // CompilerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTables);
            this.Controls.Add(this.btnFollow);
            this.Controls.Add(this.btnShowFirst);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblSyntactic);
            this.Controls.Add(this.btnSyntatic);
            this.Controls.Add(this.lblLexical);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLexical);
            this.Controls.Add(this.RtxtCompiler);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CompilerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BNF Compiler";
            this.Load += new System.EventHandler(this.CompilerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RtxtCompiler;
        private System.Windows.Forms.Button btnLexical;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLexical;
        private System.Windows.Forms.Button btnSyntatic;
        private System.Windows.Forms.Label lblSyntactic;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnShowFirst;
        private System.Windows.Forms.Button btnFollow;
        private System.Windows.Forms.Button btnTables;
        private System.Windows.Forms.RadioButton rbGrammar1;
        private System.Windows.Forms.RadioButton rbGrammar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbGrammar4;
        private System.Windows.Forms.RadioButton rbGrammar3;
    }
}

