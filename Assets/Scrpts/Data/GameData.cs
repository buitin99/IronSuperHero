using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public List<Maps> mapsInfor;

    private Maps maps;

    public GameData()
    {
        mapsInfor = new List<Maps>();
        mapsInfor.Add(this.maps);
    }

    public void Save()
    {
        GameDataManager<GameData>.SaveData(this);
    }

    public static GameData Load()
    {
        return GameDataManager<GameData>.LoadData();
    }
}

[System.Serializable]
public class Maps
{
    public int             currentLevels;
    public int             totalWaves;
    public int             totalEnemies;
    public int             totalSprites;
    public int             pointSpawnEnemies;
    public int             pointSpawnSprite;
    public List<Vector3>   positionSpawnEnemies;
    public List<Vector3>   positionSpawnSprite;


    public Maps(int leves, int waves, int enemies, int sprites, int pointEnemies, int pointSprites, List<Vector3> positionEnemies, List<Vector3> positionSprites)
    {

        positionSpawnEnemies = new List<Vector3>();
        positionSpawnSprite  = new List<Vector3>();

        currentLevels = leves;
        totalWaves = waves;
        totalEnemies = enemies;
        totalSprites = sprites;
        pointSpawnEnemies = pointEnemies;
        pointSpawnSprite = pointSprites;
        positionSpawnEnemies = positionEnemies;
        positionSpawnSprite = positionSprites;

        // positionSpawnEnemies.Add(Vector3.zero);
        // positionSpawnSprite.Add(Vector3.zero);
    }
}
