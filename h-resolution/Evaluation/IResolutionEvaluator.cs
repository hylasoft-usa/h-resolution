using System.Collections.Generic;

namespace Hylasoft.Resolution.Evaluation
{
  public interface IResolutionEvaluator
  {
    /// <summary>
    /// Attempts to determine which issue is the most relevent in a given result.
    /// </summary>
    /// <param name="issues">A collection of issues to evaluate.</param>
    /// <returns></returns>
    ResultIssue MostRevelant(IEnumerable<ResultIssue> issues);
  }
}
