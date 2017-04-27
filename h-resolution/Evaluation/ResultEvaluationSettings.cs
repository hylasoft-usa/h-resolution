namespace Hylasoft.Resolution.Evaluation
{
  /// <summary>
  /// A collection of settings for Result Evaluation.  Default settings are applied at time of instantiation.
  /// </summary>
  public class ResultEvaluationSettings
  {
    #region Defaults
    private readonly char[] _defaultPrimaryProgrammingPunctuation = 
    {
      ';',
      '{',
      '}',
      '*'
    };

    private readonly char[] _defaultSharedPunctuation =
    {
      '.',
      '(',
      ')',
      '&',
      ','
    };

    private readonly char[] _defaultSentenceSeparators =
    {
      '.',
      '!',
      '?'
    };

    private const char DefaultWordSeparator = ' ';
    private const uint DefaultLevelExponentWeight = 50;
    private const int DefaultIssueCodeBonus = 15;
    private const uint DefaultMessageMissingPenalty = int.MaxValue;
    private const uint DefaultMessageMinimumLength = 15;
    private const uint DefaultMessageMaximumLength = 100;
    private const uint DefaultMessageMinimumLengthPenalty = 10;
    private const uint DefaultMessageMaximumLengthPenalty = 50;
    private const uint DefaultMessageMinimumAverageWordsPerSentence = 2;
    private const uint DefaultMessageMaximumAverageWordsPerSentence = 10;
    private const uint DefaultMessageMinimumAverageWordLength = 2;
    private const uint DefaultMessageMaximumAverageWordLength = 20;
    private const uint DefaultMessageMinimumWordLengthPenalty = 10;
    private const uint DefaultMessageMaximumWordLengthPenalty = 20;
    private const uint DefaultMessageMinimumAverageWordCountPenalty = 10;
    private const uint DefaultMessageMaximumAverageWordCountPenalty = 30;
    private const uint DefaultMessagePenaltyPerProgrammingPunctuation = 10;
    private const uint DefaultMessagePenaltyPerSharedPunctuation = 2;
    #endregion

    #region Character Sets
    /// <summary>
    /// The set of punctuation that is much more likely to appear in programming languages than human readable messages.
    /// </summary>
    public char[] PrimaryProgrammingPunctuation { get; set; }

    /// <summary>
    /// The set of punctuation that can be either in programming languages or human readable messages.
    /// </summary>
    public char[] SharedPunctuation { get; set; }

    /// <summary>
    /// The set of punctuation that terminates a sentence in human readable messages.
    /// </summary>
    public char[] SentenceSeparators { get; set; }
    #endregion

    /// <summary>
    /// The value that is exponentiated by the integer value of the ResultIssueLevel.
    /// </summary>
    public uint ResultLevelExponentWeight { get; set; }

    /// <summary>
    /// The weight that is given to an issue for having a non-zero issue code.
    /// </summary>
    public int IssueCodeBonus { get; set; }

    /// <summary>
    /// The weight removed from an issue for having a null or empty message.
    /// </summary>
    public uint MessageMissingPenalty { get; set; }

    /// <summary>
    /// The minimum length a message can be without incurring a penalty.
    /// </summary>
    public uint MessageMinimumLength { get; set; }

    /// <summary>
    /// The maximum length a message can be without incurring a penalty.
    /// </summary>
    public uint MessageMaximumLength { get; set; }

    /// <summary>
    /// The penalty applied for messages that do not meet the minimum length.
    /// </summary>
    public uint MessageMinimumLengthPenalty { get; set; }

    /// <summary>
    /// The penalty applied for messaged that exceed the maximum length.
    /// </summary>
    public uint MessageMaximumLengthPenalty { get; set; }

    /// <summary>
    /// The minimum average words per sentence a message can have before incurring a penalty.
    /// </summary>
    public uint MessageMinimumAverageWordsPerSentence { get; set; }

    /// <summary>
    /// The maximum average words per sentence a message can have before incurring a penalty.
    /// </summary>
    public uint MessageMaximumAverageWordsPerSentence { get; set; }

    /// <summary>
    /// The minimum average word length a message can have before incurring a penalty.
    /// </summary>
    public uint MessageMinimumAverageWordLength { get; set; }

    /// <summary>
    /// The maximum average word length a message can have before incurring a penalty.
    /// </summary>
    public uint MessageMaximumAverageWordLength { get; set; }

    /// <summary>
    /// The penalty applied for not meeting the minimum average word length.
    /// </summary>
    public uint MessageMinimumWordLengthPenalty { get; set; }

    /// <summary>
    /// The penalty applied for exceeding the maximum average word length.
    /// </summary>
    public uint MessageMaximumWordLengthPenalty { get; set; }

    /// <summary>
    /// The penalty applied for not meeting the minimum average words per sentence.
    /// </summary>
    public uint MessageMinimumAverageWordCountPenalty { get; set; }

    /// <summary>
    /// The penalty applied for exceeding the maximum average words per sentence.
    /// </summary>
    public uint MessageMaximumAverageWordCountPenalty { get; set; }

    /// <summary>
    /// The character sequence that separates words within a message.
    /// </summary>
    public char MessageWordSeparator { get; set; }

    /// <summary>
    /// The penalty applied to messages for each programming punctuation character they contain.
    /// </summary>
    public uint MessagePenaltyPerProgrammingPunctuation { get; set; }

    /// <summary>
    /// The penalty applied to messages for each sharec punctuation character they contain.
    /// </summary>
    public uint MessagePenaltyPerSharedPunctuation { get; set; }

    public ResultEvaluationSettings()
    {
      PrimaryProgrammingPunctuation = _defaultPrimaryProgrammingPunctuation;
      SharedPunctuation = _defaultSharedPunctuation;
      SentenceSeparators = _defaultSentenceSeparators;

      ResultLevelExponentWeight = DefaultLevelExponentWeight;
      IssueCodeBonus = DefaultIssueCodeBonus;
      MessageWordSeparator = DefaultWordSeparator;
      MessageMissingPenalty = DefaultMessageMissingPenalty;

      MessageMinimumLength = DefaultMessageMinimumLength;
      MessageMaximumLength = DefaultMessageMaximumLength;

      MessageMinimumLengthPenalty = DefaultMessageMinimumLengthPenalty;
      MessageMaximumLengthPenalty = DefaultMessageMaximumLengthPenalty;

      MessageMinimumAverageWordsPerSentence = DefaultMessageMinimumAverageWordsPerSentence;
      MessageMaximumAverageWordsPerSentence = DefaultMessageMaximumAverageWordsPerSentence;

      MessageMinimumAverageWordLength = DefaultMessageMinimumAverageWordLength;
      MessageMaximumAverageWordLength = DefaultMessageMaximumAverageWordLength;

      MessageMinimumWordLengthPenalty = DefaultMessageMinimumWordLengthPenalty;
      MessageMaximumWordLengthPenalty = DefaultMessageMaximumWordLengthPenalty;

      MessageMinimumAverageWordCountPenalty = DefaultMessageMinimumAverageWordCountPenalty;
      MessageMaximumAverageWordCountPenalty = DefaultMessageMaximumAverageWordCountPenalty;

      MessagePenaltyPerProgrammingPunctuation = DefaultMessagePenaltyPerProgrammingPunctuation;
      MessagePenaltyPerSharedPunctuation = DefaultMessagePenaltyPerSharedPunctuation;
    }
  }
}
