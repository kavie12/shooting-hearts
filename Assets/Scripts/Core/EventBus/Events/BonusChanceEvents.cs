// Used to request a bonus chance in the game.
public sealed class OnBonusChanceRequested : IEventData { }

// Used when a bonus chance request is completed, indicating whether it was granted.
public sealed class OnBonusChanceRequestCompleted : IEventData
{
    public bool Granted { get; }
    public OnBonusChanceRequestCompleted(bool granted)
    {
        this.Granted = granted;
    }
}

// Used when a bonus chance question is fetched from the server.
public sealed class OnBonusChanceQuestionFetched : IEventData
{
    public BonusChanceQuestion Question { get; }
    public OnBonusChanceQuestionFetched(BonusChanceQuestion question)
    {
        this.Question = question;
    }
}

// Used when the player guesses an answer to the bonus chance question.
public sealed class OnBonusChanceQuestionAnswerGuessed : IEventData
{
    public int GuessedAnswer { get; }
    public OnBonusChanceQuestionAnswerGuessed(int guessedAnswer)
    {
        GuessedAnswer = guessedAnswer;
    }
}

// Used when the answer to the bonus chance question has been checked for correctness.
public sealed class OnBonusChanceQuestionAnswerChecked : IEventData
{
    public bool Correct { get; }
    public OnBonusChanceQuestionAnswerChecked(bool correct)
    {
        Correct = correct;
    }
}

// Used when the bonus chance question times out without an answer.
public sealed class OnBonusChanceQuestionTimeout : IEventData { }