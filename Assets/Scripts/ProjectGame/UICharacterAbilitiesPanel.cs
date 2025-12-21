using System;
using ProjectEventBus;
using ProjectGame.Players;
using UnityEngine;

namespace ProjectGame
{
    public sealed class UICharacterAbilitiesPanel: MonoBehaviour
    {
        [SerializeField] private GameObject _humanPlayerUI;
        
        private EventBinding<CharacterEvent> _characterEventBinding;

        private void OnEnable()
        {
            _characterEventBinding = new EventBinding<CharacterEvent>(HandleCharacterEvent);
            EventBus<CharacterEvent>.Register(_characterEventBinding);
        }

        private void OnDisable()
        {
            EventBus<CharacterEvent>.Deregister(_characterEventBinding);
        }

        private void HandleCharacterEvent(CharacterEvent characterEvent)
        {
            _humanPlayerUI.SetActive(characterEvent.Character.PlayerOwner is HumanPlayer);
        }
    }
}
