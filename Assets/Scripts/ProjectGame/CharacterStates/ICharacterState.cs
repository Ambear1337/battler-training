using UnityEngine;

public delegate void ChangeCharacterState(ICharacterState state);

public interface ICharacterState
{
    public event ChangeCharacterState ChangeCharacterState;
    
    public void EnterState();
    public void ExitState();
}
