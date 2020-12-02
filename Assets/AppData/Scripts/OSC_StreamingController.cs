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
        Debug.Log("Init");

        osc.SetAddressHandler("/action/auto", m_Controller.OnReceiveAction_Auto);

        osc.SetAddressHandler("/state/1", m_Controller.OnReceiveState_1);
        osc.SetAddressHandler("/state/2", m_Controller.OnReceiveState_2);
        osc.SetAddressHandler("/state/3", m_Controller.OnReceiveState_3);
        osc.SetAddressHandler("/state/4", m_Controller.OnReceiveState_4);
        osc.SetAddressHandler("/state/5", m_Controller.OnReceiveState_5);

       osc.SetAddressHandler("/action/prop",propController.onReceivePropInvoke );
       //osc.SetAddressHandler("/action/prop", printme);

        osc_PositionStreamOutlet = m_Controller.GetComponent<PositionStreamOutlet>();
        osc_StateStreamOutlet = m_Controller.GetComponent<StateStreamOutlet>();

       osc_PositionStreamOutlet.init();
    }

    void printme(OscMessage message)
    {
        if (message.GetFloat(0) == 1)
        {
            Debug.Log("Hi");
        }
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
