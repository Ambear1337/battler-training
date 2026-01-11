using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "HitJawCharacterAbility", menuName = "Battler/Character abilities/Hit jaw character ability", order = 3)]
    internal class HitJawICharacterAbility : ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;
        public string AbilityName => "HitJaw";

        public void TryUseAbility()
        {
        
        }
    }
}
