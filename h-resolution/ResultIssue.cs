using System;
using System.Text;

namespace Hylasoft.Resolution
{
  public partial class ResultIssue : ResolutionComponent
  {
    /// <summary>
    /// The issue code value for an issue that doesn't have a code.
    /// </summary>
    internal const long NonIssueCode = 0;

    /// <summary>
    /// Nature and/or severity of issue.
    /// </summary>
    public ResultIssueLevels Level { get; private set; }

    /// <summary>
    /// Message identifying and associated with the issue.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// The time of instantiation of the issue.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// A unique code that identifies a specific issue.
    /// </summary>
    public long IssueCode { get; private set; }

    /// <summary>
    /// Builds a new result issue.
    /// </summary>
    /// <param name="message">Message identifying the issue.</param>
    /// <param name="level">Severity of the issue.</param>
    /// <param name="issueCode">A unique, identifying code to be associated with the issue.</param>
    internal ResultIssue(string message, ResultIssueLevels level, long issueCode = NonIssueCode)
    {
      Message = message ?? string.Empty;
      Level = level;
      Date = DateTime.Now;
      IssueCode = issueCode;
    }

    /// <summary>
    /// Specifies whether the issue represents a more generic message or result.
    /// </summary>
    public bool IsMessage
    {
      get { return Level != ResultIssueLevels.Unknown && !string.IsNullOrEmpty(MessageFormat(Message)); } 
    }

    protected override string ComponentName
    {
      get { return Level.ToString(); }
    }

    protected override string ComponentIdentity
    {
      get { return Message; }
    }

    /// <summary>
    /// Transforms a message into a common, reduced format for comparison.
    /// </summary>
    protected string MessageFormat(string val)
    {
      if (val == null)
        return string.Empty;

      var builder = new StringBuilder(val.ToLower().Trim());
      StripChar(builder, ',');
      StripChar(builder, '.');
      StripChar(builder, ';');
      StripChar(builder, '!');
      StripChar(builder, '\'');

      return builder.ToString();
    }

    /// <summary>
    /// Replaces a character with a space, within a string builder.
    /// </summary>
    protected void StripChar(StringBuilder builder, char val)
    {
      builder.Replace(val, ' ');
    }
  }
}