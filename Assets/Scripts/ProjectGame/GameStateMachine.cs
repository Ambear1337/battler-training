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

        switch (_currentGameState)
        {
            case GameState.InitGame:
                break;
            case GameState.HumanPlayerTurn:
                break;
            case GameState.AIPlayerTurn:
                break;
            case GameState.EndGame:
                break;
        }
    }
}
