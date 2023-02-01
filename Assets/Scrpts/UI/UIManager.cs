using System.Collections.Generic;
using System.Collections;
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


    public GameObject virtualCameraStart;
    public GameObject virtualCameraMain;
    public GameObject               playBtn;
    public GameObject               endBtn;
    public GameObject               shopBtn;
    public GameObject               heroBtn;
    public GameObject               settingBtn;
    public GameObject               playerGo;
    //LoadingScene
    public GameObject               loadingScreen;


    // List Settings

    public GameObject               basicBtn, controlBtn, interfaceBtn, soundBtn, privacyBtn, networkBtn, otherBtn;
    public GameObject               basicPannel, controlPannel, interfacePannel, soundPannel, privacyPannel, networkPannel, otherPannel;
    private List<GameObject>        listBtn;
    private List<GameObject>        listPannel;

    //Button
    public GameObject               settingsBackground;
    public GameObject               shopBackground;    
    public GameObject               heroBackground;   
    private List<GameObject>        backgroundList; 

    private void Awake() 
    {
        gameData = GameData.Load();
        gameManager = GameManager.Instance;
        listBtn = new List<GameObject>();
        listBtn.Add(basicBtn);
        listBtn.Add(controlBtn);
        listBtn.Add(interfaceBtn);
        listBtn.Add(soundBtn);
        listBtn.Add(privacyBtn);
        listBtn.Add(networkBtn);
        listBtn.Add(otherBtn);

        listPannel = new List<GameObject>();
        listPannel.Add(basicPannel);
        listPannel.Add(controlPannel);
        listPannel.Add(interfacePannel);
        listPannel.Add(soundPannel);
        listPannel.Add(privacyPannel);
        listPannel.Add(networkPannel);
        listPannel.Add(otherPannel);

        backgroundList = new List<GameObject>();
        backgroundList.Add(settingsBackground);
        backgroundList.Add(shopBackground);
        backgroundList.Add(heroBackground);
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

        virtualCameraStart.SetActive(false);
        virtualCameraMain.SetActive(true);

        

        StartCoroutine(LoadGame());
    }
    
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(false);
    }

    public void EndGame()
    {
        playBtn.SetActive(true);
        shopBtn.SetActive(true);
        heroBtn.SetActive(true);
        settingBtn.SetActive(true);
    }

    public void ClickButtonInSettings(int id)
    {
        for (int i = 0; i < listPannel.Count; i++)
        {
            listPannel[i].gameObject.SetActive(false);
        }
        listPannel[id].gameObject.SetActive(true);
    }

    public void ClickButton(int id)
    {
        backgroundList[id].gameObject.SetActive(true);
        ClickButtonInSettings(0);
    }

    public void ClickBackButton(int id)
    {
        backgroundList[id].gameObject.SetActive(false);
    }

    public void tesst123()
    {
        gameData.totalLevel.Add(1);
        gameData.Save();
    }






}
