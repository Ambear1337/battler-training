using System;
using System.Collections.Generic;
using Sisus.Init;
using UnityEngine;

namespace ProjectGame.Players
{
    public class AIPlayer: MonoBehaviour<CharactersSpawner>, IPlayer
    {
        private List<Character> _charactersSquad;
        
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
