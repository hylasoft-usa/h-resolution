namespace Hylasoft.Resolution
{
  public partial class ResultIssue
  {
    /// <summary>
    /// Implicit casting of an issue to a string.
    /// </summary>
    public static implicit operator string(ResultIssue issue)
    {
      return ReferenceEquals(issue, null)
        ? string.Empty
        : issue.Message;
    }

    /// <summary>
    /// Inequality operator overload for result issues.
    /// </summary>
    public static bool operator !=(ResultIssue a, ResultIssue b)
    {
      return !(a == b);
    }

    /// <summary>
    /// Equality operator overload for result issues.
    /// </summary>
    public static bool operator ==(ResultIssue a, ResultIssue b)
    {
      if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        return true;

      return !ReferenceEquals(a, null) && a.Equals(b);
    }

    /// <summary>
    /// Inequality operator overload for a result issue and a message.
    /// </summary>
    public static bool operator !=(ResultIssue issue, string message)
    {
      return !(issue == message);
    }

    /// <summary>
    /// Equality operator overload for a result issue and a message.
    /// </summary>
    public static bool operator ==(ResultIssue issue, string message)
    {
      if (ReferenceEquals(issue, null) && ReferenceEquals(message, null))
        return true;

      return !ReferenceEquals(issue, null) && issue.Equals(message);
    }

    /// <summary>
    /// Inequality operator overload for a message and an issue.
    /// </summary>
    public static bool operator !=(string message, ResultIssue issue)
    {
      return !(message == issue);
    }

    /// <summary>
    /// Equality operator overload for a message and an issue.
    /// </summary>
    public static bool operator ==(string message, ResultIssue issue)
    {
      return issue == message;
    }

    /// <summary>
    /// Inequality operator overload for an issue and an issue code.
    /// </summary>
    public static bool operator !=(ResultIssue issue, long issueCode)
    {
      return !(issue == issueCode);
    }

    /// <summary>
    /// Equality operator overload for an issue and an issue code.
    /// </summary>
    public static bool operator ==(ResultIssue issue, long issueCode)
    {
      return !ReferenceEquals(issue, null) && issue.Equals(issueCode);
    }

    /// <summary>
    /// Inequality operator overload for an issue code and an issue.
    /// </summary>
    public static bool operator !=(long issueCode, ResultIssue issue)
    {
      return !(issueCode == issue);
    }

    /// <summary>
    /// Equality operator overload for an issue code and an issue.
    /// </summary>
    public static bool operator ==(long issueCode, ResultIssue issue)
    {
      return issue == issueCode;
    }

    /// <summary>
    /// Produces a result by concatonating two issues.
    /// </summary>
    public static Result operator +(ResultIssue a, ResultIssue b)
    {
      return new Result(new[] { a, b });
    }

    public static bool operator <(ResultIssue a, ResultIssue b)
    {
      if (ReferenceEquals(a, null))
        return true;

      return a.CompareTo(b) < 0;
    }

    public static bool operator >(ResultIssue a, ResultIssue b)
    {
      return b < a;
    }
  }
}
