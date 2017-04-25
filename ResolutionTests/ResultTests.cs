using Hylasoft.Resolution;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ResolutionTests
{
  [TestClass]
  public class ResultTests
  {
    protected const string TestMessage = "test";
    protected const int TraceCode = 0x1;
    protected const int DebugCode = 0x2;
    protected const int InfoCode = 0x4;
    protected const int WarningCode = 0x8;
    protected const int ErrorCode = 0x10;
    protected const int FatalCode = 0x20;

    private readonly Result _traceResult = Result.SingleTrace(TraceCode, TestMessage);
    private readonly Result _debugResult = Result.SingleDebug(DebugCode, TestMessage);
    private readonly Result _infoResult = Result.SingleInfo(InfoCode, TestMessage);
    private readonly Result _warningResult = Result.SingleWarning(WarningCode, TestMessage);
    private readonly Result _errorResult = Result.SingleError(ErrorCode, TestMessage);
    private readonly Result _fatalResult = Result.SingleFatal(FatalCode, TestMessage);

    protected Result TraceResult { get { return _traceResult; } }
    
    protected Result DebugResult { get { return _debugResult; } }
    
    protected Result InfoResult { get { return _infoResult; } }

    protected Result WarningResult { get { return _warningResult; } }

    protected Result ErrorResult { get { return _errorResult; } }

    protected Result FatalResult { get { return _fatalResult; } }

    [TestInitialize]
    public void TestInitialization()
    {
      Result.MinimumCollectionLevel = ResultIssueLevels.Trace;
    }

    [TestMethod]
    public void TestIsSuccessful()
    {
      Assert.IsTrue(TraceResult && DebugResult && InfoResult && WarningResult);
      Assert.IsFalse(ErrorResult || FatalResult);
      Assert.IsFalse(Result.Concat(InfoResult, ErrorResult));
      Assert.IsTrue(Result.Concat(InfoResult, WarningResult));
    }

    [TestMethod]
    public void TestMinimumLevel()
    {
      Result.MinimumCollectionLevel = ResultIssueLevels.Info;

      AssertMessageCount(0, TraceResult);
      AssertMessageCount(0, TraceResult, DebugResult);
      AssertMessageCount(1, TraceResult, DebugResult, InfoResult);
      AssertMessageCount(2, InfoResult, WarningResult);
      AssertMessageCount(1, ErrorResult);
      AssertMessageCount(1, FatalResult);
      AssertMessageCount(4, TraceResult, DebugResult, InfoResult, WarningResult, ErrorResult, FatalResult);

      Result.MinimumCollectionLevel = ResultIssueLevels.Warning;

      AssertMessageCount(3, TraceResult, DebugResult, InfoResult, WarningResult, ErrorResult, FatalResult);

      Result.MinimumCollectionLevel = ResultIssueLevels.Trace;

      AssertMessageCount(6, TraceResult, DebugResult, InfoResult, WarningResult, ErrorResult, FatalResult);

      Result.MinimumCollectionLevel = ResultIssueLevels.Warning;

      var allResults = Result.Concat(TraceResult, DebugResult, InfoResult, WarningResult, ErrorResult, FatalResult);
      Assert.IsFalse(allResults.Contains(TraceCode));
      Assert.IsFalse(allResults.Contains(DebugCode));
      Assert.IsFalse(allResults.Contains(InfoCode));
      Assert.IsTrue(allResults.Contains(WarningCode));
      Assert.IsTrue(allResults.Contains(ErrorCode));
      Assert.IsTrue(allResults.Contains(FatalCode));
    }

    protected void AssertMessageCount(int numberOfMessages, params Result[] results)
    {
      var result = Result.Concat(results);
      Assert.AreEqual(result.Messages.Count, numberOfMessages);
    }
  }
}
