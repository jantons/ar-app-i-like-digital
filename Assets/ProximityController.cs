using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityController : MonoBehaviour
{
    Transform playerTrans;
    public bool proximitySensor;

    [SerializeField] float safeZoneLimit = 1f;

    bool isNotified = false;

    public void setProximitySensor(bool state, GameObject Player)
    {
        Debug.Log("Hi Me"+state);
        playerTrans = Player.GetComponent<Transform>();
        proximitySensor = state;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (proximitySensor)
        {
            float dist = Vector3.Distance(playerTrans.position, transform.position);
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
        }

    }
}
