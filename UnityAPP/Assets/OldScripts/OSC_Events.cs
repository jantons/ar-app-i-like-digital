using System;
using UnityEngine;
namespace ARExample
{
    namespace OSC_Events
    {
        public class OSC_Events : MonoBehaviour
        {
            public static OSC_Events instance;
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

            public event Action onSend_OSC_Message;
            public void Send_OSC_Message()
            {
                if (onSend_OSC_Message != null)
                {
                    onSend_OSC_Message();
                }
            }

        }
    }

}


