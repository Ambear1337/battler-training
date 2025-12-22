using ProjectGame;
using ProjectGame.Buttons;
using UnityEngine;

public class SkipCurrentPlayerTurnButton: MenuButtonLeftClickBase
{
    protected override void FireEvent()
    {
        if (!TurnBasedController.IsInstanceNull)
        {
            TurnBasedController.SceneInstance.SkipCurrentPlayerTurn();
        }
    }
}
