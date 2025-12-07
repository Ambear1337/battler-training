using UnityEngine;

namespace ProjectGame
{
    public delegate void OccupieCell(bool result);

    public sealed class FieldCell: MonoBehaviour
    {
        public event OccupieCell OccupieCell;
    
        private bool _isFree = true;

        public bool IsFree
        {
            get => _isFree;
            set
            {
                _isFree = value;
                OccupieCell?.Invoke(_isFree);
            }
        }
    }
}