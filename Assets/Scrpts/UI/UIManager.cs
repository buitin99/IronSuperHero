using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Animator animator;
    private int      alertHash;
    private int      nonAlertHash;
    private int      hitPointHash;
    private float    curentTimeCombo = 3f;

    public List<Vector3> ts = new List<Vector3>();  
    public List<Vector3> tg = new List<Vector3>();   

    private GameData gameData;
    private Maps maps;
    private MapInitialization mapInitialization;
    private void Awake() 
    {
        gameData = GameData.Load();
        // ts.Add(Vector3.zero);
        ts.Add(Vector3.back);
        tg.Add(Vector3.back);

        mapInitialization = FindObjectOfType<MapInitialization>();


    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        // Maps maps = new Maps(1, 1, 1, 1, 1, 1, ts, tg);

        // gameData.mapsInfor.Add(maps);
        // gameData.Save();
        mapInitialization.SaveDataToJson();
    }
}
