using ProjectCore;
using ProjectCore.Extensions;
using UnityEngine;

public sealed class FieldCells: SceneSingleton<FieldCells>
{
    [SerializeField] private FieldCell[] _fieldCells;
    private FieldCell[] _leftFieldCells = new FieldCell[4];
    private FieldCell[] _rightFieldCells = new FieldCell[4];
    private FieldCell[] _occupiedCells;

    protected override void OnSingletonInit()
    {
        for (int i = 0; i < 4; i++)
        {
            _leftFieldCells[i] = _fieldCells[i];
        }

        for (int i = 0; i < 4; i++)
        {
            _rightFieldCells[i] = _fieldCells[i+4];
        }
    }

    public FieldCell GetRandomFreeCell()
    {
        return _fieldCells.RandomExclude(_occupiedCells)[0];
    }

    public void OccupieCell(int cellIndex)
    {
        _fieldCells[cellIndex].IsFree = false;
        _occupiedCells = new FieldCell[] {_fieldCells[cellIndex]};
    }
}
