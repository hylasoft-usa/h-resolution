namespace Hylasoft.Resolution
{
  /// <summary>
  /// Represents the nature and/or severity of a result issue.
  /// </summary>
  public enum ResultIssueLevels
  {
    Unknown = 0x0,
    Trace = 0x1,
    Debug = 0x2,
    Info = 0x3,
    Warning = 0x4,
    Error = 0x5,
    Fatal = 0x7
  }
}
