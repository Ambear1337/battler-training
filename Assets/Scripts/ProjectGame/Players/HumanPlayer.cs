using System;
using System.Collections.Generic;
using ProjectEventBus;
using ProjectGame.Buttons;
using Sisus.Init;
using UnityEngine;

namespace ProjectGame.Players
{
    public class HumanPlayer: MonoBehaviour<CharactersSpawner>, IPlayer
    {
        private List<Character> _charactersSquad;
        
        private EventBinding<PlayerButtonEvent> _spawnCharacterButtonEventBinding;
        
        private CharactersSpawner _charactersSpawner;
        
        protected override void Init(CharactersSpawner argument)
        {
            _charactersSpawner = argument;
        }
        
        private void Start()
        {
            var character = _charactersSpawner.SpawnCharacter(this);
            
            _charactersSquad.Add(character);
        }

        protected override void OnAwake()
        {
            _charactersSquad = new List<Character>(3);
        }

        private void OnEnable()
        {
            _spawnCharacterButtonEventBinding = new EventBinding<PlayerButtonEvent>(HandleSpawnCharacterButton);
            EventBus<PlayerButtonEvent>.Register(_spawnCharacterButtonEventBinding);
        }

        private void OnDisable()
        {
            EventBus<PlayerButtonEvent>.Deregister(_spawnCharacterButtonEventBinding);
        }

        private void HandleSpawnCharacterButton(PlayerButtonEvent playerButton)
        {
            SpawnRandomCharacter();
        }

        [ContextMenu("Spawn character")]
        public void SpawnRandomCharacter()
        {
            _charactersSpawner.ClearCharacters(_charactersSquad);
            _charactersSquad.Clear();
            
            var character = _charactersSpawner.SpawnCharacter(this);
            
            _charactersSquad.Add(character);
        }
        
        public void BeginTurn()
        {
            
        }

        public void EndTurn()
        {
            
        }
    }
}
