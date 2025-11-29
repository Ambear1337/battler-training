using UnityEngine;

internal sealed class Character: MonoBehaviour
{
    public ValueComponent Health => health;
    [SerializeField] private ValueComponent health;
    public ValueComponent Protection => protection;
    [SerializeField] private ValueComponent protection;
    public ValueComponent Initiative => initiative;
    [SerializeField] private ValueComponent initiative;
    [SerializeField] private CharacterDescription characterDescription;

    private IPlayer _playerOwner;

    public void SetupCharacter(IPlayer playerOwner)
    {
        _playerOwner = playerOwner;

        if (!characterDescription) return;

        health.Set(characterDescription.Health);
        protection.Set(characterDescription.Protection);
        initiative.Set(characterDescription.Initiative);
    }
}
