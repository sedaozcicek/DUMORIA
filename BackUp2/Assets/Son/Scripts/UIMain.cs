using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public GameObject turkishScreen;
    public GameObject englishScreen;
    public GameObject mainScreen;
    public GameObject PuzzleScreen;

    public static UIMain InstanceUý { get; private set; }

    private void Start()
    {
        
        if (SaveLoadManager.puzzleTr==true)
        {
            SaveLoadManager.puzzleTr = false;
            Puzzle();
        }

        if (PlayerPrefs.HasKey("Lang"))
        {
            if (PlayerPrefs.GetString("Lang") == "Turkish")
            {
                englishScreen.SetActive(false);
                Turkish();
            }
                
            else
                English();
        }

    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Lang");
    }

    public void Turkish()
    {
        mainScreen.SetActive(false);
        PuzzleScreen.SetActive(false);
        turkishScreen.SetActive(true);
        PlayerPrefs.SetString("Lang", "Turkish");
    }
    public void Tr2menu()
    {
        turkishScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    public void English()
    {
        mainScreen.SetActive(false);
        PuzzleScreen.SetActive(false);
        englishScreen.SetActive(true);
        PlayerPrefs.SetString("Lang", "English");
    }
    public void Eng2menu()
    {
        englishScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    

    public void Puzzle()
    {
        mainScreen.SetActive(false);
        PuzzleScreen.SetActive(true);
    }

    public void ButterflyE()
    {
        SaveLoadManager.loadLastGame = 1;
        SaveLoadManager.puzzleTr = true;
        PuzzleScreen.SetActive(false);
        Application.LoadLevel("Puzzle");
        
    }

    public void ButterflyD()
    {
        SaveLoadManager.loadLastGame = 2;
        SaveLoadManager.puzzleTr = true;
        PuzzleScreen.SetActive(false);
        Application.LoadLevel("Puzzle");
    }

    public void Mouse()
    {
        SaveLoadManager.loadLastGame = 3;
        SaveLoadManager.puzzleTr = true;
        PuzzleScreen.SetActive(false);
        Application.LoadLevel("Puzzle");
    }

    public void AlfabeTr()
    {
        Application.LoadLevel("TrAlfabe");
    }

    public void RakamTr()
    {
        Application.LoadLevel("TrRakam");
    }
    public void RakamEng()
    {
        Application.LoadLevel("EngRakam");
    }
    public void AlfabeEng()
    {
        Application.LoadLevel("EngAlfabe");
    }
    public void KendinYapEng()
    {
        Application.LoadLevel("son");
    }
    public void KendinYapTr()
    {
        Application.LoadLevel("sonTr");
    }
    public void Puzzle2menu()
    {
        PuzzleScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
    #else
      Application.Quit();
    #endif
    }
}
