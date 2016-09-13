using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Hylasoft.Resolution
{
  /// <summary>
  /// An exception built from a result issue.
  /// </summary>
  [Serializable]
  public class ResultException : Exception
  {
    /// <summary>
    /// The level of the issue.
    /// </summary>
    public ResultIssueLevels Level { get; private set; }

    #region Constructors
    public ResultException(ResultIssue issue, Exception inner) : this(ExtractMessage(issue), inner)
    {
      Level = issue == null ? ResultIssueLevels.Unknown : issue.Level;
    }

    protected ResultException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ResultException() : this(null, null)
    {
    }

    public ResultException(string message) : this(message, null)
    {
    }

    public ResultException(string message, Exception inner) : base(message, inner)
    {
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");

      info.AddValue("Level", Level);
      base.GetObjectData(info, context);
    }
    #endregion

    private static string ExtractMessage(ResultIssue issue)
    {
      return issue == null || !issue.IsMessage
        ? string.Empty
        : issue.Message;
    }
  }
}
