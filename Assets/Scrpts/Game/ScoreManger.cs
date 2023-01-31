using UnityEngine;
using UnityEngine.Events;


public class ScoreManger : Singleton<ScoreManger>
{
    private SpawnMap spawnMap;
    private GameManager gameManager;
    private GameData gameData;
    private int totalEnemy;
    private int score = 1;
    private int wave = 1;
    public GameObject obstacles1, obstacles2;
    public UnityEvent OnWaveDone = new UnityEvent();

    protected override void Awake()
    {
        base.Awake();
        spawnMap = FindObjectOfType<SpawnMap>();
        gameManager = GameManager.Instance;
        gameData = GameData.Load();
    }

    private void OnEnable() 
    {
        spawnMap.OnTotalEnemy.AddListener(TotalEnemy);
        gameManager.OnStartGame.AddListener(StartGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TotalEnemy(int waves, int total)
    {
        totalEnemy = total;
    }

    private void SetActiveFalseObstacles(int waves)
    {
        switch(waves)
        {
            case 1:
                break;
            case 2:
                    obstacles1.SetActive(false);
                break;
            case 3:
                    obstacles2.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void CountEnemy()
    {
        totalEnemy -= score;
        if (totalEnemy <= 0 && wave <= 2)
        {
            WaveDone();
        }

        // if ()
        // {

        // }
    }

    private void WaveDone()
    {
        wave++;
        OnWaveDone?.Invoke();
    }

    private void StartGame()
    {
        wave = 1;
        obstacles1.SetActive(true);
        obstacles2.SetActive(true);
    }

    private void OnDisable() 
    {
        spawnMap.OnTotalEnemy.RemoveListener(TotalEnemy);
        gameManager.OnStartGame.RemoveListener(StartGame);
    }
}


