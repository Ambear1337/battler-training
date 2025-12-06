using UnityEngine;

internal sealed class CharactersSpawner: MonoBehaviour
{
    public void SpawnPlayerCharacter()
    {
        //Ищет рандмоную свободную клетку на стороне игрока в FieldCells
        FieldCells.SceneInstance.GetRandomFreeCell();
        //Спавнит персонажа игрока из пула персонажей
        
    }

    public void SpawnAIPlayerCharacter()
    {
        
    }
}
