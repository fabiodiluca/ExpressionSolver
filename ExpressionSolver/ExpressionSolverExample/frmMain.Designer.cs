namespace ExpressionSolverExample
{
    partial class frmMain
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
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btSolve = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.btExampleEqual = new System.Windows.Forms.Button();
            this.btExampleDifferent = new System.Windows.Forms.Button();
            this.btVariables = new System.Windows.Forms.Button();
            this.btExampleIN = new System.Windows.Forms.Button();
            this.btExampleMath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(76, 37);
            this.txtParameters.Multiline = true;
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParameters.Size = new System.Drawing.Size(816, 80);
            this.txtParameters.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Expression";
            // 
            // btSolve
            // 
            this.btSolve.Location = new System.Drawing.Point(817, 123);
            this.btSolve.Name = "btSolve";
            this.btSolve.Size = new System.Drawing.Size(75, 23);
            this.btSolve.TabIndex = 8;
            this.btSolve.Text = "Solve";
            this.btSolve.UseVisualStyleBackColor = true;
            this.btSolve.Click += new System.EventHandler(this.btSolve_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 152);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(880, 310);
            this.txtResult.TabIndex = 7;
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(76, 8);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(816, 20);
            this.txtExpression.TabIndex = 6;
            // 
            // btExampleEqual
            // 
            this.btExampleEqual.Location = new System.Drawing.Point(12, 123);
            this.btExampleEqual.Name = "btExampleEqual";
            this.btExampleEqual.Size = new System.Drawing.Size(92, 23);
            this.btExampleEqual.TabIndex = 12;
            this.btExampleEqual.Text = "Equal";
            this.btExampleEqual.UseVisualStyleBackColor = true;
            this.btExampleEqual.Click += new System.EventHandler(this.btExampleEqual_Click);
            // 
            // btExampleDifferent
            // 
            this.btExampleDifferent.Location = new System.Drawing.Point(110, 123);
            this.btExampleDifferent.Name = "btExampleDifferent";
            this.btExampleDifferent.Size = new System.Drawing.Size(84, 23);
            this.btExampleDifferent.TabIndex = 13;
            this.btExampleDifferent.Text = "Different";
            this.btExampleDifferent.UseVisualStyleBackColor = true;
            this.btExampleDifferent.Click += new System.EventHandler(this.btExampleDifferent_Click);
            // 
            // btVariables
            // 
            this.btVariables.Location = new System.Drawing.Point(200, 123);
            this.btVariables.Name = "btVariables";
            this.btVariables.Size = new System.Drawing.Size(85, 23);
            this.btVariables.TabIndex = 14;
            this.btVariables.Text = "Variables";
            this.btVariables.UseVisualStyleBackColor = true;
            this.btVariables.Click += new System.EventHandler(this.btVariables_Click);
            // 
            // btExampleIN
            // 
            this.btExampleIN.Location = new System.Drawing.Point(291, 123);
            this.btExampleIN.Name = "btExampleIN";
            this.btExampleIN.Size = new System.Drawing.Size(75, 23);
            this.btExampleIN.TabIndex = 15;
            this.btExampleIN.Text = "IN";
            this.btExampleIN.UseVisualStyleBackColor = true;
            this.btExampleIN.Click += new System.EventHandler(this.btExampleIN_Click);
            // 
            // btExampleMath
            // 
            this.btExampleMath.Location = new System.Drawing.Point(372, 123);
            this.btExampleMath.Name = "btExampleMath";
            this.btExampleMath.Size = new System.Drawing.Size(75, 23);
            this.btExampleMath.TabIndex = 16;
            this.btExampleMath.Text = "Math";
            this.btExampleMath.UseVisualStyleBackColor = true;
            this.btExampleMath.Click += new System.EventHandler(this.btExampleMath_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 476);
            this.Controls.Add(this.btExampleMath);
            this.Controls.Add(this.btExampleIN);
            this.Controls.Add(this.btVariables);
            this.Controls.Add(this.btExampleDifferent);
            this.Controls.Add(this.btExampleEqual);
            this.Controls.Add(this.txtParameters);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSolve);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.txtExpression);
            this.Name = "frmMain";
            this.Text = "Expression Solver Examples";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtParameters;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSolve;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Button btExampleEqual;
        private System.Windows.Forms.Button btExampleDifferent;
        private System.Windows.Forms.Button btVariables;
        private System.Windows.Forms.Button btExampleIN;
        private System.Windows.Forms.Button btExampleMath;
    }
}

