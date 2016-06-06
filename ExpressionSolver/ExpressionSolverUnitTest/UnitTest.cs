using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionSolver;

namespace ExpressionSolverUnitTest
{
    [TestClass]
    public class UnitTest
    {
        Solver Solver = new Solver();
        [TestMethod]
        public void TestTRUE()
        {
            Assert.AreEqual(Solver.Solve("TRUE"), "TRUE");
        }

        [TestMethod]
        public void TestFALSE()
        {
            Assert.AreEqual(Solver.Solve("FALSE"), "FALSE");
        }

        [TestMethod]
        public void TestAND()
        {
            Assert.AreEqual(Solver.Solve("TRUE AND TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE AND FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("FALSE AND TRUE"), "FALSE");
            Assert.AreEqual(Solver.Solve("FALSE AND FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("TRUE AND TRUE AND TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE AND FALSE AND TRUE"), "FALSE");
            Assert.AreEqual(Solver.Solve("TRUE AND TRUE AND FALSE"), "FALSE");
        }

        [TestMethod]
        public void TestOR()
        {
            Assert.AreEqual(Solver.Solve("TRUE OR TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE OR FALSE"), "TRUE");
            Assert.AreEqual(Solver.Solve("FALSE OR TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("FALSE OR FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("TRUE OR TRUE OR TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE OR FALSE OR TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE OR TRUE OR FALSE"), "TRUE");
            Assert.AreEqual(Solver.Solve("FALSE OR FALSE OR FALSE"), "FALSE");
        }

        [TestMethod]
        public void TestEqual()
        {
            Assert.AreEqual(Solver.Solve("TRUE = TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE = FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("FALSE = TRUE"), "FALSE");
            Assert.AreEqual(Solver.Solve("FALSE = FALSE"), "TRUE");
            Assert.AreEqual(Solver.Solve("1 = 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' = 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' = '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1 = '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("2 = 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' = 2"), "FALSE");
            Assert.AreEqual(Solver.Solve("'2' = '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 = '2'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 = '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.0 = '1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.00 = '1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.00 = '1.000'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.000 = '1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.000 = '1.009'"), "FALSE");
            Assert.AreEqual(Solver.Solve("TRUE=TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("TRUE=FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("FALSE=TRUE"), "FALSE");
            Assert.AreEqual(Solver.Solve("FALSE=FALSE"), "TRUE");
            Assert.AreEqual(Solver.Solve("1=1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1'=1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1'='1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1='1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("2=1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1'=2"), "FALSE");
            Assert.AreEqual(Solver.Solve("'2'='1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1='2'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1='1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.0='1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.00='1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.00='1.000'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.000='1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.000='1.009'"), "FALSE");
            Assert.AreEqual(Solver.Solve("'A'='B'"), "FALSE");
            Assert.AreEqual(Solver.Solve("'A'='a'"), "FALSE");
            Assert.AreEqual(Solver.Solve("'A'='A'"), "TRUE");
            Assert.AreEqual(Solver.Solve("'TESTE'='TESTE'"), "TRUE");
            Assert.AreEqual(Solver.Solve("'TESTE'=' TESTE'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1=' TESTE'"), "FALSE");
        }

        [TestMethod]
        public void TestString()
        {
            Assert.AreEqual(Solver.Solve("'A' = 'A'"), "TRUE");
            Assert.AreEqual(Solver.Solve("'A'='A'"), "TRUE");
            Assert.AreEqual(Solver.Solve("' A'='A'"), "FALSE");
            Assert.AreEqual(Solver.Solve("''''=''''"), "TRUE");
            Assert.AreEqual(Solver.Solve("'T''E'='T''E'"), "TRUE");
            Assert.AreEqual(Solver.Solve("''''''=''''"), "FALSE");
            Assert.AreEqual(Solver.Solve("''''''=''''''"), "TRUE");
        }

        [TestMethod]
        public void TestStringVersusNumber()
        {
            Assert.AreEqual(Solver.Solve("1 = 00001"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' = '00001'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 = '00001'"), "TRUE");
        }

        [TestMethod]
        public void TestDifferent()
        {
            Assert.AreEqual(Solver.Solve("TRUE != TRUE"), "FALSE");
            Assert.AreEqual(Solver.Solve("TRUE != FALSE"), "TRUE");
            Assert.AreEqual(Solver.Solve("FALSE != TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("FALSE != FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 != 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' != 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' != '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 != '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("2 != 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' != 2"), "TRUE");
            Assert.AreEqual(Solver.Solve("'2' != '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1 != '2'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.0 != '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.00 != '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.00 != '1.000'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.000 != '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.000 != '1.009'"), "TRUE");
        }

        [TestMethod]
        public void TestGreater()
        {
            Assert.AreEqual(Solver.Solve("1 > 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' > 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' > '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 > '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("2 > 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' > 2"), "FALSE");
            Assert.AreEqual(Solver.Solve("'2' > '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1 > '2'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.0 > '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.00 > '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.00 > '1.000'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.000 > '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.000 > '1.009'"), "FALSE");
        }

        [TestMethod]
        public void TestGreaterOrEqual()
        {
            Assert.AreEqual(Solver.Solve("1 >= 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' >= 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' >= '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1 >= '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("2 >= 1"), "TRUE");
            Assert.AreEqual(Solver.Solve("'1' >= 2"), "FALSE");
            Assert.AreEqual(Solver.Solve("'2' >= '1'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1 >= '2'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.0 >= '1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.00 >= '1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.00 >= '1.000'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.000 >= '1.00'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.000 >= '1.009'"), "FALSE");
        }

        [TestMethod]
        public void TestLess()
        {
            Assert.AreEqual(Solver.Solve("1 < 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' < 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' < '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 < '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("2 < 1"), "FALSE");
            Assert.AreEqual(Solver.Solve("'1' < 2"), "TRUE");
            Assert.AreEqual(Solver.Solve("'2' < '1'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1 < '2'"), "TRUE");
            Assert.AreEqual(Solver.Solve("1.0 < '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.00 < '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.00 < '1.000'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.000 < '1.00'"), "FALSE");
            Assert.AreEqual(Solver.Solve("1.000 < '1.009'"), "TRUE");
        }

        [TestMethod]
        public void TestPlus()
        {
            Assert.AreEqual(Solver.Solve("+1"), "+1");
            Assert.AreEqual(Solver.Solve("1+1"), "2");
            Assert.AreEqual(Solver.Solve("1+1+1"), "3");
            Assert.AreEqual(Solver.Solve("1 + 2 +3+4"), "10");
            Assert.AreEqual(Solver.Solve("(1 + 2) +3+4"), "10");
            Assert.AreEqual(Solver.Solve("999999+1"), "1000000");
        }

        [TestMethod]
        public void TestMinus()
        {
            Assert.AreEqual(Solver.Solve("-1"), "-1");
            Assert.AreEqual(Solver.Solve("1-1"), "0");
            Assert.AreEqual(Solver.Solve("1-1-1"), "-1");
            Assert.AreEqual(Solver.Solve("-1 - 2 -3-4"), "-10");
            Assert.AreEqual(Solver.Solve("(-1 - 2) -3-4"), "-10");
            Assert.AreEqual(Solver.Solve("-999999-1"), "-1000000");
        }

        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual(Solver.Solve("1*1"), "1");
            Assert.AreEqual(Solver.Solve("1*2"), "2");
            Assert.AreEqual(Solver.Solve("1*1*1"), "1");
            Assert.AreEqual(Solver.Solve("-1*2"), "-2");
            Assert.AreEqual(Solver.Solve("-1*-2"), "2");
            Assert.AreEqual(Solver.Solve("-(1*  2) *3*4"), "-24");
            Assert.AreEqual(Solver.Solve("-999999*1"), "-999999");
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual(Solver.Solve("1/1"), "1");
            Assert.AreEqual(Solver.Solve("2/2"), "1");
            Assert.AreEqual(Solver.Solve("9/3"), "3");
            Assert.AreEqual(Solver.Solve("100/5"), "20");
            Assert.AreEqual(Solver.Solve("-100/5"), "-20");
            Assert.AreEqual(Solver.Solve("-100/-5"), "20");
        }

        [TestMethod]
        public void TestMath()
        {
            Assert.AreEqual(Solver.Solve("(((100/5)/20)*5)+10"), "15");
        }

        [TestMethod]
        public void TestNotLike()
        {
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE '%EST%'"), "FALSE");
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE 'TEST%'"), "FALSE");
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE '%TESTE%'"), "FALSE");
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE 'TTEST%'"), "TRUE");
        }

        [TestMethod]
        public void TestLike()
        {
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE '%EST%'"), "TRUE");
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE 'TEST%'"), "TRUE");
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE '%TESTE%'"), "TRUE");
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE 'TTEST%'"), "FALSE");
        }

        [TestMethod]
        public void TestIN()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('A','B','TESTE')"), "TRUE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('A','B','C','D')"), "FALSE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('TESTE')"), "TRUE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN (' AND ')"), "TRUE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('A','B')"), "TRUE");
        }

        [TestMethod]
        public void TestNotIN()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('A','B','TESTE')"), "FALSE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('A','B','C','D')"), "TRUE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('TESTE')"), "FALSE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN (' AND ')"), "FALSE");

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('A','B')"), "FALSE");
        }

        [TestMethod]
        public void Parenthesis()
        {
            Assert.AreEqual(Solver.Solve("(TRUE)"), "TRUE");
            Assert.AreEqual(Solver.Solve("(FALSE)"), "FALSE");

            Assert.AreEqual(Solver.Solve("((TRUE))"), "TRUE");
            Assert.AreEqual(Solver.Solve("(((FALSE)))"), "FALSE");

            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE)"), "TRUE");
            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE) AND FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE)AND FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("((TRUE AND TRUE) AND FALSE)"), "FALSE");
            Assert.AreEqual(Solver.Solve("((TRUE AND TRUE)AND FALSE)"), "FALSE");
            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE AND FALSE)"), "FALSE");
            Assert.AreEqual(Solver.Solve("(TRUE AND (TRUE AND (FALSE)))"), "FALSE");

            Assert.AreEqual(Solver.Solve("(TRUE OR TRUE) AND FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("(TRUE OR TRUE) AND TRUE"), "TRUE");
            Assert.AreEqual(Solver.Solve("(TRUE OR TRUE) AND (TRUE)"), "TRUE");
            Assert.AreEqual(Solver.Solve("((TRUE OR TRUE) AND (TRUE))"), "TRUE");
            Assert.AreEqual(Solver.Solve("(((TRUE OR TRUE) AND (TRUE)))"), "TRUE");
            Assert.AreEqual(Solver.Solve("(((FALSE OR TRUE) AND (TRUE)))"), "TRUE");
        }

        [TestMethod]
        public void ComplexLogic()
        {
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND FALSE"), "FALSE");
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103))"), "TRUE");
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1))"), "FALSE");
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1-1))"), "TRUE");
        }

        [TestMethod]
        public void Parameters()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "'TEST'");
            Assert.AreEqual(Solver.Solve("VARIABLE = 'TEST'"), "TRUE");
            Assert.AreEqual(Solver.Solve("VARIABLE != 'TEST'"), "FALSE");

            //Null parameters comparison
            Solver.Parameters.Add("VARIABLE_NULL", null);
            Assert.AreEqual(Solver.Solve("VARIABLE_NULL = NULL"), "TRUE");
            Assert.AreEqual(Solver.Solve("VARIABLE_NULL != NULL"), "FALSE");

            Solver.Parameters.Add("x", "2");
            Assert.AreEqual(Solver.Solve("(x*x)/2"), "2");
        }

    }
}
