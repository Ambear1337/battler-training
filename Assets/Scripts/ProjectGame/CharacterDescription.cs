using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDescription", menuName = "Battler/CharacterDescription", order = 0)]
public class CharacterDescription: ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int protection;
    [SerializeField] private int initiative;

    [SerializeField] List<CharacterAbility> abilities;

    public int Health => health;
    public int Protection => protection;
    public int Initiative => initiative;
    public IReadOnlyList<CharacterAbility> Abilities => abilities;
}
