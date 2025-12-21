using System;
using ProjectGame.Players;
using UnityEngine;

namespace ProjectGame
{
    public sealed class Character: MonoBehaviour
    {
        public ValueComponent Health => _health;
        [SerializeField] private ValueComponent _health;
        public ValueComponent Protection => _protection;
        [SerializeField] private ValueComponent _protection;
        public ValueComponent Initiative => _initiative;
        [SerializeField] private ValueComponent _initiative;

        [SerializeField] private CharacterMover _characterMover;
        public CharacterMover CharacterMover => _characterMover;
    
        private CharacterDescription _characterDescription;

        private IPlayer _playerOwner;
        public IPlayer PlayerOwner => _playerOwner;

        public void SetupCharacter(IPlayer playerOwner, CharacterDescription characterDescription)
        {
            _playerOwner = playerOwner;
            _characterDescription = characterDescription;

            if (!characterDescription) return;

            _health.SetMax(characterDescription.Health);
            _health.Set(characterDescription.Health);
            _protection.SetMax(characterDescription.Protection);
            _protection.Set(characterDescription.Protection);
            _initiative.SetMax(characterDescription.Initiative);
            _initiative.Set(characterDescription.Initiative);
        }

        private void OnEnable()
        {
            TurnBasedController.SceneInstance.RegisterCharacter(this);
        }

        private void OnDisable()
        {
            if (!TurnBasedController.IsInstanceNull)
                TurnBasedController.SceneInstance.UnregisterCharacter(this);
        }
    }
}
