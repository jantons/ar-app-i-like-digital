using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransmitterUI : MonoBehaviour
{
    [SerializeField] Button loginButton;
    [SerializeField] Button logoutButton;
    [SerializeField] TextMeshProUGUI status;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoggedInUI()
    {
        loginButton.interactable = false;
        logoutButton.interactable = true;
        status.text = " Successfully Loged In !";
    }
    public void OnLoggedOutUI()
    {
        loginButton.interactable = true;
        logoutButton.interactable = false;
        status.text = " Successfully Loged Out !";
    }
}
