using UnityEngine;
public class OSC_Adapter : MonoBehaviour
{
    public static OSC_Adapter Instance;
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
    public void InIt(string ip )
    {
        GetComponent<OSC>().inIT(ip);
    }
    
}
