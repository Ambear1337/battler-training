using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "HitBreastCharacterAbility", menuName = "Battler/Character abilities/Hit breast character ability", order = 2)]
    internal class HitBreastICharacterAbility : ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;
        public string AbilityName => "HitBreast";

        public void TryUseAbility()
        {
        
        }
    }
}
