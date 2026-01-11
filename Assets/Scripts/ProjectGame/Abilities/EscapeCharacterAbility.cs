using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "EscapeCharacterAbility", menuName = "Battler/Character abilities/Escape character ability", order = 0)]
    internal class EscapeCharacterAbility : ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;
        public string AbilityName => "Escape";

        public void TryUseAbility()
        {
            
        }
    }
}
