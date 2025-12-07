namespace ProjectGame.States
{
    public delegate void ChangeState(IState state);

    public interface IState
    {
        public event ChangeState ChangeState;
    
        public void EnterState();
        public void ExitState();
    }
}