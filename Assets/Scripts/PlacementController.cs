using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR;
using UnityEngine.XR.ARSubsystems;

public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    ARRaycastManager aRRaycastManager;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }


    public GameObject PlacedPrefab
    {
        get { return placedPrefab; }
        set { placedPrefab = value; }
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;


        if (aRRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPos = hits[0].pose;
            Instantiate(placedPrefab, hitPos.position, hitPos.rotation);
        }



    }
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}


