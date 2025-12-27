using ProjectGame;
using ProjectGame.Abilities;
using ProjectGame.Buttons;
using UnityEngine;

public class UseAbilityButton: MenuButtonLeftClickBase
{
    [SerializeField] private InterfaceReference<ICharacterAbility> _abilityLogic;
    
    protected override void FireEvent()
    {
        _abilityLogic.Value.TryUseAbility();
    }
}
