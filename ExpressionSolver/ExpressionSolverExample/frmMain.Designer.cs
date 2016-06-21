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
            this.btExampleNotLike = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btExampleIs = new System.Windows.Forms.Button();
            this.btExampleIsNot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(76, 79);
            this.txtParameters.Multiline = true;
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParameters.Size = new System.Drawing.Size(816, 125);
            this.txtParameters.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Expression";
            // 
            // btSolve
            // 
            this.btSolve.Location = new System.Drawing.Point(818, 210);
            this.btSolve.Name = "btSolve";
            this.btSolve.Size = new System.Drawing.Size(75, 23);
            this.btSolve.TabIndex = 8;
            this.btSolve.Text = "Solve";
            this.btSolve.UseVisualStyleBackColor = true;
            this.btSolve.Click += new System.EventHandler(this.btSolve_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 239);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(880, 254);
            this.txtResult.TabIndex = 7;
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(76, 47);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(816, 20);
            this.txtExpression.TabIndex = 6;
            // 
            // btExampleEqual
            // 
            this.btExampleEqual.Location = new System.Drawing.Point(18, 13);
            this.btExampleEqual.Name = "btExampleEqual";
            this.btExampleEqual.Size = new System.Drawing.Size(61, 23);
            this.btExampleEqual.TabIndex = 12;
            this.btExampleEqual.Text = "Equal";
            this.btExampleEqual.UseVisualStyleBackColor = true;
            this.btExampleEqual.Click += new System.EventHandler(this.btExampleEqual_Click);
            // 
            // btExampleDifferent
            // 
            this.btExampleDifferent.Location = new System.Drawing.Point(85, 13);
            this.btExampleDifferent.Name = "btExampleDifferent";
            this.btExampleDifferent.Size = new System.Drawing.Size(60, 23);
            this.btExampleDifferent.TabIndex = 13;
            this.btExampleDifferent.Text = "Different";
            this.btExampleDifferent.UseVisualStyleBackColor = true;
            this.btExampleDifferent.Click += new System.EventHandler(this.btExampleDifferent_Click);
            // 
            // btVariables
            // 
            this.btVariables.Location = new System.Drawing.Point(212, 13);
            this.btVariables.Name = "btVariables";
            this.btVariables.Size = new System.Drawing.Size(66, 23);
            this.btVariables.TabIndex = 14;
            this.btVariables.Text = "Variables";
            this.btVariables.UseVisualStyleBackColor = true;
            this.btVariables.Click += new System.EventHandler(this.btVariables_Click);
            // 
            // btExampleIN
            // 
            this.btExampleIN.Location = new System.Drawing.Point(284, 13);
            this.btExampleIN.Name = "btExampleIN";
            this.btExampleIN.Size = new System.Drawing.Size(45, 23);
            this.btExampleIN.TabIndex = 15;
            this.btExampleIN.Text = "In";
            this.btExampleIN.UseVisualStyleBackColor = true;
            this.btExampleIN.Click += new System.EventHandler(this.btExampleIN_Click);
            // 
            // btExampleMath
            // 
            this.btExampleMath.Location = new System.Drawing.Point(151, 13);
            this.btExampleMath.Name = "btExampleMath";
            this.btExampleMath.Size = new System.Drawing.Size(55, 23);
            this.btExampleMath.TabIndex = 16;
            this.btExampleMath.Text = "Math";
            this.btExampleMath.UseVisualStyleBackColor = true;
            this.btExampleMath.Click += new System.EventHandler(this.btExampleMath_Click);
            // 
            // btExampleLike
            // 
            this.btExampleLike.Location = new System.Drawing.Point(397, 13);
            this.btExampleLike.Name = "btExampleLike";
            this.btExampleLike.Size = new System.Drawing.Size(58, 23);
            this.btExampleLike.TabIndex = 17;
            this.btExampleLike.Text = "Like";
            this.btExampleLike.UseVisualStyleBackColor = true;
            this.btExampleLike.Click += new System.EventHandler(this.btExampleLike_Click);
            // 
            // btExampleNotIn
            // 
            this.btExampleNotIn.Location = new System.Drawing.Point(335, 13);
            this.btExampleNotIn.Name = "btExampleNotIn";
            this.btExampleNotIn.Size = new System.Drawing.Size(56, 23);
            this.btExampleNotIn.TabIndex = 18;
            this.btExampleNotIn.Text = "Not In";
            this.btExampleNotIn.UseVisualStyleBackColor = true;
            this.btExampleNotIn.Click += new System.EventHandler(this.btExampleNotIn_Click);
            // 
            // btPerformance
            // 
            this.btPerformance.Location = new System.Drawing.Point(788, 13);
            this.btPerformance.Name = "btPerformance";
            this.btPerformance.Size = new System.Drawing.Size(104, 23);
            this.btPerformance.TabIndex = 19;
            this.btPerformance.Text = "Performance Test";
            this.btPerformance.UseVisualStyleBackColor = true;
            this.btPerformance.Click += new System.EventHandler(this.btPerformance_Click);
            // 
            // btExampleNotLike
            // 
            this.btExampleNotLike.Location = new System.Drawing.Point(461, 13);
            this.btExampleNotLike.Name = "btExampleNotLike";
            this.btExampleNotLike.Size = new System.Drawing.Size(58, 23);
            this.btExampleNotLike.TabIndex = 20;
            this.btExampleNotLike.Text = "Not Like";
            this.btExampleNotLike.UseVisualStyleBackColor = true;
            this.btExampleNotLike.Click += new System.EventHandler(this.btExampleNotLike_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Result";
            // 
            // btExampleIs
            // 
            this.btExampleIs.Location = new System.Drawing.Point(525, 13);
            this.btExampleIs.Name = "btExampleIs";
            this.btExampleIs.Size = new System.Drawing.Size(37, 23);
            this.btExampleIs.TabIndex = 22;
            this.btExampleIs.Text = "Is";
            this.btExampleIs.UseVisualStyleBackColor = true;
            this.btExampleIs.Click += new System.EventHandler(this.btExampleIs_Click);
            // 
            // btExampleIsNot
            // 
            this.btExampleIsNot.Location = new System.Drawing.Point(568, 13);
            this.btExampleIsNot.Name = "btExampleIsNot";
            this.btExampleIsNot.Size = new System.Drawing.Size(61, 23);
            this.btExampleIsNot.TabIndex = 23;
            this.btExampleIsNot.Text = "Is Not";
            this.btExampleIsNot.UseVisualStyleBackColor = true;
            this.btExampleIsNot.Click += new System.EventHandler(this.btExampleIsNot_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 503);
            this.Controls.Add(this.btExampleIsNot);
            this.Controls.Add(this.btExampleIs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btExampleNotLike);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Button btExampleNotLike;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btExampleIs;
        private System.Windows.Forms.Button btExampleIsNot;
    }
}

