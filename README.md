# Overview
## ExpressionSolver is a .NET C# logical expression solver with a sql like syntax.

You pass a logical expression or a numeric and then you get it back resolved!

It is intended to be compatible with [http://querybuilder.js.org/](http://querybuilder.js.org/) combined with a sql expression plugin generator (see [http://querybuilder.js.org/demo.html](http://querybuilder.js.org/demo.html) Import/Export example.

***

### Example 1: Simplest logical test

**Source Code C#**
```sh
Solver Solver = new Solver();
string Return = Solver.Solve("TRUE AND FALSE");
```
**Result**
```sh
	Expression: TRUE AND FALSE
	Return: FALSE
        Log:
        Solving Primary Member: TRUE AND FALSE
        Result: FALSE
         ( 0ms )
```

***

### Example 2: Equal operator comparisons
**Source Code C#**
```sh
Solver Solver = new Solver();
string Return = Solver.Solve("('My Test String'='My Test String') AND (999=999) AND TRUE");
```
**Result**
```sh
	Expression: ('My Test String'='My Test String') AND (999=999) AND TRUE
	Return: TRUE
	Log:
	Solving Parenthesis ( 'My Test String'='My Test String' )
	Solving Primary Member: 'My Test String'='My Test String'
	Current expression: TRUE AND ( 999=999 ) AND TRUE
	Solving Parenthesis ( 999=999 )
	Solving Primary Member: 999=999
	Current expression: TRUE AND TRUE AND TRUE
	Solving Primary Member: TRUE AND TRUE
	Solving Primary Member: TRUE AND TRUE
	Result: TRUE
```

***

### Example 3: Simple Math
**Source Code C#**
```sh
Solver Solver = new Solver();
string Return = Solver.Solve("(100*100)*-1");
```
**Result**
```sh
	Expression: (100*100)*-1
	Return: -10000
	Log:
	Solving Parenthesis ( 100*100 )
	Solving Primary Member: 100*100
	Current expression: 10000 *-1
	Solving Primary Member: 10000 *-1
	Result: -10000
```

***

### Example 4: IN Operator
**Source Code C#**
```sh
Dictionary<string, string> Variables = new Dictionary<string, string>();
Variables.Add("MY_VARIABLE", "'INSIDE'");
string Return = Solver.Solve("(MY_VARIABLE IN ('A','B','INSIDE','D'))",Variables);
```
**Result**
```sh
	Expression: (MY_VARIABLE IN ('A','B','INSIDE','D'))
	Return: TRUE
	Log:
	Solving Parenthesis ( MY_VARIABLE IN ( 'A','B','INSIDE','D' ) )
	Solving Primary Member: MY_VARIABLE IN ( 'A','B','INSIDE','D' )
	Current expression: TRUE
```

***

# More Examples
See 'ExpressionSolverExample' project and unit test project for more examples.