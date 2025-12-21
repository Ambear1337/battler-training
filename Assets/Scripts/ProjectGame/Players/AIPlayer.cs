using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGame.Players
{
    public class AIPlayer: MonoBehaviour, IPlayer
    {
        private List<Character> _charactersSquad;
        
        private void Start()
        {
            var character = CharactersSpawner.SceneInstance.SpawnCharacter(this);
            
            _charactersSquad.Add(character);
        }

        private void Awake()
        {
            _charactersSquad = new List<Character>(3);
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
