using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARExample
{

    public class PositionStreamOutlet : MonoBehaviour
    {
        OSC_Send osc_Send;
        bool isActive;
        // Start is called before the first frame update
        void Start()
        {
            osc_Send = OSC_Send.Instance;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (isActive)
            {
                isActive = false;

                osc_Send.StreamMessage("/locationX/", transform.position.x);
                osc_Send.StreamMessage("/locationZ/", transform.position.z);
            }
        }
        public void init()
        {
            isActive = true;
            Debug.Log("PositionStremInit");
        }
        
    }
   
}
