using System.Collections.Generic;
using Hylasoft.Resolution.Evaluation;

namespace Hylasoft.Resolution.Extensions
{
  public static class ResultExtensions
  {
    private static IResolutionEvaluator Evaluator { get { return ResolutionEvaluator.Evaluator; } }

    public static ResultIssue GetMostRelevant(this IEnumerable<ResultIssue> issues)
    {
      return Evaluator.MostRevelant(issues);
    }
  }
}
