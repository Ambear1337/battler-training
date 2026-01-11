using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "SwingCharacterAbility", menuName = "Battler/Character abilities/Swing character ability", order = 6)]
    internal class SwingICharacterAbility : ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;
        public string AbilityName => "Swing";

        public void TryUseAbility()
        {
            
        }
    }
}
