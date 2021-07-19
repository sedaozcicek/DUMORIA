using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    Camera camera;
    Vector3 startPos;
    GameObject[] boxArray;
    PuzzleManager pm;
    private Vector3 screenPoint;
    private Vector3 offset;
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.localPosition = cursorPosition;
    }
    void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
        startPos = transform.localPosition;
        boxArray = GameObject.FindGameObjectsWithTag("kutu");
        pm = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach (GameObject kutu in boxArray)
            {
                if (kutu.name == gameObject.name)
                {
                    float mesafe = Vector3.Distance(kutu.transform.position, transform.position);
                    if (mesafe <= 0.5f)
                    {
                        transform.position = kutu.transform.position;
                        pm.SayiArtir();
                        this.enabled = false;
                    }
                    else
                        transform.localPosition = startPos;
                }
            }
        }
    }
}
