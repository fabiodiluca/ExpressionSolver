# ExpressionSolver
An logical expression solver with a sql like synthax


You pass a logical expression or a numeric and than you get it back resolved!

Example1:
	Expression:TRUE AND FALSE
	Return: FALSE


Example2:
	Expression: TRUE AND FALSE
	Return: FALSE
	((TRUE AND FALSE) OR TRUE) AND FALSE
	Solving (TRUE AND FALSE)
	Solving: TRUE AND FALSE
	( FALSE  OR TRUE) AND FALSE
	( FALSE  OR TRUE) AND FALSE
	Solving ( FALSE  OR TRUE)
	Solving: FALSE  OR TRUE
	TRUE  AND FALSE
	TRUE  AND FALSE
	Solving: TRUE  AND FALSE
	Result: FALSE
	 ( 1ms )

Example 3:
	Expression: (100*100)*-1
	Return: (100*100)*-1
	Solving (100*100)
	Solving: 100*100
	10000 *-1
	10000 *-1
	Solving: 10000 *-1
	Result: -10000
	 ( 1ms )

See unit test code to get more examples, you can even pass parameters and work with variables!