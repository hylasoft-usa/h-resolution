using System;
using System.Collections.Generic;
using Hylasoft.Resolution.Evaluation;

namespace Hylasoft.Resolution.Extensions
{
  public static class ResultExtensions
  {
    private static IResolutionEvaluator Evaluator { get { return ResolutionEvaluator.Evaluator; } }

    /// <summary>
    /// Retrieves the most relevant issue.
    /// </summary>
    /// <param name="issues">A set of issues.</param>
    public static ResultIssue GetMostRelevant(this IEnumerable<ResultIssue> issues)
    {
      return Evaluator.MostRevelant(issues);
    }

    /// <summary>
    /// Returns a subset of the Result, based on the issues that match the condition.
    /// </summary>
    /// <param name="result">The source Result.</param>
    /// <param name="condition">The condition to check for each issue.</param>
    public static Result Where(this Result result, Func<ResultIssue, bool> condition)
    {
      return Result.Where(result, condition);
    }
  }
}
