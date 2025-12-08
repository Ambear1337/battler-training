using System.Collections.Generic;
using ProjectCore;
using ProjectCore.Extensions;
using ProjectGame.Players;
using UnityEngine;

namespace ProjectGame
{
    internal sealed class CharactersSpawner: SceneSingleton<CharactersSpawner>
    {
        [SerializeField] private CharacterDescription[] _characterDescriptions;
    
        public Character SpawnCharacter(IPlayer owner)
        {
            int randomCell;

            if (owner is HumanPlayer humanPlayer)
            {
                randomCell = FieldCells.SceneInstance.GetFreeCell(FieldCellSide.Left);
            }
            else if (owner is AIPlayer aiPlayer)
            {
                randomCell = FieldCells.SceneInstance.GetFreeCell(FieldCellSide.Right);
            }
            else
            {
                randomCell = 0;
                Debug.LogError("RandomCell is not defiened");
            }
            
            //Спавнит персонажа игрока из пула персонажей
            CharactersPool.SceneInstance.Pool.Get(out var character);
        
            character.SetupCharacter(owner, _characterDescriptions.TakeRandom());

            character.CharacterMover.TeleportCharacter(randomCell, character);

            return character;
        }

        public void ClearCharacters(List<Character> characters)
        {
            for (int characterIndex = 0; characterIndex < characters.Count; characterIndex++)
            {
                CharactersPool.SceneInstance.Pool.Release(characters[characterIndex]);
            }
        }
    }
}
