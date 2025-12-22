using System;
using System.Collections.Generic;
using ProjectCore;
using ProjectEventBus;
using ProjectGame.Players;
using ProjectGame.States;
using UnityEngine;

namespace ProjectGame
{
    [AllowCreateInstance]
    public sealed class TurnBasedController: SceneSingleton<TurnBasedController>
    {
        private Character _currentActingCharacter;

        private List<Character> _characters;
        private Queue<Character> _charactersQueue;
        
        private Queue<Character> _currentCharactersQueue;

        protected override void OnSingletonInit()
        {
            base.OnSingletonInit();
            
            _characters = new List<Character>();
            _charactersQueue = new Queue<Character>();
        }

        private void Start()
        {
            SortCharactersByInitative();

            _currentCharactersQueue = new Queue<Character>(_charactersQueue);
            
            ChangeCurrentActingCharacter();
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        public void RegisterCharacter(Character character)
        {
            _characters.Add(character);
        }

        public void UnregisterCharacter(Character character)
        {
            _characters.Remove(character);
        }

        private void SortCharactersByInitative()
        {
            var tempCharacters = new List<Character>(_characters);

            tempCharacters.Sort((previousCharacter, nextCharacter) =>
            {
                var nextCharacterInitiative = nextCharacter.Initiative.CurrentValue;
                var previousCharacterInitiative = previousCharacter.Initiative.CurrentValue;
                return nextCharacterInitiative.CompareTo(previousCharacterInitiative);
            });
            
            _charactersQueue = new Queue<Character>(tempCharacters);
        }

        public void ChangeCurrentActingCharacter()
        {
            if (_currentCharactersQueue.Count <= 0)
            {
                _currentCharactersQueue = new Queue<Character>(_charactersQueue);
            }
            
            _currentActingCharacter = _currentCharactersQueue.Dequeue();

            EventBus<CharacterEvent>.Raise(new CharacterEvent
            {
                Character = _currentActingCharacter
            });
            
            // Временно
            EventBus<UIPlayerTurnTextEvent>.Raise(new UIPlayerTurnTextEvent()
            {
                Player = _currentActingCharacter.PlayerOwner
            });
            
            Debug.LogError($"Current acting character: {_currentActingCharacter.gameObject.GetFullScenePath()}, " +
                           $"player: {_currentActingCharacter.PlayerOwner}");
        }

        public void SkipCurrentPlayerTurn()
        {
            ChangeCurrentActingCharacter();
        }

        public void SkipHumanPlayerTurn()
        {
            if (_currentActingCharacter.PlayerOwner is HumanPlayer)
            {
                ChangeCurrentActingCharacter();
            }
        }

        public void SkipAIPlayerTurn()
        {
            if (_currentActingCharacter.PlayerOwner is AIPlayer)
            {
                ChangeCurrentActingCharacter();
            }
        }
    }
}
