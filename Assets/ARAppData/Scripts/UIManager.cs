using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    ARManager arManager;
    // Start is called before the first frame update
    void Start()
    {
        arManager = ARManager.Instance;
        
    }

    #region UI Call Fucntions
    public void PlayAnimation(string clipName)
    {
        arManager.playAnimation(clipName);
    }
    public void ChangeModel(int level)
    {
        arManager.changeModel(level);
    }
    public void resetModel()
    {
        arManager.resetModel();
    }
    #endregion
}
