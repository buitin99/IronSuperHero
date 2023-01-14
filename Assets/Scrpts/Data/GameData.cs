using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class GameData 
{
    public List<int>     levels;
    public List<int>     totalWaves;
    public List<int>     pointSpawnEnemies;
    public List<int>     pointSpawnMaps;
    public List<int>     totalEnemies;
    public List<int>     totalPositionSpawnedMaps;
    public List<int>     totalPositionSpawnedEnemies;
    public List<Vector3> positionSpawnEnemies;
    public List<Vector3> positionSpawnMaps;
}
