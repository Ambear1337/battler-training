using System;
using System.Collections.Generic;
using ProjectEventBus;
using ProjectGame.Buttons;
using UnityEngine;

namespace ProjectGame.Players
{
    public class HumanPlayer: MonoBehaviour, IPlayer
    {
        private List<Character> _charactersSquad;
        
        private EventBinding<PlayerButtonEvent> _spawnCharacterButtonEventBinding;
        
        private void Start()
        {
            var character = CharactersSpawner.SceneInstance.SpawnCharacter(this);
            
            _charactersSquad.Add(character);
        }

        private void Awake()
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
            CharactersSpawner.SceneInstance.ClearCharacters(_charactersSquad);
            _charactersSquad.Clear();
            
            var character = CharactersSpawner.SceneInstance.SpawnCharacter(this);
            
            _charactersSquad.Add(character);
        }
        
        public void BeginTurn()
        {
            throw new System.NotImplementedException();
        }

        public void EndTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}
