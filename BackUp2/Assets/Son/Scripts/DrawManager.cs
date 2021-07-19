using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DrawManager : MonoBehaviour
{
    public Transform tutorialFinger;
    public GameObject nextButtonActive;
    public List<GameObject> mainList;
    public List<GameObject> meshRenderer;
    public GameObject drawPrefab;
    public GameObject theTrail;
    private GameObject temp;
    public GameObject confetti1;
    public GameObject confetti2;
    Plane planeObj;
    Vector3 startPos;
    int Index = 0;
    int green = 0;
    int wait = 0;

    private Sequence _tutorialSequence;

    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, this.transform.position);
        Vector3 nextButtonPrevScale = nextButtonActive.transform.lossyScale;

        nextButtonPrevScale.x += 0.5f;
        nextButtonPrevScale.y += 0.5f;
        nextButtonPrevScale.z += 0.5f;

        nextButtonActive.transform.DOScale(nextButtonPrevScale, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Lang");
    }
    void Update()
    {
        if (Index % 2 == 0 && Index != 0  )
        {
            nextButtonActive.SetActive(false);
            GameManager.Instance.isDecidedTalking = false;
            if (wait % 2 == 0 || wait==1 )
            {
                GameManager.Instance.isWait = true;
                wait++;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.isWait = false;
                GameManager.Instance.isDecidedThinking = true;
                theTrail = (GameObject)Instantiate(drawPrefab, this.transform.position, Quaternion.identity);
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float _dis;
                if (planeObj.Raycast(mouseRay, out _dis))
                {
                    startPos = mouseRay.GetPoint(_dis);
                }
            }

            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float _dis;
                if (planeObj.Raycast(mouseRay, out _dis))
                {
                    theTrail.transform.position = mouseRay.GetPoint(_dis);
                }
            }
            boya();
        }
    }
    void boya()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        for (int i = 0; i < meshRenderer.Count; i++)
        {
            if (Physics.Raycast(ray, out hit, 20))
            {
               
                if (hit.transform.gameObject.tag.Equals("obj0"))
                {
                    
                    Debug.Log("Týklandý..");
                    
                    if (hit.transform.GetComponent<Renderer>().material.color != Color.green)
                    {
                        hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        green++;

                        if (_tutorialSequence != null)
                        {
                            //Debug.LogWarning("Tutorial Ended!!!");
                            _tutorialSequence.Kill();
                            _tutorialSequence = null;
                        }
                    }
                    if (hit.transform.parent.childCount == green)
                    {
                        GameManager.Instance.isDecidedThinking = false;
                        green++;

                        confetti1.SetActive(true);
                        confetti1.GetComponent<ParticleSystem>().Play(true);
                        GameManager.Instance.isChickenDance = true;
                        mainList[Index - 2].SetActive(true);
                        mainList[Index - 1].SetActive(false);

                        temp = mainList[Index - 2];
                        Color prevColor = mainList[Index - 2].GetComponent<MeshRenderer>().material.color;
                        mainList[Index - 2].GetComponent<MeshRenderer>().material.color = Color.clear;
                        mainList[Index - 2].GetComponent<MeshRenderer>().material.DOColor(prevColor, 3f).OnComplete(() =>
                        {
                            GameManager.Instance.isChickenDance = true;                            
                            confetti2.SetActive(true);
                            confetti2.GetComponent<ParticleSystem>().Play(true);

                            StartCoroutine(NextObjDelay());

                        });


                    }
                }
            }

        }
    }
    private IEnumerator NextObjDelay()
    {

        yield return new WaitForSeconds(2f);
        
        nextObj();
    }

    private int j = 0;
    private bool ilkGiris = true;
    public void nextObj()
    {
        GameManager.Instance.isDance = false;
        GameManager.Instance.isChickenDance = false;
        GameManager.Instance.isDecidedThinking = false;
        green = 0;
        ++Index;
        
        if (Index % 2 == 1)
        {
            GameManager.Instance.isDecidedTalking = true;
            wait++;
            
        }
        if (temp != null)
        {
            temp.SetActive(false);
        }
        if (ilkGiris == false)
        {
            j++;
            ilkGiris = true;
        }
        Debug.Log("J degeri: " + j);
        boya();
        for (int i = 0; i < mainList.Count; i++)
        {
            if (j == i)
            {
                StartCoroutine(ActiveObjDelay(mainList[i], i));
                temp = mainList[i];
            }
            if (j > 0 && j == i)
                mainList[i - 1].SetActive(false);

           
        }
        j++;
    }

    private IEnumerator ActiveObjDelay(GameObject go, int goIndex)
    {
        
        yield return new WaitForSeconds(3f);
        go.SetActive(true);
        nextButtonActive.SetActive(true);
        
        _tutorialSequence = DOTween.Sequence();

        if (ilkGiris && goIndex % 2 != 0)
        {
            tutorialFinger.gameObject.SetActive(true);
            Vector3 firstTutorialFingerPos = go.transform.GetChild(0).position;
            firstTutorialFingerPos.z -= 0.1f;
            tutorialFinger.position = firstTutorialFingerPos;

            foreach (Transform goChildTransform in go.transform)
            {
                Vector3 tutorialDestPoint = goChildTransform.position;
                tutorialDestPoint.z -= 0.1f;
                _tutorialSequence.Append(tutorialFinger.DOMove(tutorialDestPoint, 0.2f));
            }

            _tutorialSequence.SetLoops(-1, LoopType.Yoyo);
            _tutorialSequence.SetEase(Ease.InOutSine);
            _tutorialSequence.OnKill(() => tutorialFinger.gameObject.SetActive(false));

            
            _tutorialSequence.Play();
        }
    }
}
