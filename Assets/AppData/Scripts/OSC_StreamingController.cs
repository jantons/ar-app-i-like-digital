using UnityEngine;
[RequireComponent(typeof(OSC))]
public class OSC_StreamingController:MonoBehaviour
{
    OSC osc;
    public static OSC_StreamingController instance;
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
    public void SendMessage(string address, string value)
    {
        OscMessage message;

        message = new OscMessage();
        message.address = address;
        message.values.Add(value);
        osc.Send(message);
    }
}
