using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDescription", menuName = "Battler/CharacterDescription", order = 0)]
public class CharacterDescription: ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _protection;
    [SerializeField] private int _initiative;

    [SerializeField] List<CharacterAbility> _abilities;

    public int Health => _health;
    public int Protection => _protection;
    public int Initiative => _initiative;
    public IReadOnlyList<CharacterAbility> Abilities => _abilities;
}
