using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCustomAttribute;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    [System.Serializable]
    public class ObjectSpawn {
        [SerializeField] private GameObjectPool gameObjectPool;
        [SerializeField] private int chanceToSpawn;

        public int GetChanceToSpawn() {
            return chanceToSpawn;
        }

        public GameObjectPool GetGameObjectPool() {
            return(gameObjectPool);
        }
    }
    [SerializeField] private int levels, waves;
    [SerializeField] private Transform startPosition;
    [SerializeField] private int row, collumn;
    [SerializeField] private int minObstacle, maxObstacle;
    [SerializeField] private ObjectSpawn[] obstacles;
    [SerializeField] private int minEnemy, maxEnemy;
    [SerializeField] private int increasesEnemy;
    public float delaySpawnEnemy;
    [SerializeField] private ObjectSpawn[] enemies;
    public EffectObjectPool teleEffect;
    private List<Vector3> gridPositions, enemyPosSpawned, obstaclePosSpawned;
    private List<int> listIndexEnemy;
    private List<int> listIndexObstacle;
    private int quantityObstacle, quantityEnemy;
    [SerializeField, ReadOnly] private int CurrentLevel = 1, CurrentWave = 1;
    private ObjectPoolerManager ObjectPoolerManager;
    private GameManager gameManager;
    public event Action OnLevelCleared;
    public static event Action<int> OnWaveChange;
    public static event Action<int> OnLevelChange;

    private void Awake() {
        ObjectPoolerManager = ObjectPoolerManager.Instance;
        gameManager = GameManager.Instance;
        gridPositions = new List<Vector3>();
        enemyPosSpawned = new List<Vector3>();
        obstaclePosSpawned = new List<Vector3>();
        listIndexEnemy = new List<int>();
        listIndexObstacle = new List<int>();
        quantityObstacle = Random.Range(minObstacle, maxObstacle + 1);
        quantityEnemy = Random.Range(minEnemy, maxEnemy + 1);
        //cập nhật số màn và số wave
        gameManager.levels = levels;
        gameManager.waves = waves;

        // tạo hệ thống spawn obstacle theo tỉ lệ
        for(int i = 0; i < obstacles.Length; i ++) {
            for(int j = 0; j < obstacles[i].GetChanceToSpawn(); j++) {
                listIndexObstacle.Add(i);
            }
        }

        // tạo hệ thống spawn enemy theo tỉ lệ
        for(int i = 0; i < enemies.Length; i ++) {
            for(int j = 0; j < enemies[i].GetChanceToSpawn(); j++) {
                listIndexEnemy.Add(i);
            }
        }

    }

    private void OnEnable() {
        //chờ object pool tạo xong các object
        ObjectPoolerManager.OnCreatedObject += SpawnGameObject;
        // gameManager.OnEnemiesDestroyed += OnEnemiesDestroyed;
    }

    private void Start() {
        OnLevelChange?.Invoke(CurrentLevel);
        OnWaveChange?.Invoke(CurrentWave);
    }

    private void OnDisable() {
        ObjectPoolerManager.OnCreatedObject -= SpawnGameObject;
        // gameManager.OnEnemiesDestroyed -= OnEnemiesDestroyed;
    }

    private void CreateGridBoard() {
        // tạo grid tự động
        for(int y = 0; y <  collumn ; y ++) {
            // tạo grid cho từng hàng
            for(int x = 0; x < row ; x++) {
                // 5 là độ lớn của 1 ô grid
                Transform gridPos=  Instantiate(startPosition, startPosition.position + new Vector3(x * 5, 0, -y * 5), startPosition.rotation);
                gridPos.SetParent(transform);
                gridPositions.Add(gridPos.position);
            }
        }
    }

    private void RandomSpawnObstacle() {
        // random spawn obstacle
        for(int i = 0 ; i < quantityObstacle ; i ++) {
            int randomIndexObstacle  = listIndexObstacle[Random.Range(0, listIndexObstacle.Count)];
            int randomIndexPos = Random.Range(0,gridPositions.Count);
            Vector3 spawnPos = gridPositions[randomIndexPos];
            while(obstaclePosSpawned.IndexOf(spawnPos) != -1) {
                randomIndexPos = Random.Range(0,gridPositions.Count);
                spawnPos = gridPositions[randomIndexPos];
            }
            obstaclePosSpawned.Add(spawnPos);
            ObjectPoolerManager.SpawnObject(obstacles[randomIndexObstacle].GetGameObjectPool(), spawnPos, Quaternion.identity);
        }
    }

    private void RandomSpawnEnemy() {
        //clear danh sách enemy
        ClearEnemies();
        // random spawn enemy
        for(int i = 0 ; i < quantityEnemy ; i ++) {
            int randomIndexPos = Random.Range(0,gridPositions.Count);
            Vector3 spawnPos = gridPositions[randomIndexPos];
            while(obstaclePosSpawned.IndexOf(spawnPos) != -1 || enemyPosSpawned.IndexOf(spawnPos) != -1) {
                randomIndexPos = Random.Range(0,gridPositions.Count);
                spawnPos = gridPositions[randomIndexPos];
            }
            enemyPosSpawned.Add(spawnPos);
            //hiệu ứng tele
            GameObjectPool effect = ObjectPoolerManager.SpawnObject(teleEffect, spawnPos + Vector3.up * 0.001f, Quaternion.LookRotation(Vector3.up));
            StartCoroutine(SpawnEnemy(spawnPos, effect.gameObject));
        }
    }

    IEnumerator SpawnEnemy(Vector3 position, GameObject spawnEffect) {
        yield return new WaitForSeconds(delaySpawnEnemy);
        int randomIndexEnemy = listIndexEnemy[Random.Range(0, listIndexEnemy.Count)];
        ObjectPoolerManager.SpawnObject(enemies[randomIndexEnemy].GetGameObjectPool(), position, Quaternion.identity);
        spawnEffect.GetComponent<ParticleSystem>().Stop();
    }

    private void SpawnGameObject() {
        //tạo hệ thống grid vị trí có thể spawn
        CreateGridBoard();
        //random vật cản
        RandomSpawnObstacle();
        //random enemy
        Invoke("RandomSpawnEnemy", 1f);
    }

    private void OnEnemiesDestroyed() {
        if(CurrentWave < waves) {
            NextWave();
        } else {
            OnLevelCleared?.Invoke();
        }
    }

    public void NextWave() {
        //clear vị trí spawn cũ
        enemyPosSpawned.Clear();
        //tính lại random số enemy
        quantityEnemy = Random.Range(minEnemy, maxEnemy + 1);
        //spawn enemy
        Invoke("RandomSpawnEnemy", 1f);
        CurrentWave++;
        OnWaveChange?.Invoke(CurrentWave);
    }

    public void NextLevel() {
        //clear vị trí spawn cũ
        ObjectPoolerManager.ResetObjectPoolerManager();
        enemyPosSpawned.Clear();
        obstaclePosSpawned.Clear();
        //tăng số lượng enemy
        minEnemy += increasesEnemy;
        maxEnemy += increasesEnemy;
        //tính lại random số lượng vật cản và enemy
        quantityObstacle = Random.Range(minObstacle, maxObstacle + 1);
        quantityEnemy = Random.Range(minEnemy, maxEnemy + 1);
        //random vật cản
        RandomSpawnObstacle();
        //random enemy
        Invoke("RandomSpawnEnemy", 1f);
        CurrentWave = 1;
        CurrentLevel++;
        OnLevelChange?.Invoke(CurrentLevel);
        OnWaveChange?.Invoke(CurrentWave);
    }

    private void ClearEnemies() {
        gameManager.ClearEnemies();
    }

}
