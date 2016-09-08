using System.Collections.Generic;

namespace Hylasoft.Resolution
{
  public partial class Result
  {
    /// <summary>
    /// Returns whether the result is successful.
    /// </summary>
    public static implicit operator bool(Result result)
    {
      return result.IsSuccessful;
    }

    /// <summary>
    /// Concatonates two results via the addition operator.
    /// </summary>
    public static Result operator +(Result a, Result b)
    {
      return Concat(a, b);
    }

    /// <summary>
    /// Concatonates a result and issue via the addition operator.
    /// </summary>
    public static Result operator +(Result result, ResultIssue issue)
    {
      return Concat(result, new Result(issue));
    }

    /// <summary>
    /// Concatonates an issue and result via the addition operator.
    /// </summary>
    public static Result operator +(ResultIssue issue, Result result)
    {
      return result + issue;
    }

    /// <summary>
    /// Concatonates a result and a collection of issues via the addition operator.
    /// </summary>
    public static Result operator +(Result result, IEnumerable<ResultIssue> issues)
    {
      return issues is Result
        ? result + ((Result)issues)
        : Concat(result, new Result(issues));
    }

    /// <summary>
    /// Concatonates a collection of issues and a result via the addition operator.
    /// </summary>
    public static Result operator +(IEnumerable<ResultIssue> issues, Result result)
    {
      return result + issues;
    }
  }
}
