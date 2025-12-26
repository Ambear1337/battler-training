using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "HeadStrikeCharacterAbility", menuName = "Battler/Character abilities/Head strike character ability", order = 1)]

    internal class HeadStrikeICharacterAbility : ScriptableObject, ICharacterAbility
    {
        public void TryUseAbility()
        {
        
        }
    }
}
