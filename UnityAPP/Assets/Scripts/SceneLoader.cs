using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Button transmitter_BTN;
    [SerializeField] Button reciever_BTN;
    // Start is called before the first frame update

    private void Start()
    { 
        transmitter_BTN.onClick.AddListener(() => loadScene("scene_Transmitter"));
        reciever_BTN.onClick.AddListener(() => loadScene("scene_Receiver"));
    }

    // Update is called once per frame
    void loadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
