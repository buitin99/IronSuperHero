using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitialization : MonoBehaviour
{   
    [ReadOnly]
    public int level;
    public int totalWaves;
    public int totalEnemies;
    public int enemyInWave;
    public int totalSprites;
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

    public void ResetProperty()
    {
        Reset();
    }

    private void Reset() 
    {
        level++;
        totalWaves    = 0;
        totalEnemies  = 0;
        enemyInWave   = 0;
        totalSprites  = 0;
        positionSpawnEnemiesList = null;
        positionSpawnSpritesList = null;
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

        gameData.mapsInfor.Add(new Maps(level, totalWaves, totalEnemies, totalSprites, posSpawnEnemies, posSpawnSprites));
        gameData.Save();
    }
}

