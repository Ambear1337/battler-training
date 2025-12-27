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
            //Спавнит персонажа игрока из пула персонажей
            CharactersPool.SceneInstance.Pool.Get(out var character);
            
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
                CharactersPool.SceneInstance.Pool.Release(characters[characterIndex]);
            }
        }
    }
}
