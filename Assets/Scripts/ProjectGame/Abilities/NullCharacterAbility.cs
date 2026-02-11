using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "NullCharacterAbility", menuName = "Battler/Character abilities/Null character ability")]
    public class NullCharacterAbility: ScriptableObject, ICharacterAbility
    {
        public string AbilityName => " ";
        public void TryUseAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}