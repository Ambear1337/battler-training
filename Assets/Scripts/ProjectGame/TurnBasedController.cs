using System;
using System.Collections.Generic;
using ProjectCore;
using ProjectGame.Players;
using ProjectGame.States;
using UnityEngine;

namespace ProjectGame
{
    public delegate void CurrentCharacterChangedHandler(Character character);
    
    [AllowCreateInstance]
    public sealed class TurnBasedController: SceneSingleton<TurnBasedController>
    {
        private Character _currentActingCharacter;

        private List<Character> _characters;
        private Queue<Character> _charactersQueue;
        
        private Queue<Character> _currentCharactersQueue;
        
        public event  CurrentCharacterChangedHandler CurrentCharacterChanged;

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
            
            //Change character() {
            _currentActingCharacter = _currentCharactersQueue.Dequeue();

            CurrentCharacterChanged?.Invoke(_currentActingCharacter);
            
            Debug.LogError("Current acting character: " + _currentActingCharacter.gameObject.GetFullScenePath());
            //}
        }

        public void RegisterCharacter(Character character)
        {
            _characters.Add(character);
        }

        public void UnregisterCharacter(Character character)
        {
            _characters.Remove(character);
        }

        public void SortCharactersByInitative()
        {
            List<Character> tempCharacters = new List<Character>(_characters);

            tempCharacters.Sort((previousCharacter, nextCharacter) =>
            {
                var nextCharacterInitiative = nextCharacter.Initiative.CurrentValue;
                var previousCharacterInitiative = previousCharacter.Initiative.CurrentValue;
                return nextCharacterInitiative.CompareTo(previousCharacterInitiative);
            });
            
            _charactersQueue = new Queue<Character>(tempCharacters);
        }
    }
}
