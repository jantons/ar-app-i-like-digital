using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARExample;

public class OSC_Send : MonoBehaviour
{
    OSC osc;
    public static OSC_Send Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        
    }
    private void Start()
    {
        osc = gameObject.GetComponent<OSC>();
    }

    public void SendLocation_XZ(float position_X, float position_Z)
    {
        OscMessage message;

        message = new OscMessage();
        message.address = "position";
        message.values.Add(position_X);
        message.values.Add(position_Z);
        osc.Send(message);
    }
    public void StreamMessage(string address, string value)
    {
        OscMessage message;

        message = new OscMessage();
        message.address = address;
        message.values.Add(value);
        osc.Send(message);
    }
    public void StreamMessage(string address, int value)
    {
        OscMessage message;

        message = new OscMessage();
        message.address = address;
        message.values.Add(value);
        osc.Send(message);
    }
    public void StreamMessage(string address, float value)
    {
        OscMessage message;

        message = new OscMessage();
        message.address = address;
        message.values.Add(value);
        osc.Send(message);
    }

}
