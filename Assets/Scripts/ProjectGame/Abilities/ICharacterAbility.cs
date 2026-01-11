using UnityEngine;

namespace ProjectGame.Abilities
{
    public interface ICharacterAbility
    {
        public string AbilityName { get; }
        public void TryUseAbility();
    }
}
