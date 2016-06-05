﻿using ExpressionSolver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpressionSolverExample
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        Solver Solver = new Solver();
        StringBuilder Log = new StringBuilder();

        private void btSolve_Click(object sender, EventArgs e)
        {
            //try
            //{
            #region Parameters
            Dictionary<string, string> Values = new Dictionary<string, string>();
            string[] KeyValues = txtParameters.Text.Replace("\r", "").Replace("\n", "").Split(';');
            foreach (string KeyValue in KeyValues)
            {
                try
                {
                    Values.Add(KeyValue.Split('=')[0].Trim(), KeyValue.Split('=')[1].Trim());
                }
                catch { }
            }
            #endregion

            DateTime Start = DateTime.Now;
            Log = new StringBuilder(15000);
            Solver.Solve(txtExpression.Text, ref Log, Values);
            DateTime End = DateTime.Now;
            txtResult.Text = Log.ToString();
            TimeSpan Solving = End - Start;
            txtResult.Text += " ( " + Solving.Milliseconds.ToString() + "ms )";
            //} 
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btExampleEqual_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "('My Test String'='My Test String') AND (999=999) AND TRUE";
            txtResult.Text = "";
            txtParameters.Text = "";
        }

        private void btExampleDifferent_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "('My String'!='My Test String') AND (999!=999.1)";
            txtResult.Text = "";
            txtParameters.Text = "";
        }

        private void btVariables_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "(MY_VARIABLE = 'My Test String') AND (MY_VARIABLE2 = 'My Test String 2')";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = 'My Test String';\r\nMY_VARIABLE2 = 'My Test String 2'";
        }

        private void btExampleIN_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "(MY_VARIABLE IN ('A','B','INSIDE','D'))";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = 'INSIDE';";
        }

        private void btExampleMath_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "((2*2*2)+2-2)*1";
            txtResult.Text = "";
            txtParameters.Text = "";
        }


    }
}
