using System.Collections.Generic;
using ProjectGame.Abilities;
using UnityEngine;

namespace ProjectGame
{
    [CreateAssetMenu(fileName = "CharacterDescription", menuName = "Battler/CharacterDescription", order = 0)]
    public class CharacterDescription: ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _protection;
        [SerializeField] private int _initiative;
        [SerializeField] private List<InterfaceReference<ICharacterAbility>> _abilities;

        public int Health => _health;
        public int Protection => _protection;
        public int Initiative => _initiative;
        public IReadOnlyList<InterfaceReference<ICharacterAbility>> Abilities => _abilities;
    }
}
