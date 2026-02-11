using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "RotateCharacterAbility", menuName = "Battler/Character abilities/Rotate character ability")]
    public class RotateCharacterAbility: ScriptableObject, ICharacterAbility
    {
        public string AbilityName { get; }
        public void TryUseAbility()
        {
            var currentCharacter = TurnBasedController.SceneInstance.CurrentActingCharacter;
            
            CharacterRotator.SceneInstance.RotateCharacter(currentCharacter);
        }
    }
}