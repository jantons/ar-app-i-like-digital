using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ARExample;

[RequireComponent (typeof(ARRaycastManager))]


public class TapToPlace : MonoBehaviour
{
    [SerializeField] GameObject objectToInstantiate;
    GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    bool isActive = false;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    ARManager _arManager;
    // Start is called before the first frame update
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        
    }
    private void Start()
    {
        _arManager = ARManager.Instance;
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            touchPosition = default;
            return false;
        }
    }
    void getObjectToInstantiate()
    {
        switch (_arManager.ModelType)
        {
            case Model.Wolf:
                {
                    objectToInstantiate = _arManager.GetCharacterModel(0);
                    break;
                }
            case Model.Eagle:
                {
                    objectToInstantiate = _arManager.GetCharacterModel(1);
                    break;
                }
            case Model.Rabit:
                {
                    objectToInstantiate = _arManager.GetCharacterModel(2);
                    break;
                }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
#if UNITY_EDITOR
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectToInstantiate, transform.position, transform.rotation);
                RotateToCamera();
            }
#endif
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;
            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(objectToInstantiate, hitPose.position, hitPose.rotation);
                    RotateToCamera();
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }

    }
    public void Init()
    {
        isActive = true;
        if (spawnedObject != null)
        {
            spawnedObject.SetActive(true);
        }

    }
    public void SaveSpawnPosition()
    {
        isActive = false;
        if (spawnedObject != null)
        {
            _arManager.AnchorPosition = spawnedObject.transform.position;
            spawnedObject.SetActive(false);
        }
        else
        {
            _arManager.AnchorPosition = Vector3.zero;
        }

    }
    public void RotateToCamera()
    {
        Vector3 targetPostition = new Vector3(Camera.main.transform.position.x,
                                   spawnedObject.transform.position.y,
                                   Camera.main.transform.position.z);
        spawnedObject.transform.LookAt(targetPostition);
    }
}
