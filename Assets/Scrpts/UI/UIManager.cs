using UnityEngine;
using Cinemachine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Animator    animator;
    private int         alertHash;
    private int         nonAlertHash;
    private int         hitPointHash;
    private float       curentTimeCombo = 3f;
    private GameData    gameData;
    private GameManager gameManager;
    private int         level;

    public CinemachineVirtualCamera virtualCamera;
    public GameObject               playBtn;
    public GameObject               endBtn;
    public GameObject               shopBtn;
    public GameObject               heroBtn;
    public GameObject               settingBtn;
    public GameObject               playerGo;

    //LoadingScene
    public GameObject               loadingScreen;

    
    private void Awake() 
    {
        gameData = GameData.Load();
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayGame()
    {
        loadingScreen.SetActive(true);
        gameData = GameData.Load();
        gameManager.StartGame();

        playBtn.SetActive(false);
        shopBtn.SetActive(false);
        heroBtn.SetActive(false);
        settingBtn.SetActive(false);

    }

    public void EndGame()
    {
        playBtn.SetActive(true);
        shopBtn.SetActive(true);
        heroBtn.SetActive(true);
        settingBtn.SetActive(true);

        // if ()
        // {
        //     gameData.mapsInfor[gameData.mapsInfor.].currentLevels
        // }

    }

}
