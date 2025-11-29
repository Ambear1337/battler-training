using ProjectCore;
using ProjectCore.Extensions;
using UnityEngine;

public sealed class FieldCells: SceneSingleton<FieldCells>
{
    [SerializeField] private GameObject[] _fieldCellsGameObjects;
    private GameObject[] _leftFieldCellsGameObjects = new GameObject[4];
    private GameObject[] _rightFieldCellsGameObjects = new GameObject[4];
    private GameObject[] _occupiedCellsGameObjects;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            _leftFieldCellsGameObjects[i] = _fieldCellsGameObjects[i];
        }

        for (int i = 4; i < 8; i++)
        {
            _rightFieldCellsGameObjects[i] = _fieldCellsGameObjects[i];
        }
    }

    public GameObject GetRandomFreeCell()
    {
        return _fieldCellsGameObjects.RandomExclude(_occupiedCellsGameObjects)[0];
    }
}
