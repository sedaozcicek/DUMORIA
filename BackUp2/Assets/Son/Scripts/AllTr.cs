using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllTr : MonoBehaviour
{
    public List<GameObject> mainList;
    GameObject temp;
    public GameObject effect1;
    public GameObject effect2;
    private int j = 0;
    private bool ilkGiris = true;

    private IEnumerator Start()
    {
        
        effect1.SetActive(false);
        effect2.SetActive(false);
        Debug.Log("Starttasýn");
        yield return null;
        StartCoroutine(nextObj());
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Lang");
    }
    
    private IEnumerator nextObj()
    {
        
        yield return new WaitForSeconds(3f);
        Debug.Log("Next obj içindesin");
        if (temp != null)
        {
            Debug.Log("temp null deðil");
            temp.SetActive(false);
        }
        if (ilkGiris == false)
        {
            j++;
            ilkGiris = true;
        }
        Debug.Log("J degeri: " + j);
        for (int i = 0; i < mainList.Count; i++)
        {
            mainList[i].SetActive(true);
            mainList[i].transform.DORotate(Vector3.one * 360f, 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() =>
            {
                mainList[i].transform.DOLookAt(FindObjectOfType<Camera>().transform.position, 1f).SetEase(Ease.Linear);
            });
            yield return new WaitForSeconds(3f);
            mainList[i].SetActive(false);
        }
        effect1.SetActive(true);
        effect2.SetActive(true);
    }
}
