using System;
using ProjectCore;
using ProjectGame.States;
using ProjectGame.States.GameStates;

namespace ProjectGame
{
    public sealed class TurnBasedController: SceneSingleton<TurnBasedController>
    {
        private IState _currentState = new InitGameState();

        public IState CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        private void Start()
        {
            _currentState.ChangeState += ChangeState;
        }

        public void ChangeState(IState newState)
        {
            _currentState = newState;
        }
    }
}
