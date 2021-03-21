using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;
using UnityEngine.iOS;

public class VivoxNode : MonoBehaviour
{
    public static VivoxNode Instance;
    VivoxVoiceManager vivox;
    Client _client = new Client();

   [SerializeField] string _userName = "RxNode";
    string _channelName = "ARAPP";

    public bool LoginStatus() { if (vivox.LoginState == LoginState.LoggedIn) return true; else return false; }

void Awake()
    {
        Instance = this;
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            Application.RequestUserAuthorization(UserAuthorization.Microphone);
        vivox = VivoxVoiceManager.Instance;
        _client.Uninitialize();
        _client.Initialize();
        vivox.OnUserLoggedInEvent += LoggedIn;
        vivox.OnUserLoggedOutEvent += LoggedOut;

        if (_userName.Equals("TxNode"))
        {
            if (vivox.LoginState == LoginState.LoggedIn)
                GetComponent<TransmitterUI>().OnLoggedInUI();
            else
                GetComponent<TransmitterUI>().OnLoggedOutUI();
        }
        
    }


    private void LoggedIn()
    {
        if (_userName.Equals("TxNode"))
        {
            GetComponent<TransmitterUI>().OnLoggedInUI();
        }
        Debug.Log(vivox.LoginSession.LoginSessionId.DisplayName);
        JoinChannel();
    }
    private void LoggedOut()
    {
        if (_userName.Equals("TxNode"))
        {
            GetComponent<TransmitterUI>().OnLoggedOutUI();
        }
    }

    void Start()
    {
      

        // if(_userName.Equals("RxNode"))
        // LogIn();
    }

    void init_Client()
    {
        _client.Uninitialize();
        _client.Initialize();
    }

    public void LogIn()
    {
        vivox.Login(_userName);
    }
    public void LogIn(string name) { vivox.Login(name); }

    public void LogOut()
    {
        vivox.DisconnectAllChannels();
        if (vivox.LoginState == LoginState.LoggedIn)
            vivox.Logout();
        _client.Uninitialize();

    }
    void JoinChannel()
    {
        if (vivox.LoginState == LoginState.LoggedIn)
        {
            vivox.JoinChannel(_channelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.AudioOnly);
        }
    }

    void OnApplicationQuit()
    {
        //if (vivox.LoginState == LoginState.LoggedIn)
        //  vivox.Logout();
        vivox.DisconnectAllChannels();
        _client.Uninitialize();
    }

    private void OnDestroy()
    {
        vivox.OnUserLoggedInEvent -= LoggedIn;
        vivox.OnUserLoggedOutEvent -= LogOut;
    }
}
