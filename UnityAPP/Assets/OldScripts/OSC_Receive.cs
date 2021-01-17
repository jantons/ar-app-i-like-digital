using UnityEngine;
using ARExample;
[RequireComponent(typeof(OSC))]
public class OSC_Receive:MonoBehaviour
{
    OSC osc;
    public static OSC_Receive Instance;
    ModelController m_Controller;
    [SerializeField]PropController propController;

    public ModelController ModelController { get => m_Controller; set => m_Controller = value; }

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
        osc = gameObject.GetComponent<OSC>();

    }
    void Start()
    {

        osc.SetAllMessageHandler(OnReceiveMessages);

    }
    void OnReceiveMessages(OscMessage message)
    {
        if (message.address == "/action/auto")
        {
            if (message.GetFloat(0) == 1)
                m_Controller.OnReceiveAction_Auto();
        }
        else if (message.address == "/state/1")
        {
            if (message.GetFloat(0) == 1)
                m_Controller.OnReceiveState_1();
        }
        else if (message.address == "/state/2")
        {
            if (message.GetFloat(0) == 1)
                m_Controller.OnReceiveState_2();
        }
        else if (message.address == "/state/3")
        {
            if (message.GetFloat(0) == 1)
                m_Controller.OnReceiveState_3();
        }
        else if (message.address == "/state/4")
        {
            if (message.GetFloat(0) == 1)
                m_Controller.OnReceiveState_4();
        }
        else if (message.address == "/state/5")
        {
            if (message.GetFloat(0) == 1)
                m_Controller.OnReceiveState_5();
        }
        else if (message.address == "/action/prop")
        {
            if (message.GetFloat(0) == 1)
                propController.onReceivePropInvoke();
        }
    }


}