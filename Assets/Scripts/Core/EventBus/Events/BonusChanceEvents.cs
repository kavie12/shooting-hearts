public sealed class OnBonusChanceRequested : IEventData { }

public sealed class OnBonusChanceRequestCompleted : IEventData
{
    public bool Granted { get; }
    public OnBonusChanceRequestCompleted(bool granted)
    {
        this.Granted = granted;
    }
}

public sealed class OnBonusChanceQuestionFetched : IEventData
{
    public BonusChanceQuestion Question { get; }
    public OnBonusChanceQuestionFetched(BonusChanceQuestion question)
    {
        this.Question = question;
    }
}

public sealed class OnBonusChanceQuestionAnswerGuessed : IEventData
{
    public int GuessedAnswer { get; }
    public OnBonusChanceQuestionAnswerGuessed(int guessedAnswer)
    {
        GuessedAnswer = guessedAnswer;
    }
}

public sealed class OnBonusChanceQuestionTimeout : IEventData { }