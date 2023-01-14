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

    private GameData gameData;
    public List<Vector3> v3 = new List<Vector3>();
    private void Awake() 
    {
    }

    // Start is called before the first frame update
    void Start()
    { 
        // Debug.Log(gameData.)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
