using System;
using Codice.Client.BaseCommands.Import;
using UnityEngine;

namespace ProjectGame.States.GameStates
{
    internal class InitGameState: MonoBehaviour, IState
    {
        public event ChangeState ChangeState;

        private void Awake()
        {
            EnterState();
        }

        public void EnterState()
        {
            
        }

        public void ExitState(IState nextState)
        {
            ChangeState.Invoke(nextState);
        }
    }
}
