using System;
using System.Collections.Generic;
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
    /// <typeparam name="TInput"></typeparam>
    /// <param name="getResult"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>
    public static Result Concat<TInput>(Func<TInput, Result> getResult, IEnumerable<TInput> inputs)
    {
      var results = inputs.Select(getResult);
      return Concat(results);
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
  }
}
