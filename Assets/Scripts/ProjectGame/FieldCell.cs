using UnityEngine;

namespace ProjectGame
{
    public delegate void OccupyCell(bool result);

    public sealed class FieldCell: MonoBehaviour
    {
        public event OccupyCell OccupyCell;
    
        private bool _isFree = true;

        public bool IsFree
        {
            get => _isFree;
            set
            {
                _isFree = value;
                OccupyCell?.Invoke(_isFree);
            }
        }
    }
}