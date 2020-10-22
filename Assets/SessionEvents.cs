using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionEvents : MonoBehaviour
{
    public static SessionEvents instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public event Action onDangerZoneTriggered;
    public event Action onSafeZoneTriggered;

    public void DangerZoneTriggered()
    {
        if (onDangerZoneTriggered != null)
        {
            onDangerZoneTriggered();
        }
    }
    public void SafeZoneTriggere()
    {
        if (onSafeZoneTriggered != null)
        {
            onSafeZoneTriggered();
        }
    }
}
