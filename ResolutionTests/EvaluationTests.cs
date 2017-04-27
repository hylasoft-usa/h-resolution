using Hylasoft.Resolution;
using Hylasoft.Resolution.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ResolutionTests
{
  [TestClass]
  public class EvaluationTests
  {
    protected const string CodeMessage = @"namespace Code { public class TestCodeClass { public const bool IsCode = true; } }";
    protected const string HumanMessage = @"TestConcept 'Instance' passed validation.";
    protected const string XmlMessage = @"<Object><Children><Child>First</Child><Child>Second</Child></Children></Object>";

    protected const string HumanShortMessage = @"Success!";
    protected const string XmlShortMessage = @"<Object><Item>Instance</Item></Object>";

    [TestInitialize]
    public void Initialize()
    {
      Result.MinimumCollectionLevel = ResultIssueLevels.Trace;
    }

    [TestMethod]
    public void HumanTraceOverXmlDebugTest()
    {
      const int traceCode = 0x1;
      const int debugCode = 0x2;

      var humanTrace = Result.SingleTrace(traceCode, HumanMessage);
      var debugXml = Result.SingleDebug(debugCode, XmlMessage);

      TestMostRelevant(traceCode, humanTrace, debugXml);
    }

    [TestMethod]
    public void HumanOverCodeTest()
    {
      const int humanCode = 0x3;
      const int xmlCode = 0x1;
      const int codeCode = 0x2;
      const int shortXmlCode = 0x4;

      var human = Result.SingleInfo(humanCode, HumanMessage);
      var xml = Result.SingleInfo(xmlCode, XmlMessage);
      var code = Result.SingleInfo(codeCode, CodeMessage);
      var shortXml = Result.SingleInfo(shortXmlCode, XmlShortMessage);

      TestMostRelevant(humanCode, xml, code, human, shortXml);
    }

    [TestMethod]
    public void HumanOverShortTest()
    {
      const int shortHumanCode = 0x1;
      const int humanCode = 0x2;

      var human = Result.SingleInfo(humanCode, HumanMessage);
      var shortHuman = Result.SingleInfo(shortHumanCode, HumanShortMessage);

      TestMostRelevant(humanCode, human, shortHuman);
    }

    protected void TestMostRelevant(long code, params Result[] results)
    {
      Assert.IsNotNull(results);
      
      var combined = Result.Concat(results);
      var mostRelevant = combined.GetMostRelevant();

      Assert.IsNotNull(mostRelevant);
      Assert.AreEqual(mostRelevant.IssueCode, code);
    }
  }
}
