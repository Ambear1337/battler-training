using UnityEngine;

internal sealed class GameStateMachine
{
    private GameState _currentGameState;
    
    public void InitTurns()
    {
        SetCurrentGameState(GameState.InitGame);
    }

    private void SetCurrentGameState(GameState nextState)
    {
        _currentGameState = nextState;

        
    }
}
