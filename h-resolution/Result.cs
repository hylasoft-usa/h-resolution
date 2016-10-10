using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hylasoft.Resolution
{
  /// <summary>
  /// An immutable collection of result issues.
  /// </summary>
  public partial class Result : ResolutionComponent, IEnumerable<ResultIssue>
  {
    /// <summary>
    /// Issues belinging to the result.
    /// </summary>
    public Collection<ResultIssue> Issues { get; private set; }

    private bool? _isSuccessful;
    private Collection<ResultIssue> _messages;

    /// <summary>
    /// The global, minimum level of issues to collect in any given Result.
    /// </summary>
    public static ResultIssueLevels MinimumCollectionLevel { get; set; }

    /// <summary>
    /// Determines whether the result was successful.  Success is defined as the absence of issues above a warning level.
    /// </summary>
    public bool IsSuccessful
    {
      get { return _isSuccessful ?? (_isSuccessful = this.All(issue => issue == null || issue.Level <= ResultIssueLevels.Warning)).Value; }
    }

    /// <summary>
    /// Returns any distinct messages (non-fallacies) that exist in the result.
    /// </summary>
    public Collection<ResultIssue> Messages
    {
      get { return _messages ?? (_messages = new Collection<ResultIssue>(Issues.Distinct().Where(issue => issue.IsMessage).ToArray())); }
    }

    /// <summary>
    /// Returns whether any of the issues contain a message.
    /// </summary>
    public bool Contains(string message)
    {
      return Issues.Any(issue => issue == message);
    }

    /// <summary>
    /// Returns whether any of the issues contain a unique, identifying issue code.
    /// </summary>
    public bool Contains(long issueCode)
    {
      return issueCode != ResultIssue.NonIssueCode && Issues.Any(issue => issue == issueCode);
    }

    /// <summary>
    /// Produces an exception from a result, ordered by descending severity.
    /// </summary>
    public ResultException ToException()
    {
      return Issues
        .Where(issue => !ReferenceEquals(issue, null))
        .OrderByDescending(issue => issue.Level)
        .Aggregate<ResultIssue, ResultException>(null, (inner, issue) => new ResultException(issue, inner));
    }

    protected override string ComponentName
    {
      get { return "Result"; }
    }

    protected override string ComponentIdentity
    {
      get
      {
        const string success = "Success";

        if (Issues == null || !Issues.Any())
          return success;

        var maxIssue = Issues.Where(issue => !ReferenceEquals(issue, null))
          .OrderByDescending(issue => issue.Level)
          .FirstOrDefault();

        return ReferenceEquals(maxIssue, null)
          ? success
          : maxIssue.Message;
      }
    }

    public IEnumerator<ResultIssue> GetEnumerator()
    {
      return Issues.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
