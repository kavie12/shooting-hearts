public sealed class BonusChanceGrantedEvent : IEventData
{
}

public sealed class BonusChanceDeniedEvent : IEventData
{
}

public sealed class BonusChanceQuestionDisplayEvent : IEventData
{
    public BonusChanceQuestion Question { get; }
    public BonusChanceQuestionDisplayEvent(BonusChanceQuestion question)
    {
        this.Question = question;
    }
}

public sealed class BonusChanceQuestionAnswerGuessEvent : IEventData
{
    public int GuessedAnswer { get; }
    public BonusChanceQuestionAnswerGuessEvent(int guessedAnswer)
    {
        GuessedAnswer = guessedAnswer;
    }
}

public sealed class BonusChanceQuestionTimeout : IEventData
{
}