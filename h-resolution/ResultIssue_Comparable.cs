using System;

namespace Hylasoft.Resolution
{
  public partial class ResultIssue : IComparable<ResultIssue>, IEquatable<ResultIssue>, IEquatable<string>, IEquatable<long>
  {
    /// <summary>
    /// Compares a ResultIssue to another.
    /// </summary>
    public int CompareTo(ResultIssue otherIssue)
    {
      const int problematicComparison = 1;
      if (ReferenceEquals(otherIssue, null))
        return problematicComparison;

      // Sort by date first.
      int dateComparison;
      if ((dateComparison = Date.CompareTo(otherIssue.Date)) != 0)
        return dateComparison;

      // Sort by issue code second.
      int issueCodeComparison;
      if ((issueCodeComparison = IssueCode.CompareTo(otherIssue.IssueCode)) != 0)
        return issueCodeComparison;

      // Sort by message value last.
      return string.IsNullOrEmpty(Message)
        ? problematicComparison
        : MessageCompare(Message, otherIssue.Message);
    }

    /// <summary>
    /// Returns whether the ResultIssue is equal.
    /// </summary>
    public bool Equals(ResultIssue issue)
    {
      if (ReferenceEquals(null, issue)) return false;
      if (ReferenceEquals(this, issue)) return true;
      return Level == issue.Level && string.Equals(Message, issue.Message) && Date.Equals(issue.Date) && IssueCode == issue.IssueCode;
    }

    /// <summary>
    /// Returns whether the current issue is equal to the provided string.
    /// </summary>
    public bool Equals(string message)
    {
      return MessageEquals(Message, message);
    }

    /// <summary>
    /// Returns whether the current issue is equal to the provided issue code.
    /// </summary>
    public bool Equals(long issueCode)
    {
      return IssueCode.Equals(issueCode);
    }

    /// <summary>
    /// Returns whether the current issue is equal to the provided object.
    /// </summary>
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ResultIssue)obj);
    }

    /// <summary>
    /// Returns the current issue's hash code.
    /// </summary>
    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = (int)Level;
        hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ Date.GetHashCode();
        hashCode = (hashCode * 397) ^ IssueCode.GetHashCode();
        return hashCode;
      }
    }

    /// <summary>
    /// Returns whether two messages are relatively equal.
    /// </summary>
    protected bool MessageEquals(string a, string b)
    {
      return MessageCompare(a, b) == 0;
    }

    /// <summary>
    /// Compares two messages.
    /// </summary>
    /// <returns>An integer value representing the comparison.</returns>
    protected int MessageCompare(string a, string b)
    {
      return string.Compare(MessageFormat(a), MessageFormat(b), StringComparison.Ordinal);
    }
  }
}
