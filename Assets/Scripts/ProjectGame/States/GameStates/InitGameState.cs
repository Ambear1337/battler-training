using System;
using Codice.Client.BaseCommands.Import;
using UnityEngine;

namespace ProjectGame.States.GameStates
{
    internal class InitGameState: MonoBehaviour, IState
    {
        public event ChangeState ChangeState;

        public void EnterState()
        {
            //Calculate inititatives
            //Change state on result of calculations
            
            
            
            ChangeState?.Invoke(this);
        }

        public void ExitState(IState nextState)
        {
            
        }
    }
}
