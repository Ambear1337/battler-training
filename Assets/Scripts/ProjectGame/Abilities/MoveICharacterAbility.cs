using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "MoveCharacterAbility", menuName = "Battler/Character abilities/Move character ability", order = 5)]
    internal class MoveICharacterAbility : ScriptableObject, ICharacterAbility
    {
        public void TryUseAbility()
        {
        
        }
    }
}
