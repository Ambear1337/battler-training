namespace ProjectGame.States.CharacterStates
{
    public sealed class IdleCharacterState: IState
    {
        public event ChangeState ChangeState;
        public void EnterState()
        {
        
        }

        public void ExitState(IState nextState)
        {
            ChangeState?.Invoke(nextState);
        }
    }
}
