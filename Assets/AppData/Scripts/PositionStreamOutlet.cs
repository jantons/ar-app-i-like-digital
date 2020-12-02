using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARExample
{

    public class PositionStreamOutlet : MonoBehaviour
    {
        public static PositionStreamOutlet instance;
        OSC_StreamingController osc_StreamingController;
        bool isActive;
        // Start is called before the first frame update
        void Start()
        {
            osc_StreamingController = OSC_StreamingController.instance;
        }
        void Awake()
        {
                instance = this;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (isActive)
            {
                isActive = false;

                osc_StreamingController.StreamMessage("/locationX/", transform.position.x);
                osc_StreamingController.StreamMessage("/locationZ/", transform.position.z);
            }
        }
        public void init()
        {
            isActive = true;
        }
       

        
    }
   
}
