public sealed class BonusChanceQuestionFetchSuccessEvent : EventData
{
    public BonusChanceQuestion Question { get; }

    public BonusChanceQuestionFetchSuccessEvent(BonusChanceQuestion question)
    {
        this.Question = question;
    }
}