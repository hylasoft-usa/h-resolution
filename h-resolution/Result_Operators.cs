using System.Collections.Generic;
using System.Linq;

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
      var resultB = issues as Result;
      return resultB != null
        ? result + resultB
        : Concat(result, new Result(issues));
    }

    /// <summary>
    /// Concatonates a collection of issues and a result via the addition operator.
    /// </summary>
    public static Result operator +(IEnumerable<ResultIssue> issues, Result result)
    {
      return result + issues;
    }

    /// <summary>
    /// Checks if two results are equal.
    /// </summary>
    public static bool operator ==(Result a, Result b)
    {
      if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        return true;

      if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        return false;

      var distinctA = a.Distinct().ToArray();
      var distinctB = b.Distinct().ToArray();

      return (distinctA.Length == distinctB.Length) && distinctA.All(distinctB.Contains);
    }

    /// <summary>
    /// Checks if two results are not equal.
    /// </summary>
    public static bool operator !=(Result a, Result b)
    {
      return !(a == b);
    }

    protected bool Equals(Result other)
    {
      return _isSuccessful.Equals(other._isSuccessful) && Equals(_messages, other._messages) && Equals(Issues, other.Issues);
    }

    #region Overrides
    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Result)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int hashCode = _isSuccessful.GetHashCode();
        hashCode = (hashCode * 397) ^ (_messages != null ? _messages.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Issues != null ? Issues.GetHashCode() : 0);
        return hashCode;
      }
    }
    #endregion
  }
}
