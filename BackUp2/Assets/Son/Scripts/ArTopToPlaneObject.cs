using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ArTopToPlaneObject : MonoBehaviour
{
    public GameObject gameObjectToInstantiate;
    private GameObject spawnObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    private bool checkAdviser;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        checkAdviser = false;
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (checkAdviser==false)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;
            if (_arRaycastManager.Raycast(touchPosition, hits, trackableTypes: TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                if (spawnObject == null)
                {
                    spawnObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnObject.transform.position = hitPose.position;
                }
            }

            checkAdviser = true;
        }
    }
}
