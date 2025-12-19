using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGame.Players
{
    public class HumanPlayer: MonoBehaviour, IPlayer
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

        public int CalculateCharacterWithMostInitative()
        {
            if (_charactersSquad.Count == 0) 
                return 0;

            int maxInitiative = 0;
            Character characterWithMostInitative = null;
            
            for (int i = 0; i < _charactersSquad.Count; i++)
            {
                if (_charactersSquad[i].Initiative.CurrentValue > maxInitiative)
                {
                    maxInitiative = _charactersSquad[i].Initiative.CurrentValue;
                    characterWithMostInitative = _charactersSquad[i];
                }
            }

            return maxInitiative;
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
