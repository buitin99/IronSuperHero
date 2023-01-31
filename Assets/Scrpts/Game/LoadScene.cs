using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

public class LoadScene : MonoBehaviour
{
    public GameObject       loadingScreen;
    public bool             loadOnAwake;
    private AsyncOperation  operation;
    private GameManager     gameManager;
    private GameData        gameData;
    private int             level;

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

    public void LoadNextLevel()
    {
        LoadNewScene(level);
    }

    public void LoadNewScene(int index)
    {
        StartCoroutine(LoadAsync(index));
        operation.completed += InitGame;
    }

    IEnumerator LoadAsync(int index) {
        operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);
        while(!operation.isDone) {
            yield return null;
        }
    }

    private void InitGame(AsyncOperation asyncOperation)
    {
        // gameManager.Init
    }
}
