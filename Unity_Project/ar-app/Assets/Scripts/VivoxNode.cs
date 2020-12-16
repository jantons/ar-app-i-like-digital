using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;
using UnityEngine.iOS;

public class VivoxNode : MonoBehaviour
{
    VivoxVoiceManager vivox;
    Client _client = new Client();

   [SerializeField] string _userName = "RxNode";
    string _channelName = "ARAPP";

    void Awake()
    {
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            Application.RequestUserAuthorization(UserAuthorization.Microphone);
        vivox = VivoxVoiceManager.Instance;
        _client.Uninitialize();
        _client.Initialize();
        vivox.OnUserLoggedInEvent += LoggedIn;
        vivox.OnUserLoggedOutEvent += LogOut;
    }

    private void LoggedIn()
    {
        Debug.Log(vivox.LoginSession.LoginSessionId.DisplayName);
        JoinChannel();
    }

    void Start()
    {
        LogIn();
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
