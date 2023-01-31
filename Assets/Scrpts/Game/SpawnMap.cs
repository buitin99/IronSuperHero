using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class SpawnMap : MonoBehaviour
{
    [SerializeField]
    private ScriptableEnemy enemy;
    private GameData gameData;
    private int level;
    private int turn = 1;
    private int temp;
    private int enemiesInWave;
    private int randomEnemy;
    private List<GameObject> goSpawnEnemyList = new List<GameObject>();
    private List<GameObject> goSpawnMapList = new List<GameObject>();

    public GameObject spawnEnemyPoint1;
    public GameObject spawnEnemyPoint2;
    public GameObject spawnEnemyPoint3;
    public GameObject spawnSpritePoint1;
    public GameObject spawnSpritePoint2;
    public GameObject spawnSpritePoint3;
    public UnityEvent<int, int> OnTotalEnemy = new UnityEvent<int, int>();


    private GameManager gameManager;



    private void OnEnable() 
    {
        gameData = GameData.Load();
        gameManager = GameManager.Instance;
        
    }
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    // }


    // private void StartGame()
    // {
    //     gameData = GameData.Load();
    //     if (gameManager.isInit)
    //     {
    //         level++;
    //     }else
    //     {
    //         level = 1;
    //     }

    //     turn = 1;
    //     goSpawnEnemyList.Clear();
    //     goSpawnMapList.Clear();
    //     InitSpawnEnemy();
    //     SetPositionWhenStart();
    //     Wave();
    // }

    // private void InitSpawnEnemy()
    // {
    //     for (int k = 0; k < gameData.mapsInfor[2].positionSpawnEnemies.Count; k++)
    //     {
    //         GameObject go = new GameObject("go" + k);
    //         go.transform.position = gameData.mapsInfor[2].positionSpawnEnemies[k];
    //         goSpawnEnemyList.Add(go);
    //     }
    // }

    // private void SetPositionWhenStart()
    // {
    //     temp = gameData.mapsInfor[level].pointSpawnEnemies;

    //     switch(temp)
    //     {
    //         case 1:
    //                 spawnEnemyPoint1.transform.position = goSpawnEnemyList[temp-1].transform.position;
    //             break;
    //         case 2:
    //                 spawnEnemyPoint1.transform.position = goSpawnEnemyList[temp-temp].transform.position;
    //                 spawnEnemyPoint2.transform.position = goSpawnEnemyList[temp-1].transform.position;
    //             break;
    //         case 3:
    //                 spawnEnemyPoint1.transform.position = goSpawnEnemyList[temp-temp].transform.position;
    //                 spawnEnemyPoint2.transform.position = goSpawnEnemyList[temp-1].transform.position;
    //                 spawnEnemyPoint3.transform.position = goSpawnEnemyList[temp].transform.position;
    //             break;
    //         default:
    //             break;
    //     }
    // }

    // private void Wave()
    // {
    //     ChangePositionSpawn();
    //     enemiesInWave = gameData.mapsInfor[level].totalEnemies;

    //     switch(enemiesInWave)
    //     {
    //         case 1:
    //                 randomEnemy = Random.Range(0,1);
    //                 Instantiate(enemy.enemies[randomEnemy].enemy, spawnEnemyPoint1.transform.position, Quaternion.identity);
    //             break;
    //         case 2:
    //                 randomEnemy = Random.Range(0,1);
    //                 Instantiate(enemy.enemies[randomEnemy].enemy, spawnEnemyPoint1.transform.position, Quaternion.identity);
    //                 randomEnemy = Random.Range(0,1);
    //                 Instantiate(enemy.enemies[randomEnemy].enemy, spawnEnemyPoint2.transform.position, Quaternion.identity);
    //             break;
    //         case 3:
    //                 randomEnemy = Random.Range(0,1);
    //                 Instantiate(enemy.enemies[randomEnemy].enemy, spawnEnemyPoint1.transform.position, Quaternion.identity);
    //                 randomEnemy = Random.Range(0,1);
    //                 Instantiate(enemy.enemies[randomEnemy].enemy, spawnEnemyPoint2.transform.position, Quaternion.identity);
    //                 randomEnemy = Random.Range(0,1);
    //                 Instantiate(enemy.enemies[randomEnemy].enemy, spawnEnemyPoint3.transform.position, Quaternion.identity);
    //             break;
    //         default:
    //             break;
    //     }
    //     turn++;
    //     OnTotalEnemy?.Invoke(turn, enemiesInWave);
    // }

    // private void ChangePositionSpawn()
    // {
    //     if (temp == 2)
    //     {
    //         switch(turn)
    //         {
    //             case 1:
    //                 break;
    //             case 2:
    //                     spawnEnemyPoint1.transform.position = goSpawnEnemyList[turn].transform.position;
    //                     spawnEnemyPoint2.transform.position = goSpawnEnemyList[turn+1].transform.position;
    //                 break;
    //             case 3:
    //                     spawnEnemyPoint1.transform.position = goSpawnEnemyList[turn].transform.position;
    //                     spawnEnemyPoint2.transform.position = goSpawnEnemyList[turn+1].transform.position;   
    //                 break;
    //             default:
    //                 break;
    //         }  
    //     }
    //     else if (temp == 3)
    //     {
    //         switch(turn)
    //         {
    //             case 1:
    //                 break;
    //             case 2:
    //                     spawnEnemyPoint1.transform.position = goSpawnEnemyList[turn].transform.position;
    //                     spawnEnemyPoint2.transform.position = goSpawnEnemyList[turn+1].transform.position;
    //                     spawnEnemyPoint3.transform.position = goSpawnEnemyList[turn+2].transform.position;
    //                 break;
    //             case 3:
    //                     spawnEnemyPoint1.transform.position = goSpawnEnemyList[turn].transform.position;
    //                     spawnEnemyPoint2.transform.position = goSpawnEnemyList[turn+1].transform.position;
    //                     spawnEnemyPoint3.transform.position = goSpawnEnemyList[turn+2].transform.position;
    //                 break;
    //             default:
    //                 break;
    //         }
    //     } 
    // }

    // private void EndGame()
    // {
    //     for (int n = 0; n < goSpawnEnemyList.Count; n++)
    //     {
    //         Destroy(goSpawnEnemyList[n].gameObject);
    //     }

    //     NavMeshAgent[] goEnemyDestroy = FindObjectsOfType<NavMeshAgent>();
    //     for (int m = 0; m < goEnemyDestroy.Length; m++)
    //     {
    //         Destroy(goEnemyDestroy[m].gameObject);
    //     }
    // }

    // private void OnDisable() 
    // {
        
    // }
}
