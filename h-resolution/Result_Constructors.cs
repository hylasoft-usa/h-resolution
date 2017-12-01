using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hylasoft.Resolution
{
  public partial class Result
  {
    /// <summary>
    /// Creates a new result with a single issue.
    /// </summary>
    internal Result(ResultIssue issue)
      : this(new[] { issue })
    {
    }

    /// <summary>
    /// Creates a result from a collection of issues.
    /// </summary>
    internal Result(IEnumerable<ResultIssue> issues)
    {
      var validIssues = issues
        .Where(issue => !ReferenceEquals(issue, null) && issue.Level >= MinimumCollectionLevel)
        .ToArray();

      Issues = new Collection<ResultIssue>(validIssues);
    }

    /// <summary>
    /// Creates a result from an exception.
    /// </summary>
    public static Result Error(Exception e)
    {
      var messages = new List<string>();

      var stack = e == null
        ? string.Empty
        : e.StackTrace;

      for (; e != null; e = e.InnerException)
        messages.Add(e.Message);

      var issues = messages
        .Distinct()
        .Select(message => new ResultIssue(message, ResultIssueLevels.Error));

      var error = new Result(issues);
      if (!string.IsNullOrEmpty(stack))
        error += SingleDebug(stack);

      return error;
    }

    /// <summary>
    /// Returns an empty, successful instance of a result.
    /// </summary>
    public static Result Success
    {
      get
      {
        return new Result(new Collection<ResultIssue>());
      }
    }

    #region Singles
    /// <summary>
    /// Produces a result, with a single trace message.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleTrace(string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Trace, ResultIssue.NonIssueCode, message, parameters);
    }

    /// <summary>
    /// Produces an issue coded result, with a single trace message.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleTrace(long issueCode, string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Trace, issueCode, message, parameters);
    }

    /// <summary>
    /// Produces a result, with a single debug message.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleDebug(string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Debug, ResultIssue.NonIssueCode, message, parameters);
    }

    /// <summary>
    /// Produces an issue coded result, with a single debug message.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleDebug(long issueCode, string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Trace, issueCode, message, parameters);
    }

    /// <summary>
    /// Produces a result, with a single info message.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleInfo(string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Info, ResultIssue.NonIssueCode, message, parameters);
    }

    /// <summary>
    /// Produces an issue coded result, with a single info message.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleInfo(long issueCode, string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Info, issueCode, message, parameters);
    }

    /// <summary>
    /// Produces a result, with a single warning message.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleWarning(string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Warning, ResultIssue.NonIssueCode, message, parameters);
    }

    /// <summary>
    /// Produces an issue coded result, with a single warning message.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleWarning(long issueCode, string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Warning, issueCode, message, parameters);
    }

    /// <summary>
    /// Produces a result, with a single error message.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleError(string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Error, ResultIssue.NonIssueCode, message, parameters);
    }

    /// <summary>
    /// Produces an issue coded result, with a single error message.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleError(long issueCode, string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Error, issueCode, message, parameters);
    }

    /// <summary>
    /// Produces a result, with a single fatal message.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleFatal(string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Fatal, ResultIssue.NonIssueCode, message, parameters);
    }

    /// <summary>
    /// Produces an issue coded result, with a single fatal message.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public static Result SingleFatal(long issueCode, string message, params object[] parameters)
    {
      return SingleResult(ResultIssueLevels.Fatal, issueCode, message, parameters);
    }

    private static Result SingleResult(ResultIssueLevels level, long issueCode, string message, params object[] parameters)
    {
      string msgFormat;
      Result formatResult;
      try
      {
        msgFormat = parameters == null || !parameters.Any()
          ? message
          : string.Format(message, parameters);

        formatResult = Success;
      }
      catch (Exception e)
      {
        msgFormat = message;
        formatResult = SingleWarning("Failed to format message '{0}'.", message) + Error(e);
      }

      var singleResult = new Result(new ResultIssue(msgFormat, level, issueCode));
      return Concat(formatResult, singleResult);
    }
    #endregion

    #region Appends
    /// <summary>
    /// Appends a result to the current one.
    /// </summary>
    public Result Append(Result result)
    {
      return Concat(this, result);
    }

    /// <summary>
    /// Executes a function that returns a result, and appends it to the current one.
    /// </summary>
    /// <param name="action">A function that returns a result.</param>
    public Result Append(Func<Result> action)
    {
      return Append(action());
    }

    /// <summary>
    /// Appends a single trace to the current result.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    /// <returns></returns>
    public Result AppendTrace(string message, params object[] parameters)
    {
      return Append(SingleTrace(message, parameters));
    }

    /// <summary>
    /// Appends a single issue coded trace to the current result.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendTrace(long issueCode, string message, params object[] parameters)
    {
      return Append(SingleTrace(issueCode, message, parameters));
    }

    /// <summary>
    /// Appends a single debug to the current result.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    /// <returns></returns>
    public Result AppendDebug(string message, params object[] parameters)
    {
      return Append(SingleDebug(message, parameters));
    }

    /// <summary>
    /// Appends a single issue coded debug to the current result.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendDebug(long issueCode, string message, params object[] parameters)
    {
      return Append(SingleDebug(issueCode, message, parameters));
    }

    /// <summary>
    /// Appends a single info to the current result.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendInfo(string message, params object[] parameters)
    {
      return Append(SingleInfo(message, parameters));
    }

    /// <summary>
    /// Appends a single issue coded info to the current result.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendInfo(long issueCode, string message, params object[] parameters)
    {
      return Append(SingleInfo(issueCode, message, parameters));
    }

    /// <summary>
    /// Appends a single warning to the current result.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendWarning(string message, params object[] parameters)
    {
      return Append(SingleWarning(message, parameters));
    }

    /// <summary>
    /// Appends a single issue coded warning to the current result.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    /// <returns></returns>
    public Result AppendWarning(long issueCode, string message, params object[] parameters)
    {
      return Append(SingleWarning(issueCode, message, parameters));
    }

    /// <summary>
    /// Appends a single error to the current result.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendError(string message, params object[] parameters)
    {
      return Append(SingleError(message, parameters));
    }

    /// <summary>
    /// Appends a single issue coded error to the current result.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendError(long issueCode, string message, params object[] parameters)
    {
      return Append(SingleError(issueCode, message, parameters));
    }

    /// <summary>
    /// Appends a single fatal message to the current result.
    /// </summary>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    public Result AppendFatal(string message, params object[] parameters)
    {
      return Append(SingleFatal(message, parameters));
    }

    /// <summary>
    /// Appends a single issue coded fatal to the current result.
    /// </summary>
    /// <param name="issueCode">The unique, identifying issue code.</param>
    /// <param name="message">Either the complete message, or the message's format string.  Following String.Format() convention.</param>
    /// <param name="parameters">(Optional) arguments to a message format string.</param>
    /// <returns></returns>
    public Result AppendFatal(long issueCode, string message, params object[] parameters)
    {
      return Append(SingleFatal(issueCode, message, parameters));
    }
    #endregion

    #region Subsets
    /// <summary>
    /// Returns a subset of the Result, based on the issues that match the condition.
    /// </summary>
    /// <param name="source">The source Result.</param>
    /// <param name="condition">The condition to check for each issue.</param>
    public static Result Where(Result source, Func<ResultIssue, bool> condition)
    {
      return source == null
        ? null
        : new Result(source.Where(condition));
    }
    #endregion
  }
}
