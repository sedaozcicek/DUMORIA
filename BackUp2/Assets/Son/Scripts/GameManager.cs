using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool isDance = false;
    public bool isWait = false;
    public bool isDecidedTalking = false;
    public bool isChickenDance = false;
    public bool isDecidedThinking = false;
    public bool isSambaDance = false;


    [SerializeField]
    private float delay = 0.3f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
        if (Instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Lang");
    }
    public void BackTr()
    {
        Application.LoadLevel("MainMenu");
        SaveLoadManager.puzzleTr = false;
        PlayerPrefs.SetString("Lang", "Turkish");
    }
    public void BackEng()
    {
        Application.LoadLevel("MainMenu");
        SaveLoadManager.puzzleTr = false;
        PlayerPrefs.SetString("Lang", "English");
    }
    
}
