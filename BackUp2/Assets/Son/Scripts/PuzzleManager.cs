using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject conf;
    int yerlestirilen_parca;
    int toplam_s2 = 20;
    int toplam_s1 = 12;
    int toplam_s3 = 55;
    private static object hit;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;

    private void Start()
    {
        
        if (SaveLoadManager.loadLastGame == 1)
        {
            ButterflyE();
            Debug.Log("LoasLastGame is 1");
        }
        if (SaveLoadManager.loadLastGame == 2)
        {
            ButterflyD();
            Debug.Log("LoasLastGame is 2");
        }
        if (SaveLoadManager.loadLastGame == 3)
        {
            PuzzleDog();
            Debug.Log("LoasLastGame is 3");
        }
    }

    private IEnumerator LoadNextScene()
    {
        conf.SetActive(true);
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("MainMenu");
    }
    
    public void SayiArtir()
    {
        yerlestirilen_parca++;
        if (s1.activeSelf == true)
        {
            if (yerlestirilen_parca == toplam_s1)
            {
                StartCoroutine(LoadNextScene());
                
            }
        }

        if (s2.activeSelf == true)
        {
            if (yerlestirilen_parca == toplam_s2)
            {
                StartCoroutine(LoadNextScene());
            }
        }

        if (s3.activeSelf == true)
        {
            if (yerlestirilen_parca == toplam_s3)
            {
                StartCoroutine(LoadNextScene());
            }
        }

    }

    public void ButterflyE()
    {
        s1.SetActive(true);
        s2.SetActive(false);
        s3.SetActive(false);
    }

    public void ButterflyD()
    {
        s1.SetActive(false);
        s2.SetActive(true);
        s3.SetActive(false);
    }

    public void PuzzleDog()
    {
        s1.SetActive(false);
        s2.SetActive(false);
        s3.SetActive(true);
    }

    public void Repeat()
    {
        Application.LoadLevel("puzzle");

    }

    public void Back()
    {
        
        SaveLoadManager.puzzleTr = true;

        Application.LoadLevel("MainMenu");
    }
}
