using UnityEngine;

namespace ProjectGame.States.GameStates
{
    internal class ResultsGameState: MonoBehaviour, IState
    {
        public event ChangeState ChangeState;
        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void ExitState(IState nextState)
        {
            ChangeState?.Invoke(nextState);
        }
    }
}
