public sealed class BonusChanceQuestionAnswerGuessEvent : EventData
{
    public int GuessedAnswer { get; }

    public BonusChanceQuestionAnswerGuessEvent(int guessedAnswer)
    {
        GuessedAnswer = guessedAnswer;
    }
}