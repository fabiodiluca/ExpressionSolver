using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionSolver;
using System.Globalization;

namespace ExpressionSolverUnitTest
{
    [TestClass]
    public class UnitTest
    {
        Solver Solver = new Solver(new CultureInfo("en-US"));
        [TestMethod]
        public void TestTRUE()
        {
            Assert.AreEqual(Solver.Solve("TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("tRuE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("true"), Solver.TRUE);
        }

        [TestMethod]
        public void TestFALSE()
        {
            Assert.AreEqual(Solver.Solve("FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("fAlSe"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("falSe"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("false"), Solver.FALSE);
        }

        [TestMethod]
        public void TestAND()
        {
            Assert.AreEqual(Solver.Solve("TRUE AND TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("FALSE AND TRUE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("FALSE AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("TRUE AND TRUE AND TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TrUE ANd trUE anD TRue"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE AND FALSE AND TRUE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("TRUE AND TRUE AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("TRUE aND TRUE And FALSE"), Solver.FALSE);
        }

        [TestMethod]
        public void TestOR()
        {
            Assert.AreEqual(Solver.Solve("TRUE OR TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE oR TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE or TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE OR FALSE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE OR TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE OR FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("TRUE OR TRUE OR TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE OR FALSE OR TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE OR TRUE OR FALSE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE OR FALSE OR FALSE"), Solver.FALSE);
        }

        [TestMethod]
        public void TestEqual()
        {
            Assert.AreEqual(Solver.Solve("TRUE = TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE = FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("FALSE = TRUE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("FALSE = FALSE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1 = 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' = 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' = '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1 = '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2 = 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' = 2"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'2' = '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 = '2'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 = '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.0 = '1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.00 = '1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.00 = '1.000'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.000 = '1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.000 = '1.009'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("TRUE=TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE=FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("FALSE=TRUE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("FALSE=FALSE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1=1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1'=1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1'='1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1='1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2=1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1'=2"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'2'='1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1='2'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1='1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.0='1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.00='1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.00='1.000'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.000='1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.000='1.009'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'A'='B'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'A'='a'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'A'='A'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE'='TESTE'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE'=' TESTE'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1=' TESTE'"), Solver.FALSE);
        }

        [TestMethod]
        public void TestString()
        {
            Assert.AreEqual(Solver.Solve("'A' = 'A'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'A'='A'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("' A'='A'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("''''=''''"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'T''E'='T''E'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("''''''=''''"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("''''''=''''''"), Solver.TRUE);
        }

        [TestMethod]
        public void TestStringVersusNumber()
        {
            Assert.AreEqual(Solver.Solve("1 = 00001"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' = '00001'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 = '00001'"), Solver.TRUE);
        }

        [TestMethod]
        public void TestDifferent()
        {
            Assert.AreEqual(Solver.Solve("TRUE != TRUE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("TRUE != FALSE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("TRUE!=FALSE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE != TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE !=TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE!=TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("FALSE != FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 != 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' != 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' != '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 != '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("2 != 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' != 2"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'2' != '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1 != '2'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.0 != '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.00 != '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.00 != '1.000'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 != '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 != '1.009'"), Solver.TRUE);
        }

        [TestMethod]
        public void TestGreater()
        {
            Assert.AreEqual(Solver.Solve("1 > 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' > 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' > '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 > '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("2 > 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2> 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2 >1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2>1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2   >  1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' > 2"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'2' > '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1 > '2'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.0 > '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.00 > '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.00 > '1.000'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 > '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 > '1.009'"), Solver.FALSE);
        }

        [TestMethod]
        public void TestGreaterOrEqual()
        {
            Assert.AreEqual(Solver.Solve("1 >= 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' >= 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' >= '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1 >= '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("2 >= 1"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'1' >= 2"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'2' >= '1'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1 >= '2'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.0 >= '1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.0 >='1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.0>='1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.00 >= '1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.00 >= '1.000'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.000 >= '1.00'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.000 >= '1.009'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 >='1.009'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000>='1.009'"), Solver.FALSE);
        }

        [TestMethod]
        public void TestLess()
        {
            Assert.AreEqual(Solver.Solve("1 < 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1<1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' < 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1'< 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' < '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 < '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("2 < 1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("2 <1"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'1' < 2"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'2' < '1'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1 < '2'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("1.0 < '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.00 < '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.00 < '1.000'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 < '1.00'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("1.000 < '1.009'"), Solver.TRUE);
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
            Assert.AreEqual(Solver.Solve("1/ 1"), "1");
            Assert.AreEqual(Solver.Solve("1 / 1"), "1");
            Assert.AreEqual(Solver.Solve("2/2"), "1");
            Assert.AreEqual(Solver.Solve("9/3"), "3");
            Assert.AreEqual(Solver.Solve("100/5"), "20");
            Assert.AreEqual(Solver.Solve("-100/5"), "-20");
            Assert.AreEqual(Solver.Solve("-100/-5"), "20");
            Assert.AreEqual(Solver.Solve("-100/ -5"), "20");
            Assert.AreEqual(Solver.Solve("-100 / -5"), "20");
        }

        [TestMethod]
        public void TestMath()
        {
            Assert.AreEqual(Solver.Solve("(((100/5)/20)*5)+10"), "15");
            Assert.AreEqual(Solver.Solve("(((100 / 5)   /   20)   *5    )+10"), "15");
        }

        [TestMethod]
        public void TestNotLike()
        {
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE '%EST%'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'TESTE' nOT lIKE '%EST%'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE 'TEST%'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE '%TESTE%'"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("'TESTE' NOT LIKE 'TTEST%'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE' noT lIkE 'TTEST%'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE' noT lIkE    'TTEST%'"), Solver.TRUE);
        }

        [TestMethod]
        public void TestLike()
        {
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE '%EST%'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE 'TEST%'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE' lIkE 'TEST%'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE' like '%TESTE%'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("'TESTE' LIKE 'TTEST%'"), Solver.FALSE);
        }

        [TestMethod]
        public void TestIN()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('A','B','TESTE')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE iN ('A','B','TESTE')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE in ('A','B','TESTE')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE in('A','B','TESTE')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE in    ('A' ,'B' ,   'TESTE')"), Solver.TRUE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('A','B','C','D')"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("VARIABLE IN('A','B','C','D')"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("VARIABLE in(   'A'   ,'B','C','D')"), Solver.FALSE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('TESTE')"), Solver.TRUE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN (' AND ')"), Solver.TRUE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE IN ('A','B')"), Solver.TRUE);
        }

        [TestMethod]
        public void TestNotIN()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('A','B','TESTE')"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("VARIABLE nOT iN ('A','B','TESTE')"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("VARIABLE nOT iN('A','B','TESTE')"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("VARIABLE not in    ('A','B','TESTE')"), Solver.FALSE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('A','B','C','D')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE nOt in ('A','B','C','D')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE nOt in('A','B','C','D')"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE nOt in('A',  'B'  ,'C','D')"), Solver.TRUE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','TESTE')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('TESTE')"), Solver.FALSE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN (' AND ')"), Solver.FALSE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','B',' AND ')");
            Assert.AreEqual(Solver.Solve("VARIABLE NOT IN ('A','B')"), Solver.FALSE);
        }

        [TestMethod]
        public void TestIsNull()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", null);
            Solver.Parameters.Add("VARIABLE_2", "null");
            Assert.AreEqual(Solver.Solve("VARIABLE IS NULL"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE iS nULL"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE    iS nULL"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE    is null"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE IS NULL AND VARIABLE_2 IS NULL"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE IS null   and VARIABLE_2 IS NULL"), Solver.TRUE);
        }

        [TestMethod]
        public void TestIsNotNull()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "'this is a string'");
            Solver.Parameters.Add("VARIABLE_2", "387");
            Assert.AreEqual(Solver.Solve("VARIABLE IS not NULL"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE IS not NULL AND VARIABLE_2 IS not NULL"), Solver.TRUE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", null);
            Solver.Parameters.Add("VARIABLE_2", "387");
            Assert.AreEqual(Solver.Solve("VARIABLE IS not NULL"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("VARIABLE IS not NULL OR VARIABLE_2 Is not NULL"), Solver.TRUE);

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", null);
            Solver.Parameters.Add("VARIABLE_2", null);
            Assert.AreEqual(Solver.Solve("VARIABLE IS not NULL OR VARIABLE_2 iS not NULL"), Solver.FALSE);
        }

        [TestMethod]
        public void Parenthesis()
        {
            Assert.AreEqual(Solver.Solve("(TRUE)"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(FALSE)"), Solver.FALSE);

            Assert.AreEqual(Solver.Solve("((TRUE))"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(((FALSE)))"), Solver.FALSE);

            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE)"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE) AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE)AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("((TRUE AND TRUE) AND FALSE)"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("((TRUE AND TRUE)AND FALSE)"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("(TRUE AND TRUE AND FALSE)"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("(TRUE AND (TRUE AND (FALSE)))"), Solver.FALSE);

            Assert.AreEqual(Solver.Solve("(TRUE OR TRUE) AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("(TRUE OR TRUE) AND TRUE"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(TRUE OR TRUE) AND (TRUE)"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("((TRUE OR TRUE) AND (TRUE))"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(((TRUE OR TRUE) AND (TRUE)))"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(((FALSE OR TRUE) AND (TRUE)))"), Solver.TRUE);
        }

        [TestMethod]
        public void ComplexLogic()
        {
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND FALSE"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103))"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1))"), Solver.FALSE);
            Assert.AreEqual(Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1-1))"), Solver.TRUE);
        }

        [TestMethod]
        public void Parameters()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "'TEST'");
            Assert.AreEqual(Solver.Solve("VARIABLE = 'TEST'"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE != 'TEST'"), Solver.FALSE);

            //Null parameters comparison
            Solver.Parameters.Add("VARIABLE_NULL", null);
            Assert.AreEqual(Solver.Solve("VARIABLE_NULL = NULL"), Solver.TRUE);
            Assert.AreEqual(Solver.Solve("VARIABLE_NULL != NULL"), Solver.FALSE);

            Solver.Parameters.Add("x", "2");
            Assert.AreEqual(Solver.Solve("(x*x)/2"), "2");
        }

    }
}
