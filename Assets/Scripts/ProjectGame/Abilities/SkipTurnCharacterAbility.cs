using UnityEngine;

namespace ProjectGame.Abilities
{
    [CreateAssetMenu(fileName = "SkipTurnCharacterAbility", menuName = "Battler/Character abilities/Skip turn character ability", order = 8)]
    public class SkipTurnCharacterAbility: ScriptableObject, ICharacterAbility
    {
        [SerializeField] private int _cost = 1;
        public string AbilityName => "SkipTurn";

        public void TryUseAbility()
        {
            TurnBasedController.SceneInstance.SkipCurrentPlayerTurn();
        }
    }
}