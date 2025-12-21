using System;
using UnityEngine;

namespace ProjectGame
{
    public sealed class CharacterMover: MonoBehaviour
    {
        private int _currentCellIndex;

        private void Awake()
        {
            _currentCellIndex = -1;
        }

        public bool MoveCharacter(int deltaIndex, Character character)
        {
            deltaIndex = Math.Clamp(_currentCellIndex + deltaIndex, 0, FieldCells.SceneInstance.Count);
            
            if (FieldCells.SceneInstance.OccupyCell(deltaIndex, out var cellTransform))
            {
                if (_currentCellIndex != -1)
                {
                    FieldCells.SceneInstance.FreeCell(_currentCellIndex);
                }

                character.transform.position = cellTransform.position;
                
                _currentCellIndex = deltaIndex;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TeleportCharacter(int cellIndex, Character character)
        {
            cellIndex = Math.Clamp(cellIndex, 0, FieldCells.SceneInstance.Count);
            
            if (FieldCells.SceneInstance.OccupyCell(cellIndex, out var cellTransform))
            {
                if (_currentCellIndex != -1)
                {
                    FieldCells.SceneInstance.FreeCell(_currentCellIndex);
                }

                character.transform.position = cellTransform.position;
                
                _currentCellIndex = cellIndex;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
