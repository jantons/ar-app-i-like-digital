using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

public class ArTapSpawn : MonoBehaviour
{
    [SerializeField] TrackableType trackableTypeMask = TrackableType.Planes;
    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARSessionOrigin arOrigin;
    private Pose placementPose = default;
    private bool placementPoseIsValid = false;
    private bool isSpawned = false;

    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isSpawned)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {

        isSpawned = true;
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);

    }


    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hitsList = new List<ARRaycastHit>();
        var rayCastMgr = arOrigin.GetComponent<ARRaycastManager>();
        rayCastMgr.Raycast(screenCenter, hitsList, trackableTypeMask);

        placementPoseIsValid = hitsList.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hitsList[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
