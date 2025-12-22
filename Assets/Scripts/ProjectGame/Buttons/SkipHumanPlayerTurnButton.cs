namespace ProjectGame.Buttons
{
    public class SkipHumanPlayerTurnButton: MenuButtonLeftClickBase
    {
        protected override void FireEvent()
        {
            if (!TurnBasedController.IsInstanceNull)
            {
                TurnBasedController.SceneInstance.SkipHumanPlayerTurn();
            }
        }
    }
}