using System.Collections.Generic;
using ProjectCore;
using ProjectCore.Extensions;
using UnityEngine;

namespace ProjectGame
{
    public sealed class FieldCells: SceneSingleton<FieldCells>
    {
        public int Count => _fieldCells.Count;
    
        [SerializeField] private List<FieldCell> _fieldCells;

        private Dictionary<FieldCellPositionType, List<FieldCell>> _typedFieldCells;
        private List<FieldCell> _occupiedCells;

        protected override void OnSingletonInit()
        {
            _occupiedCells = new List<FieldCell>(_fieldCells.Count);
            _typedFieldCells = new Dictionary<FieldCellPositionType, List<FieldCell>>(2);
            _typedFieldCells.Add(FieldCellPositionType.Left, new List<FieldCell>(4));
            _typedFieldCells.Add(FieldCellPositionType.Right, new List<FieldCell>(4));
            
            for (int i = 0; i < 4; i++)
            {
                _typedFieldCells[FieldCellPositionType.Left].Add(_fieldCells[i]);
            }

            for (int i = 0; i < 4; i++)
            {
                _typedFieldCells[FieldCellPositionType.Right].Add(_fieldCells[i+4]);
            }
        }

        public int GetRandomFreeCell()
        {
            var freeCell = _fieldCells.RandomExclude(_occupiedCells)[0];
            var index = _fieldCells.IndexOf(freeCell);
            
            return index;
        }

        public int GetFreeCell(FieldCellPositionType positionType)
        {
            var typedFreeCells = _typedFieldCells[positionType];
            var freeCell = typedFreeCells.RandomExclude(_occupiedCells)[0];
            var index = _fieldCells.IndexOf(freeCell);

            return index;
        }

        public bool OccupieCell(int cellIndex, out Transform cellTransform)
        {
            cellTransform = null;
        
            if (_occupiedCells.Contains(_fieldCells[cellIndex]))
            {
                return false;
            }
        
            _fieldCells[cellIndex].IsFree = false;
            _occupiedCells.Add(_fieldCells[cellIndex]);

            cellTransform = _fieldCells[cellIndex].transform;

            return true;
        }

        public void FreeCell(int cellIndex)
        {
            _fieldCells[cellIndex].IsFree = true;
            _occupiedCells.Remove(_fieldCells[cellIndex]);
        }
    }
}
