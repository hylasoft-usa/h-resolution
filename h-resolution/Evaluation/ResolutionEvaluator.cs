using System;
using System.Collections.Generic;
using System.Linq;

namespace Hylasoft.Resolution.Evaluation
{
  /// <summary>
  /// Evaluates results and result issues, making comparisons and selections.
  /// </summary>
  public class ResolutionEvaluator : IResolutionEvaluator
  {
    #region Settings
    protected static ResultEvaluationSettings Settings { get; private set; }
    #endregion

    #region Singleton Implementation
    private static ResolutionEvaluator _resolutionEvaluator;

    /// <summary>
    /// Sets evalutation configuration.
    /// </summary>
    /// <param name="settings">The collection of settings to use for evaluation.</param>
    public static void SetConfiguration(ResultEvaluationSettings settings)
    {
      if (settings != null)
        Settings = settings;
    }

    /// <summary>
    /// Singleton exposure of a ResolutionEvaluator.
    /// </summary>
    public static ResolutionEvaluator Evaluator
    {
      get { return _resolutionEvaluator ?? (_resolutionEvaluator = BuildEvaluator()); }
    }

    protected ResolutionEvaluator()
    {
      Settings = new ResultEvaluationSettings();
    }

    private static ResolutionEvaluator BuildEvaluator()
    {
      return new ResolutionEvaluator();
    }
    #endregion

    #region IResolutionEvaluator Implementation
    /// <summary>
    /// Attempts to determine which issue is the most relevent in a given result.
    /// </summary>
    /// <param name="issues">A collection of issues to evaluate.</param>
    /// <returns></returns>
    public ResultIssue MostRevelvant(IEnumerable<ResultIssue> issues)
    {
      if (issues == null)
        return null;

      return issues
        .Select(issue => new { Issue = issue, Weight = RetrieveWeight(issue) })
        .OrderByDescending(setItem => setItem.Weight)
        .Select(setItem => setItem.Issue)
        .FirstOrDefault();
    }
    #endregion

    #region Weight Methods
    protected virtual long RetrieveWeight(ResultIssue issue)
    {
      return RetrieveLevelWeight(issue)
             + RetrieveCodeWeight(issue)
             + RetrieveMessageWeight(issue);
    }

    protected virtual long RetrieveLevelWeight(ResultIssue issue)
    {
      var level = (int)issue.Level;
      return (long)Math.Pow(Settings.ResultLevelExponentWeight, level);
    }

    protected virtual long RetrieveCodeWeight(ResultIssue issue)
    {
      var code = issue.IssueCode;
      return code > 0x0
        ? Settings.IssueCodeBonus
        : 0x0;
    }

    protected virtual long RetrieveMessageWeight(ResultIssue issue)
    {
      var message = issue.Message;
      if (string.IsNullOrEmpty(message))
        return 0x0 - Settings.MessageMissingPenalty;

      var combinedWeight = RetrieveMessageLengthWeight(message)
                           + RetrieveMessageWordCountRatioWeights(message)
                           + RetrieveMessagePunctuationRatioWeight(message);

      return combinedWeight;
    }

    protected virtual long RetrieveMessageLengthWeight(string message)
    {
      var length = message.Length;
      if (length < Settings.MessageMinimumLength)
        return 0x0 - Settings.MessageMinimumLengthPenalty;

      return length > Settings.MessageMaximumLength
        ? 0x0 - Settings.MessageMaximumLengthPenalty 
        : 0x0;
    }

    protected virtual long RetrieveMessageWordCountRatioWeights(string message)
    {
      var averageWordsPerSentence = message
        .Split(Settings.SentenceSeparators)
        .Average(sentence => sentence.Split(Settings.MessageWordSeparator).Length);

      var averageWordLength = message
        .Split(Settings.MessageWordSeparator)
        .Average(word => word.Length);


      var weight = (long)0x0;
      if (averageWordsPerSentence < Settings.MessageMinimumAverageWordsPerSentence)
        weight -= Settings.MessageMinimumAverageWordCountPenalty;

      if (averageWordsPerSentence > Settings.MessageMaximumAverageWordsPerSentence)
        weight -= Settings.MessageMaximumAverageWordCountPenalty;

      if (averageWordLength < Settings.MessageMinimumAverageWordLength)
        weight -= Settings.MessageMinimumWordLengthPenalty;

      if (averageWordLength > Settings.MessageMaximumAverageWordLength)
        weight -= Settings.MessageMaximumWordLengthPenalty;

      return weight;
    }

    protected virtual long RetrieveMessagePunctuationRatioWeight(string message)
    {
      var messageCharacters = message.ToCharArray();
      var programmingPunctuations = messageCharacters
        .Where(chr => Settings.PrimaryProgrammingPunctuation.Contains(chr))
        .ToArray();

      var languagePunctuations = messageCharacters
        .Where(chr => Settings.SharedPunctuation.Contains(chr))
        .ToArray();

      var weight = (long)0x0;

      weight -= (programmingPunctuations.Length * Settings.MessagePenaltyPerProgrammingPunctuation);
      weight -= (languagePunctuations.Length * Settings.MessagePenaltyPerSharedPunctuation);

      var puncDiff = programmingPunctuations.Length - languagePunctuations.Length;
      if (puncDiff > 0x0)
        weight *= puncDiff;
      
      return weight;
    }
    #endregion
  }
}
