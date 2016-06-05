# ExpressionSolver
A .NET C# logical expression solver with a sql like synthax.


You pass a logical expression or a numeric and then you get it back resolved! 
It is intended to be compatible with http://querybuilder.js.org/ sql expression generator.

Example1:
	Expression:TRUE AND FALSE
	Return: FALSE


Example2:
	Expression: ('My Test String'='My Test String') AND (999=999) AND TRUE
	Return: TRUE
	Log:
	('My Test String'='My Test String') AND (999=999) AND TRUE
	Solving ('My Test String'='My Test String')
	Solving: 'My Test String'='My Test String'
	TRUE  AND (999=999) AND TRUE
	TRUE  AND (999=999) AND TRUE
	Solving (999=999)
	Solving: 999=999
	TRUE  AND TRUE  AND TRUE
	TRUE  AND TRUE  AND TRUE
	Solving: TRUE  AND TRUE
	Solving: TRUE  AND TRUE
	Result: TRUE
	 ( 0ms )

Example 3:
	Expression: (100*100)*-1
	Return: -10000
	Log:
	(100*100)*-1
	Solving (100*100)
	Solving: 100*100
	10000 *-1
	10000 *-1
	Solving: 10000 *-1
	Result: -10000
	 ( 1ms )

See unit test code to get more examples, you can even pass parameters and work with variables!