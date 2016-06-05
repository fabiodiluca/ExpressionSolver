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
            this.btExampleLike = new System.Windows.Forms.Button();
            this.btExampleNotIn = new System.Windows.Forms.Button();
            this.btPerformance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(76, 40);
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
            this.label1.Location = new System.Drawing.Point(15, 11);
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
            this.btExampleEqual.Location = new System.Drawing.Point(12, 126);
            this.btExampleEqual.Name = "btExampleEqual";
            this.btExampleEqual.Size = new System.Drawing.Size(79, 23);
            this.btExampleEqual.TabIndex = 12;
            this.btExampleEqual.Text = "Equal";
            this.btExampleEqual.UseVisualStyleBackColor = true;
            this.btExampleEqual.Click += new System.EventHandler(this.btExampleEqual_Click);
            // 
            // btExampleDifferent
            // 
            this.btExampleDifferent.Location = new System.Drawing.Point(97, 126);
            this.btExampleDifferent.Name = "btExampleDifferent";
            this.btExampleDifferent.Size = new System.Drawing.Size(76, 23);
            this.btExampleDifferent.TabIndex = 13;
            this.btExampleDifferent.Text = "Different";
            this.btExampleDifferent.UseVisualStyleBackColor = true;
            this.btExampleDifferent.Click += new System.EventHandler(this.btExampleDifferent_Click);
            // 
            // btVariables
            // 
            this.btVariables.Location = new System.Drawing.Point(260, 126);
            this.btVariables.Name = "btVariables";
            this.btVariables.Size = new System.Drawing.Size(73, 23);
            this.btVariables.TabIndex = 14;
            this.btVariables.Text = "Variables";
            this.btVariables.UseVisualStyleBackColor = true;
            this.btVariables.Click += new System.EventHandler(this.btVariables_Click);
            // 
            // btExampleIN
            // 
            this.btExampleIN.Location = new System.Drawing.Point(339, 126);
            this.btExampleIN.Name = "btExampleIN";
            this.btExampleIN.Size = new System.Drawing.Size(75, 23);
            this.btExampleIN.TabIndex = 15;
            this.btExampleIN.Text = "In";
            this.btExampleIN.UseVisualStyleBackColor = true;
            this.btExampleIN.Click += new System.EventHandler(this.btExampleIN_Click);
            // 
            // btExampleMath
            // 
            this.btExampleMath.Location = new System.Drawing.Point(179, 126);
            this.btExampleMath.Name = "btExampleMath";
            this.btExampleMath.Size = new System.Drawing.Size(75, 23);
            this.btExampleMath.TabIndex = 16;
            this.btExampleMath.Text = "Math";
            this.btExampleMath.UseVisualStyleBackColor = true;
            this.btExampleMath.Click += new System.EventHandler(this.btExampleMath_Click);
            // 
            // btExampleLike
            // 
            this.btExampleLike.Location = new System.Drawing.Point(501, 126);
            this.btExampleLike.Name = "btExampleLike";
            this.btExampleLike.Size = new System.Drawing.Size(75, 23);
            this.btExampleLike.TabIndex = 17;
            this.btExampleLike.Text = "Like";
            this.btExampleLike.UseVisualStyleBackColor = true;
            this.btExampleLike.Click += new System.EventHandler(this.btExampleLike_Click);
            // 
            // btExampleNotIn
            // 
            this.btExampleNotIn.Location = new System.Drawing.Point(420, 126);
            this.btExampleNotIn.Name = "btExampleNotIn";
            this.btExampleNotIn.Size = new System.Drawing.Size(75, 23);
            this.btExampleNotIn.TabIndex = 18;
            this.btExampleNotIn.Text = "Not In";
            this.btExampleNotIn.UseVisualStyleBackColor = true;
            this.btExampleNotIn.Click += new System.EventHandler(this.btExampleNotIn_Click);
            // 
            // btPerformance
            // 
            this.btPerformance.Location = new System.Drawing.Point(582, 126);
            this.btPerformance.Name = "btPerformance";
            this.btPerformance.Size = new System.Drawing.Size(104, 23);
            this.btPerformance.TabIndex = 19;
            this.btPerformance.Text = "Performance Test";
            this.btPerformance.UseVisualStyleBackColor = true;
            this.btPerformance.Click += new System.EventHandler(this.btPerformance_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 476);
            this.Controls.Add(this.btPerformance);
            this.Controls.Add(this.btExampleNotIn);
            this.Controls.Add(this.btExampleLike);
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
        private System.Windows.Forms.Button btExampleLike;
        private System.Windows.Forms.Button btExampleNotIn;
        private System.Windows.Forms.Button btPerformance;
    }
}

