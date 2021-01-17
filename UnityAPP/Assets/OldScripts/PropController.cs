using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARExample;

public class PropController : MonoBehaviour
{
    bool isActive = false;
    [SerializeField] GameObject prop;
    ARManager _arManager;
    private void Start()
    {
        _arManager = ARManager.Instance;
    }
    public void onReceivePropInvoke()
    {

            if (!isActive)
            {
                isActive = true;
                prop.transform.position = new Vector3(prop.transform.position.x,
                                                        _arManager.AnchorPosition.y,
                                                        prop.transform.position.z);
                prop.SetActive(true);
            }
            else
            {
                isActive = false;
                prop.SetActive(false);
            }
    }
}
