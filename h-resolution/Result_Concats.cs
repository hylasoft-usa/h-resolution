using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hylasoft.Resolution
{
  public partial class Result
  {
    /// <summary>
    /// Concatonates an array of results.
    /// </summary>
    public static Result Concat(params Result[] results)
    {
      return Concat((IEnumerable<Result>)results);
    }

    /// <summary>
    /// Concatonates a collection of results.
    /// </summary>
    public static Result Concat(IEnumerable<Result> results)
    {
      var issues = new List<ResultIssue>(results.SelectMany(res => res.Issues));
      return new Result(issues);
    }

    /// <summary>
    /// Concatonates a list of result functions.
    /// </summary>
    public static Result Concat(params Func<Result>[] resultFunctions)
    {
      return Concat(resultFunctions.Select(resFunc => resFunc()));
    }

    /// <summary>
    /// Concatonates a series of results, from a collection of functions, that all take a single parameter.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="value">The input parameter to each function.</param>
    /// <param name="resultFunctions">A collection of functions to call that yield results.</param>
    public static Result Concat<TInput>(TInput value, params Func<TInput, Result>[] resultFunctions)
    {
      return Concat(resultFunctions.Select(resFunc => resFunc(value)));
    }

    /// <summary>
    /// Concatonates a series of results, from a collection of functions.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="getResult">The method to call to yield a result.</param>
    /// <param name="inputs">A collection of input values to pass to the method.</param>
    /// <returns></returns>
    public static Result Concat<TInput>(Func<TInput, Result> getResult, params TInput[] inputs)
    {
      var results = inputs == null
        ? new Result[0]
        : inputs.Select(getResult);

      return Concat(results);
    }

    /// <summary>
    /// Concatonates a series of results, from a collection of functions.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="getResult">The method to call to yield a result.</param>
    /// <param name="inputs">A collection of input values to pass to the method.</param>
    /// <returns></returns>
    public static Result Concat<TInput>(Func<TInput, Result> getResult, IEnumerable<TInput> inputs)
    {
      return Concat(getResult, inputs == null ? new TInput[0] : inputs.ToArray());
    }

    /// <summary>
    /// Concatonates a list of result functions, stopping after the first failure.
    /// </summary>
    public static Result ConcatRestricted(params Func<Result>[] resultFunctions)
    {
      var issues = new List<ResultIssue>();
      foreach (var resFunc in resultFunctions)
      {
        var res = resFunc();
        issues.AddRange(res.Issues);
        if (!res) break;
      }

      return new Result(issues);
    }

    /// <summary>
    /// Concatonates a list of result functions, stopping after the first failure.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "A more streamlined design does exist, using arrays.  But this also exists to suppliment it.")]
    public static Result ConcatRestricted(IEnumerable<Func<Result>> resultFunctions)
    {
      return resultFunctions == null
        ? ConcatRestricted(new Func<Result>[0])
        : ConcatRestricted(resultFunctions.ToArray());
    }

    /// <summary>
    /// Concatonates a series of results, from a collection of functions, that all take a single parameter.  Stops after first failure.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="value">The input parameter to each function.</param>
    /// <param name="resultFunctions">A collection of functions to call that yield results.</param>
    /// <returns></returns>
    public static Result ConcatRestricted<TInput>(TInput value, params Func<TInput, Result>[] resultFunctions)
    {
      return resultFunctions == null
        ? ConcatRestricted(null)
        : ConcatRestricted(resultFunctions.Select(rf => (Func<Result>)(() => rf(value))));
    }

    /// <summary>
    /// Concatonates a series of results, from a collection of functions, that all take a single parameter.  Stops after first failure.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="value">The input parameter to each function.</param>
    /// <param name="resultFunctions">A collection of functions to call that yield results.</param>
    /// <returns></returns>
    [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "A more streamlined design does exist, using arrays.  But this also exists to suppliment it.")]
    public static Result ConcatRestricted<TInput>(TInput value, IEnumerable<Func<TInput, Result>> resultFunctions)
    {
      return ConcatRestricted(value, resultFunctions == null ? new Func<TInput, Result>[0] : resultFunctions.ToArray());
    }

    /// <summary>
    /// Concatonates a series of results, from a single method, and a collection of inputs.  Stops after the first failure.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="getResult">The method to call to yield a result.</param>
    /// <param name="inputs">A collection of input values to pass to the method.</param>
    /// <returns></returns>
    public static Result ConcatRestricted<TInput>(Func<TInput, Result> getResult, params TInput[] inputs)
    {
      return inputs == null || getResult == null
        ? ConcatRestricted(null)
        : ConcatRestricted(inputs.Select(input => (Func<Result>)(() => getResult(input))));
    }

    /// <summary>
    /// Concatonates a series of results, from a single method, and a collection of inputs.  Stops after the first failure.
    /// </summary>
    /// <typeparam name="TInput">The type of the parameter for the function calls.</typeparam>
    /// <param name="getResult">The method to call to yield a result.</param>
    /// <param name="inputs">A collection of input values to pass to the method.</param>
    /// <returns></returns>
    public static Result ConcatRestricted<TInput>(Func<TInput, Result> getResult, IEnumerable<TInput> inputs)
    {
      return ConcatRestricted(getResult, inputs == null ? new TInput[0] : inputs.ToArray());
    }
  }
}
