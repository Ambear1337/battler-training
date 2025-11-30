using UnityEngine;

internal sealed class Character: MonoBehaviour
{
    public ValueComponent Health => _health;
    [SerializeField] private ValueComponent _health;
    public ValueComponent Protection => _protection;
    [SerializeField] private ValueComponent _protection;
    public ValueComponent Initiative => _initiative;
    [SerializeField] private ValueComponent _initiative;
    [SerializeField] private CharacterDescription _characterDescription;

    private IPlayer _playerOwner;

    public void SetupCharacter(IPlayer playerOwner)
    {
        _playerOwner = playerOwner;

        if (!_characterDescription) return;

        _health.Set(_characterDescription.Health);
        _protection.Set(_characterDescription.Protection);
        _initiative.Set(_characterDescription.Initiative);
    }
}
