namespace ProjectGame.Buttons
{
    public class SkipAIPlayerTurnButton: MenuButtonLeftClickBase
    {
        protected override void FireEvent()
        {
            if (!TurnBasedController.IsInstanceNull)
            {
                TurnBasedController.SceneInstance.SkipAIPlayerTurn();
            }
        }
    }
}