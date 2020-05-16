using ExpressionSolver;
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

        ExpressionSolver.Solver Solver = new ExpressionSolver.Solver();
        StringBuilder Log = new StringBuilder();

        private void btSolve_Click(object sender, EventArgs e)
        {
            #region Parameters
            Dictionary<string, string> Parameters = SolverUtil.ParseParameters(txtParameters.Text);
            #endregion

            DateTime Start = DateTime.Now;
            Log = new StringBuilder(15000);
            Solver.Solve(txtExpression.Text, ref Log, Parameters);
            DateTime End = DateTime.Now;
            txtResult.Text = Log.ToString();
            TimeSpan Solving = End - Start;
            txtResult.Text += " ( " + Solving.Milliseconds.ToString() + "ms )";

            foreach(var field in typeof(Operators).GetFields())
            {
                object a = field.GetValue(null);
            }
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

        private void btExampleNotIn_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "MY_VARIABLE NOT IN ('A','B','C')";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = 'D';";
        }

        private void btExampleMath_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "10 > (((2*2*2)+2-2)*1)";
            txtResult.Text = "";
            txtParameters.Text = "";
        }

        private void btExampleLike_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "MY_VARIABLE LIKE '%test%'";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = 'This is a test variable';";
        }

        private void btExampleNotLike_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "MY_VARIABLE NOT LIKE '%test vaaaariable%'";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = 'This is a test variable';";
        }

        private void btPerformance_Click(object sender, EventArgs e)
        {
            int MaxInteractions = 5000;
            string Expression = "(10 > (((2*2*2)+2-2)*1)) AND (TRUE OR FALSE) AND ('TEST'='TEST') OR (1000 <= 1000)";
            Log = new StringBuilder(15000);
            Log.AppendLine("Resolving " + MaxInteractions + " times the expression " + Expression);

            txtResult.Text = Log.ToString();
            Application.DoEvents();

            DateTime Start = DateTime.Now;
            for (int aux = 1; aux <= MaxInteractions; aux++)
            {
                Solver.Solve(Expression);
            }
            DateTime End = DateTime.Now;
            txtResult.Text = Log.ToString();
            TimeSpan Solving = End - Start;
            txtResult.Text += " ( " + Solving.Milliseconds.ToString() + "ms )\r\n";
            txtResult.Text += (((double)1000 * (double)MaxInteractions) / (double)Solving.Milliseconds).ToString("f2") + " expressions/second";
        }

        private void btExampleIs_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "(MY_VARIABLE IS NULL) AND (MY_VARIABLE_2 IS NULL)";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = null;\r\nMY_VARIABLE_2 = null;";
        }

        private void btExampleIsNot_Click(object sender, EventArgs e)
        {
            txtExpression.Text = "(MY_VARIABLE IS NOT NULL) AND (MY_VARIABLE_2 IS NOT NULL)";
            txtResult.Text = "";
            txtParameters.Text = "MY_VARIABLE = 'this is a string';\r\nMY_VARIABLE_2 = 343;";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //var a = new ExpressionSolver.Solver();            

            //var log = new StringBuilder();
            //try
            //{
            //    //var b = a.Solve("(10 > (((2*2*2)+2-2)*1)) AND (TRUE OR FALSE) AND ('TEST'='TEST') OR (1000 <= 1000)", ref log);
            //    //MessageBox.Show(log.ToString());
            //    //var b = a.Solve("1>=+1 AND  -234.2*--154>23421142   OR FALSE OR   TRUE", ref log);
            //    //var b = a.Solve("4.251- + 3++-  +4*  1    +4/4.5+25--1", ref log);
            //    //var b = a.Solve("+++1+1---+1+-1+1*-58 * 55/-4556", ref log);
            //    //var b = a.Solve("((10 )-(10)*10)", ref log);
            //    //var b = a.Solve("1+5+4*5+8+6*10000", ref log);
            //    var b = a.Solve("'AAA'='AAA'", ref log);
            //    //var b = a.Solve("('My String'!='My Test String') AND (999!=999.1)", ref log);
            //    //var b = a.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1-1))", ref log);
            //    //var Parameters = new Dictionary<string, string>();
            //    //Parameters.Add("MY_VARIABLE", "'A'");
            //    //var b = a.Solve("(MY_VARIABLE IN ('A','B)','INSIDE','D'))", ref log, Parameters);
            //    //var Parameters = new Dictionary<string, string>();
            //    //Parameters.Add("x", "3");
            //    //var b = a.Solve("x+(x+3)", ref log, Parameters);
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.ToString());
            //}
            //finally
            //{
            //    MessageBox.Show(log.ToString());
            //}
        }

    }
}
