// Used when a game over panel button is clicked, indicating which button in the GameOverPanelButton enum.
public sealed class OnGameOverPanelButtonClicked : IEventData
{
    public GameOverPanelButton ButtonId { get; }
    public OnGameOverPanelButtonClicked(GameOverPanelButton buttonId)
    {
        this.ButtonId = buttonId;
    }
}