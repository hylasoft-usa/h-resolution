h-resolution
==========

A lightweight error collection framework.

## Requirements
h-resolution works with .NET Framework 4.5 and higher

## Usage

Have validation, service, and/or domain methods return a `Hylasoft.Resolution.Result` object.

````C#
public Result Validate()
{
	return conditionMet()
		? Result.Info("Validation passed.")
		: Result.Error("Validation did not pass.");
}
````

Concatenate Results by using the addition operator.

````C#
public Result Validate()
{
	var first = ValidateFirst();
	var second = ValidateSecond();

	return first + second;
}
````

Or by using Result.Concat().

````C#
public Result Validate()
{
	return Result.Concat(ValidateFirst, ValidateSecond, ValidateThird);
}

public Result ValidateWithParam<T>(T param)
{
	return Result.Concat(param, ValidateFirst, ValidateSecond, ValidateThird);
}
````

Result.ConcatRestricted() can be used in the same way, but will stop execution on the first failure.

````C#
public Result Validate()
{
	return Result.ConcatRestricted(ValidateFirst, ValidateSecond, ValidateThird);
}
````

Results can be built from exceptions.

````C#
public Result Validate()
{
	try
	{
		ValidateFirst();
		ValidateSecond();

		return Result.Success;
	}
	catch (Exception e)
	{
		return Result.Error(e);
	}
}
````

Results can be implicitly cast to bool.

````C#
public Result Validate()
{
	Result first;
	return (first = ValidateFirst())
		? first + ValidateSecond()
		: first.AppendError("The first phase of validation failed.");
}
````

Failure is defined as a result containing any Result Issue with a level higher than a warning.

````C#
	if (Result.Trace("Trace message"); // true

	if (Result.SingleDebug("Debug message"); // true

	if (Result.SingleInfo("Info message"); // true

	if (Result.SingleWarning("Warning message"); // true

	if (Result.SingleError("Error message"); // false

	if (Result.SingleFatal("Fatal message"); // false
````

Results are treated as IEnumerable<ResultIssue>

````C#
	var result = Validate();
	var errors = result.Where(issue => issue.Level > ResultLevels.Warning).ToList();
````

Result issues can be compared to each other, and equated against longs, strings, or other result issues.

````C#
	var result = Validate();
	var result2 = ValidateSecond();

	var distinctIssues = result.Distinct().ToList();
	var exclusions = result.Except(result2).ToList();
	
	var targetCode = 5;
	var targetMessage = Resources.Warnings.SpecificWarning;

	if (result.Contains(targetCode))
		result.AppendTrace("Result contains '{0}'.", targetCode);

	if (result.Contains(targetMessage))
		result.AppendTrace("Result contains specific message.");

	if (result.Contains(result2.Max())
		result.AppendTrace("Max result2 included in first.");
````

## Build

You can build the project using Visual Studio or by running the grunt tasks for `msbuild`

## Contribute

This project uses [hylasoft/cs-boilerplate](https://github.com/hylasoft-usa/cs-boilerplate) to define tasks and stle guides. Please read the readme of the project to learn more about how to contribute.
