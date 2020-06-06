using System;
using ExpressionSolver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionSolverUnitTest
{
    [TestClass]
    public class UnitTest
    {
        Solver Solver = new Solver();
        [TestMethod]
        public void TestTRUE()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("tRuE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("true"));
        }

        [TestMethod]
        public void TestFALSE()
        {
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("fAlSe"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("falSe"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("false"));
        }

        [TestMethod]
        public void TestAND()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE AND TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE AND FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE AND TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE AND FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE AND TRUE AND TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TrUE ANd trUE anD TRue"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE AND FALSE AND TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE AND TRUE AND FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE aND TRUE And FALSE"));
        }

        [TestMethod]
        public void TestOR()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE OR TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE oR TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE or TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE OR FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("FALSE OR TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE OR FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE OR TRUE OR TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE OR FALSE OR TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE OR TRUE OR FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE OR FALSE OR FALSE"));
        }

        [TestMethod]
        public void TestEqual()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE = TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE = FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE = TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("FALSE = FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 = 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' = 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' = '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 = '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("2 = 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' = 2"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'2' = '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 = '2'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 = '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.0 = '1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.00 = '1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.00 = '1.000'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.000 = '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 = '1.009'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE=TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE=FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE=TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("FALSE=FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1=1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1'=1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1'='1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1='1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("2=1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1'=2"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'2'='1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1='2'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1='1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.0='1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.00='1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.00='1.000'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.000='1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000='1.009'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'A'='B'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'A'='a'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'A'='A'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE'='TESTE'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'TESTE'=' TESTE'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1=' TESTE'"));
        }

        [TestMethod]
        public void TestString()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'A' = 'A'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'A'='A'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("' A'='A'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("''''=''''"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'T''E'='T''E'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("''''''=''''"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("''''''=''''''"));
        }

        [TestMethod]
        public void TestStringVersusNumber()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 = 00001"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' = '00001'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 = '00001'"));
        }

        [TestMethod]
        public void TestDifferent()
        {
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("TRUE != TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE != FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TRUE!=FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("FALSE != TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("FALSE !=TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("FALSE!=TRUE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("FALSE != FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 != 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' != 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' != '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 != '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2 != 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' != 2"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'2' != '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 != '2'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.0 != '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.00 != '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.00 != '1.000'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 != '1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.000 != '1.009'"));
        }

        [TestMethod]
        public void TestGreater()
        {
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 > 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' > 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' > '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 > '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2 > 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2> 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2 >1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2>1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2   >  1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' > 2"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'2' > '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 > '2'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.0 > '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.00 > '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.00 > '1.000'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 > '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 > '1.009'"));
        }

        [TestMethod]
        public void TestGreaterOrEqual()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 >= 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' >= 1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' >= '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 >= '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2 >= 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' >= 2"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'2' >= '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 >= '2'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.0 >= '1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.0 >='1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.0>='1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.00 >= '1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.00 >= '1.000'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.000 >= '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 >= '1.009'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 >='1.009'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000>='1.009'"));
        }

        [TestMethod]
        public void TestLess()
        {
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 < 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1<1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' < 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1'< 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'1' < '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1 < '1'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("2 < 1"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("2 <1"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'1' < 2"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'2' < '1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1 < '2'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.0 < '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.00 < '1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.00 < '1.000'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000 < '1.00'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1.000 < '1.009'"));
        }

        [TestMethod]
        public void TestPlus()
        {
            Assert.AreEqual("1", Solver.Solve("+1"));
            Assert.AreEqual("2", Solver.Solve("1+1"));
            Assert.AreEqual("3", Solver.Solve("1+1+1"));
            Assert.AreEqual("10", Solver.Solve("1 + 2 +3+4"));
            Assert.AreEqual("10", Solver.Solve("(1 + 2) +3+4"));
            Assert.AreEqual("1000000", Solver.Solve("999999+1"));
        }

        [TestMethod]
        public void TestMinus()
        {
            Assert.AreEqual("-1", Solver.Solve("-1"));
            Assert.AreEqual("0", Solver.Solve("1-1"));
            Assert.AreEqual("-1", Solver.Solve("1-1-1"));
            Assert.AreEqual("-10", Solver.Solve("-1 - 2 -3-4"));
            Assert.AreEqual("-10", Solver.Solve("(-1 - 2) -3-4"));
            Assert.AreEqual("-1000000", Solver.Solve("-999999-1"));
        }

        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual("1", Solver.Solve("1*1"));
            Assert.AreEqual("2", Solver.Solve("1*2"));
            Assert.AreEqual("1", Solver.Solve("1*1*1"));
            Assert.AreEqual("-2", Solver.Solve("-1*2"));
            Assert.AreEqual("2", Solver.Solve("-1*-2"));
            Assert.AreEqual("-24", Solver.Solve("-(1*  2) *3*4"));
            Assert.AreEqual("-999999", Solver.Solve("-999999*1"));
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual("1", Solver.Solve("1/1"));
            Assert.AreEqual("1", Solver.Solve("1/ 1"));
            Assert.AreEqual("1", Solver.Solve("1 / 1"));
            Assert.AreEqual("1", Solver.Solve("2/2"));
            Assert.AreEqual("3", Solver.Solve("9/3"));
            Assert.AreEqual("20", Solver.Solve("100/5"));
            Assert.AreEqual("-20", Solver.Solve("-100/5"));
            Assert.AreEqual("20", Solver.Solve("-100/-5"));
            Assert.AreEqual("20", Solver.Solve("-100/ -5"));
            Assert.AreEqual("20", Solver.Solve("-100 / -5"));
        }

        [TestMethod]
        public void TestPower()
        {
            Assert.AreEqual("1", Solver.Solve("1^1"));
            Assert.AreEqual("1", Solver.Solve("1^ 1"));
            Assert.AreEqual("1", Solver.Solve("1 ^ 1"));
            Assert.AreEqual("4", Solver.Solve("2^2"));
            Assert.AreEqual("729", Solver.Solve("9^3"));
        }

        [TestMethod]
        public void TestMath()
        {
            Assert.AreEqual("15", Solver.Solve("(((100/5)/20)*5)+10"));
            Assert.AreEqual("15", Solver.Solve("(((100 / 5)   /   20)   *5    )+10"));
        }

        [TestMethod]
        public void TestNotLike()
        {
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'TESTE' NOT LIKE '%EST%'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'TESTE' nOT lIKE '%EST%'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'TESTE' NOT LIKE 'TEST%'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'TESTE' NOT LIKE '%TESTE%'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' NOT LIKE 'TTEST%'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' noT lIkE 'TTEST%'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' noT lIkE    'TTEST%'"));
        }

        [TestMethod]
        public void TestLike()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' LIKE '%EST%'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' LIKE 'TEST%'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' lIkE 'TEST%'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("'TESTE' like '%TESTE%'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'TESTE' LIKE 'TTEST%'"));
        }

        [TestMethod]
        public void TestIN()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IN ('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE iN ('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE in ('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE in('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE in    ('A' ,'B' ,   'TESTE')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE IN ('A','B','C','D')"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE IN('A','B','C','D')"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE in(   'A'   ,'B','C','D')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','TESTE')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE IN ('TESTE')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B',' AND ')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE IN (' AND ')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','B',' AND ')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE IN ('A','B')"));
        }

        [TestMethod]
        public void TestNotIN()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE NOT IN ('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE nOT iN ('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE nOT iN('A','B','TESTE')"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE not in    ('A','B','TESTE')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('TESTE')");
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE NOT IN ('A','B','C','D')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE nOt in ('A','B','C','D')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE nOt in('A','B','C','D')"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE nOt in('A',  'B'  ,'C','D')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','TESTE')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE NOT IN ('TESTE')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B',' AND ')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE NOT IN (' AND ')"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "('A','B','B',' AND ')");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE NOT IN ('A','B')"));
        }

        [TestMethod]
        public void TestIsNull()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", null);
            Solver.Parameters.Add("VARIABLE_2", "null");
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IS NULL"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE iS nULL"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE    iS nULL"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE    is null"));
            //Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IS NULL AND VARIABLE_2 IS NULL"));
            //Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IS null   and VARIABLE_2 IS NULL"));
        }

        [TestMethod]
        public void TestIsNotNull()
        {
            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "'this is a string'");
            Solver.Parameters.Add("VARIABLE_2", "387");
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IS not NULL"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IS not NULL AND VARIABLE_2 IS not NULL"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", null);
            Solver.Parameters.Add("VARIABLE_2", "387");
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE IS not NULL"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE IS not NULL OR VARIABLE_2 Is not NULL"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", null);
            Solver.Parameters.Add("VARIABLE_2", null);
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(VARIABLE IS not NULL) OR (VARIABLE_2 iS not NULL)"));
        }

        [TestMethod]
        public void Parenthesis()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE)"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(FALSE)"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("((TRUE))"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(((FALSE)))"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE AND TRUE)"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE AND TRUE) AND FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE AND TRUE)AND FALSE"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("((TRUE AND TRUE) AND FALSE)"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("((TRUE AND TRUE)AND FALSE)"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE AND TRUE AND FALSE)"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE AND (TRUE AND (FALSE)))"));

            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE OR TRUE) AND FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE OR TRUE) AND TRUE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE OR TRUE) AND (TRUE)"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("((TRUE OR TRUE) AND (TRUE))"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(((TRUE OR TRUE) AND (TRUE)))"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(((FALSE OR TRUE) AND (TRUE)))"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE)AND(TRUE)AND(TRUE) AND TRUE"));
        }

        [TestMethod]
        public void ComplexLogic()
        {
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND FALSE"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103))"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1))"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("(TRUE AND ((TRUE OR FALSE) OR TRUE)) AND ((102+1) = (103+1-1))"));
            Assert.AreEqual("3", Solver.Solve("+-+-(--3)"));
        }

        [TestMethod]
        public void Parameters()
        {
            Solver.Parameters.Clear();
            //Solver.Parameters.Add("VARIABLE", "'TEST'");
            //Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE = 'TEST'"));
            //Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE != 'TEST'"));

            ////Null parameters comparison
            //Solver.Parameters.Add("VARIABLE_NULL", null);
            //Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE_NULL = NULL"));
            //Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE_NULL != NULL"));

            Solver.Parameters.Add("x", "2");
            Assert.AreEqual("2", Solver.Solve("(x*x)/2"));
        }

        [TestMethod]
        public void Multiline()
        {
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\rTRUE\n"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("tRuE\r\n"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("true\t\t\t"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("TrUE\rANd\n\ttrUE\ranD TRue"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\r\nTRUE\rOR\r\n\r\nFALSE\tOR\tTRUE"));

            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("'2'\r\n=\r\n\t'1'"));
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\r\n'TESTE'\r\n=\r\n'TESTE'"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\r\n''''''\t=\r\n''''''\r\n"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1\r=\n\r\t00001\r"));

            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("\r\nTRUE\n\n!=\r\tTRUE"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("2\r\n>\t\t\t\r\n1"));

            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000\t\t\r\n>\r\n\t\t\t'1.009'"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\t\t\r\n1.0\n >=\r\n'1.00'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.000\r\n>=\t\t\r\n'1.009'"));

            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("1.0\r\n\t\r\n<\n\n\n'1.00'\t\t\t"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("1\r\n\n\r<\t\n\r'2'"));

            Assert.AreEqual("10", Solver.Solve("\t(1\r\n+\t2)\r+\n\n3+\t4"));

            Assert.AreEqual("-10", Solver.Solve("\t\t\t\r\n(\t\t\r\n-1-\r\n2)\t\n\r-\r3-\n\n4"));

            Assert.AreEqual("-24", Solver.Solve("-\r\n(\n\n1\r\n*\r\n\n2)\r*3\n\r*\r\n4\t\t"));

            Assert.AreEqual("20", Solver.Solve("-\n\n\r\t100\r\n/\t\t-5\n\r"));

            Assert.AreEqual("729", Solver.Solve("\r\n9^\r\n3\r\n"));

            Assert.AreEqual("15", Solver.Solve("\n\n(\r\n(\t\t(\n\r\t100\n/   5)   /\r\n20  )\r\n*5\r\n)\t\t+\t10"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\r\n\t'\rTESTE'\t\r\nNOT LIKE\r\n'\rTTEST%'"));

            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("\r\t\n(TRUE\nAND\r\n((TRUE\r\n\tOR\tFALSE\r\n)\r\nOR    TRUE))\nAND   ((102+  1) = (103))"));

            Solver.Parameters.Clear();
            Solver.Parameters.Add("VARIABLE", "'TEST'");
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE =\r\n\t 'TEST'"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE\r\n !=\t 'TEST'"));

            //Null parameters comparison
            Solver.Parameters.Add("VARIABLE_NULL", null);
            Assert.AreEqual(TokenValueConstants.TRUE, Solver.Solve("VARIABLE_NULL = NULL"));
            Assert.AreEqual(TokenValueConstants.FALSE, Solver.Solve("VARIABLE_NULL != NULL"));

            Solver.Parameters.Add("x", "2");
            Assert.AreEqual("2", Solver.Solve("(x*x)/2"));
        }
    }
}
