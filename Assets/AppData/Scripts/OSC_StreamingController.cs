using UnityEngine;
using ARExample;
[RequireComponent(typeof(OSC))]
public class OSC_StreamingController:MonoBehaviour
{
    OSC osc;
    public static OSC_StreamingController instance;
    ModelController m_Controller;
    public ModelController M_Controller_Instance { set => m_Controller = value; }
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

    private void Update()
    {
        SendMessage("/test/1", "hello");
    }


    // Use this for initialization
    void Start()
    {

    }

    public void Init()
    {
        osc.SetAddressHandler("/1/push15", m_Controller.OnReceivePush15);
        //osc.SetAddressHandler("/1/push15", OnReceivePush15);
        osc.SetAddressHandler("/1/stop1", m_Controller.OnReceiveStop1);
        osc.SetAddressHandler("/1/stop2", m_Controller.OnReceiveStop2);
        osc.SetAddressHandler("/1/stop3", m_Controller.OnReceiveStop3);
        osc.SetAddressHandler("/1/stop4", m_Controller.OnReceiveStop4);
        osc.SetAddressHandler("/1/stop5", m_Controller.OnReceiveStop5);
        osc.SetAddressHandler("/1/stop6", m_Controller.OnReceiveStop6);

    }


}
