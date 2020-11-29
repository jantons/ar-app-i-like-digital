using UnityEngine;
using ARExample;
[RequireComponent(typeof(OSC))]
public class OSC_StreamingController:MonoBehaviour
{
    OSC osc;
    public static OSC_StreamingController instance;
    ModelController m_Controller;
    public ModelController M_Controller_Instance { set => m_Controller = value; }

    [SerializeField]PropController propController;
    PositionStreamOutlet osc_PositionStreamOutlet;
    StateStreamOutlet osc_StateStreamOutlet;
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
    public void Init()
    {

        osc.SetAddressHandler("/1/push15", m_Controller.OnReceivePush1);

        osc.SetAddressHandler("/1/stop1", m_Controller.OnReceiveStop10);
        osc.SetAddressHandler("/1/stop2", m_Controller.OnReceiveStop11);
        osc.SetAddressHandler("/1/stop3", m_Controller.OnReceiveStop12);
        osc.SetAddressHandler("/1/stop4", m_Controller.OnReceiveStop13);
        osc.SetAddressHandler("/1/stop5", m_Controller.OnReceiveStop14);

        osc.SetAddressHandler("/1/prop",propController.onReceivePropInvoke );

        osc_PositionStreamOutlet = m_Controller.GetComponent<PositionStreamOutlet>();
        osc_StateStreamOutlet = m_Controller.GetComponent<StateStreamOutlet>();

       osc_PositionStreamOutlet.init();
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
