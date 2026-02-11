using System.Collections.Generic;
using ProjectCore;
using ProjectCore.Extensions;
using ProjectGame.Players;
using Sisus.Init;
using UnityEngine;

namespace ProjectGame
{
    [Service(FindFromScene = true)]
    public sealed class CharactersSpawner: MonoBehaviour<CharactersPool>
    {
        [SerializeField] private CharacterDescription[] _characterDescriptions;
        
        private CharactersPool _charactersPool;
    
        protected override void Init(CharactersPool argument)
        {
            _charactersPool = argument;
        }
        
        public Character SpawnCharacter(IPlayer owner)
        {
            //Спавнит персонажа игрока из пула персонажей
            _charactersPool.Pool.Get(out var character);
            
            int randomCell;
            
            if (owner is HumanPlayer humanPlayer)
            {
                randomCell = FieldCells.SceneInstance.GetFreeCell(FieldCellSide.Left);
                character.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            else if (owner is AIPlayer aiPlayer)
            {
                randomCell = FieldCells.SceneInstance.GetFreeCell(FieldCellSide.Right);
                character.transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                randomCell = 0;
                Debug.LogError("RandomCell is not defiened");
            }
        
            character.SetupCharacter(owner, _characterDescriptions.TakeRandom());

            CharacterMover.SceneInstance.TeleportCharacter(randomCell, character);

            return character;
        }

        public void ClearCharacters(List<Character> characters)
        {
            for (int characterIndex = 0; characterIndex < characters.Count; characterIndex++)
            {
                _charactersPool.Pool.Release(characters[characterIndex]);
            }
        }
    }
}
