using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitialization : MonoBehaviour
{   
    public int currentLevels;
    public int totalWaves;
    public int totalEnemies;
    public int totalSprites;
    public int pointSpawnEnemies;
    public int pointSpawnSprites;
    public Vector3[] positionSpawnEnemiesList;
    public Vector3[] positionSpawnSpritesList;

    private GameData gameData;
    private Maps maps;

    private void Awake() 
    {
        gameData = GameData.Load();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SaveDataToJson()
    {
        List<Vector3> posSpawnEnemies = new List<Vector3>();
        List<Vector3> posSpawnSprites = new List<Vector3>();

        foreach (Vector3 item in positionSpawnEnemiesList)
        {
            posSpawnEnemies.Add(item);
        }

        foreach (Vector3 item1 in positionSpawnSpritesList)
        {
            posSpawnSprites.Add(item1);
        }

        Maps maps = new Maps(currentLevels, totalWaves, totalEnemies, totalSprites, pointSpawnEnemies,  pointSpawnSprites, posSpawnEnemies, posSpawnSprites);

        gameData.mapsInfor.Add(maps);
        gameData.Save();
    }
}

