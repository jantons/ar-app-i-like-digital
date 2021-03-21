using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ARExample
{
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
            }
        }

        public event Action onDangerZoneTriggered;
        public event Action onSafeZoneTriggered;
        public event Action onFollowTriggered;
        public event Action onUnFollowTriggered;
        public event Action onRestTriggered;

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
        public void FollowTriggered()
        {
            if (onFollowTriggered != null)
            {
                onFollowTriggered();
            }
        }
        public void UnFollowTriggered()
        {
            if (onUnFollowTriggered != null)
            {
                onUnFollowTriggered();
            }
        }
        public void RestTriggered()
        {
            if (onRestTriggered != null)
            {
                onRestTriggered();
            }
        }
    }
}
