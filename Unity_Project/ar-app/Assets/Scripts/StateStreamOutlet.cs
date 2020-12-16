using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARExample
{

    public class StateStreamOutlet : MonoBehaviour
    {
        public static StateStreamOutlet Instance;
        [SerializeField] float refreshRate = 0.1f;

        string c_state;
        OSC_Send osc_StreamingController;
      

        void Awake()
        {
            osc_StreamingController = OSC_Send.Instance;
            Instance = this;
        }

        public void streamStateStart()
        {
            osc_StreamingController.StreamMessage("/animation/state/", 1);
        }
        public void streamStateEnd()
        {
            osc_StreamingController.StreamMessage("/animation/state/", 0);
        }

        public void animName(AnimStates state)
        {
            switch (state)
            {
                case AnimStates.LayingIdle:
                    {
                        osc_StreamingController.StreamMessage("/animation/name/", 1);
                        break;
                    }
                case AnimStates.SittingIdle_1:
                    {
                        osc_StreamingController.StreamMessage("/animation/name/", 2);
                        break;
                    }
                case AnimStates.SittingIdle_2:
                    {
                        osc_StreamingController.StreamMessage("/animation/name/", 3);
                        break;
                    }
            }
        }

        public void sendAnimState(int state)
        {
            Debug.Log(state);
            if (state == 0)
                streamStateEnd();
            else
                streamStateStart();
        }
    }
    public enum AnimStates
    {
        LayingIdle, SittingIdle_1, SittingIdle_2,
    }
}
