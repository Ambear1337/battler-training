using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "HeadStrikeCharacterAbility", menuName = "Battler/Character abilities/Head strike character ability", order = 1)]

    internal class HeadStrikeICharacterAbility : ScriptableObject, ICharacterAbility, IDamaging
    {
        [SerializeField] private int _cost = 1;
        
        public string AbilityName => "HeadStrike";

        public void TryUseAbility()
        {
            //И врагу и себе
        }

        public int Damage => 50;
    }
}
