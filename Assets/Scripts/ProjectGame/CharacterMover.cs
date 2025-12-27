using System;
using ProjectCore;
using UnityEngine;

namespace ProjectGame
{
    [AllowCreateInstance]
    public sealed class CharacterMover: SceneSingleton<CharacterMover>
    {
        public bool MoveCharacter(int cellsCount, Character character)
        {
            var direction =
                    character.transform.localRotation == Quaternion.Euler(0, -90, 0) ?
                      -1 :
                       1 ;
            
            var targetCell = character.CurrentCellIndex + (cellsCount * direction);
            
            return TeleportCharacterInternal(character, targetCell);
        }

        public bool TeleportCharacter(int cellIndex, Character character)
        {
            return TeleportCharacterInternal(character, cellIndex);
        }
        
        private bool TeleportCharacterInternal(Character character, int targetCell)
        {
            targetCell = Math.Clamp(targetCell,
                0, FieldCells.SceneInstance.Count);
            
            if (FieldCells.SceneInstance.OccupyCell(targetCell, out var cellTransform))
            {
                FieldCells.SceneInstance.FreeCell(character.CurrentCellIndex);
                
                character.SetCellIndex(targetCell);

                character.transform.position = cellTransform.position;
  
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
