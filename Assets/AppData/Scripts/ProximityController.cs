using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARExample
{
    public class ProximityController : MonoBehaviour
    {
        Transform playerTrans;
        public Transform GetPlayerTrans { get => playerTrans; }
        public bool proximitySensor;

        [SerializeField] float safeZoneLimit = 1f;
        [SerializeField] float startfollowLimit = 4f;
        [SerializeField] float stopfollowLimit = 3f;

        bool isNotified = false;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (proximitySensor)
            {
                float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
                if (dist < safeZoneLimit)
                {
                    // Danger Zone

                    SessionEvents.instance.DangerZoneTriggered();
                }
                else
                {
                    // Safe Zone
                    SessionEvents.instance.SafeZoneTriggere();
                }
                // Follow and Unfollow

                if (dist > startfollowLimit && dist > stopfollowLimit)
                {
                    SessionEvents.instance.FollowTriggered();
                }
                else
                {
                    SessionEvents.instance.UnFollowTriggered();
                }

            }

        }
        public void EnableProximity(float delay, Transform refPlayerTrans)
        {
            StartCoroutine(DelayEnableProximity(delay,refPlayerTrans));
        }
        public void setProximitySensor(bool state, GameObject Player)
        {
            playerTrans = Player.GetComponent<Transform>();
            proximitySensor = state;
        }
        IEnumerator DelayEnableProximity(float delay, Transform refPlayerTrans)
        {
            yield return new WaitForSeconds(delay);
            proximitySensor = true;
            playerTrans = refPlayerTrans;
            gameObject.GetComponent<ModelController>().SetModelControllerState(true, playerTrans);
        }
    }
}
