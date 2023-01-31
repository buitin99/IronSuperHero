using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private Maps maps;
    public List<int> totalLevel;

    public int gold;
    public int diamond;
    public List<Maps> mapsInfor;
    public int LastestLevel {
        get {
            return totalLevel.Count > 0 ? totalLevel[totalLevel.Count-1] : 0;
        }
    }


    public GameData()
    {
        totalLevel = new List<int>();
        mapsInfor = new List<Maps>();


        gold = 0;
        diamond = 0;
        totalLevel.Add(0);
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
    public int             level;
    public int             totalWaves;
    public int             EnemyInWave;
    public int             totalSprites;
    public List<Vector3>   positionSpawnEnemy;
    public List<Vector3>   positionSpawnSprite;


    public Maps(int leves, int waves, int enemies, int sprites, List<Vector3> positionEnemies, List<Vector3> positionSprites)
    {
        positionSpawnEnemy = new List<Vector3>();
        positionSpawnSprite  = new List<Vector3>();

        level = leves;
        totalWaves = waves;
        EnemyInWave = enemies;
        totalSprites = sprites;
        positionSpawnEnemy = positionEnemies;
        positionSpawnSprite = positionSprites;
    }
}
