using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "MoveCharacterAbility", menuName = "Battler/Character abilities/Move character ability", order = 5)]
    internal class MoveICharacterAbility : ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;
        public string AbilityName => "Move";

        public void TryUseAbility()
        {
            var currentCharacter = TurnBasedController.SceneInstance.CurrentActingCharacter;

            CharacterMover.SceneInstance.MoveCharacter(1, currentCharacter);
        }
    }
}
