using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "LungeCharacterAbility", menuName = "Battler/Character abilities/Lunge character ability", order = 4)]
    internal class LungeICharacterAbility : ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;

        public string AbilityName => "Lunge";

        public void TryUseAbility()
        {
        
        }
    }
}
