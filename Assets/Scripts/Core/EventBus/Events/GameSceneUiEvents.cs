public sealed class OnGameOverPanelButtonClicked : IEventData
{
    public GameOverPanelButton ButtonId { get; }
    public OnGameOverPanelButtonClicked(GameOverPanelButton buttonId)
    {
        this.ButtonId = buttonId;
    }
}