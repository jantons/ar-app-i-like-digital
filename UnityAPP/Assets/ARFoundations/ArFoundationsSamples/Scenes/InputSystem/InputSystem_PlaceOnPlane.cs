using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using ARExample;

namespace UnityEngine.XR.ARFoundation.Samples
{
    public class InputSystem_PlaceOnPlane : MonoBehaviour
    {
        [SerializeField] GameObject refObject;

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        public void AddObject(InputAction.CallbackContext context)
        {
            var touchPosition = context.ReadValue<Vector2>();
            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    //spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                   // spawnedObject = Instantiate(arManager.GetARModel(), hitPose.position, hitPose.rotation);

                }
            }
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }

}
