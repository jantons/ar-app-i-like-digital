using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARExample
{

    public class StateStreamOutlet : MonoBehaviour
    {
        public static StateStreamOutlet instance;
        [SerializeField] float refreshRate = 0.1f;
        [SerializeField] List<string> animStates;
        [SerializeField] List<float> animLength;
        string c_state;
        OSC_StreamingController osc_StreamingController;
        AnimatorClipInfo[] m_CurrentClipInfo;

        void Start()
        {
            osc_StreamingController = OSC_StreamingController.instance;

            //anim = gameObject.GetComponent<Animator>();
            //AnimationClip[] animationClips = anim.runtimeAnimatorController.animationClips;
            //// Iterate over the clips and gather their information
            //foreach (AnimationClip animClip in animationClips)
            //{
            //    animStates.Add(animClip.name);
            //    animLength.Add(animClip.length);
            //}


        }
        void Awake()
        {
            instance = this;
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

        public void setState(int state)
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
