using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{
    int modelLevel = 0;
    [SerializeField]GameObject onScreenModel;
    Transform transformAnchor;
    GameObject playerObj;

    [SerializeField]GameObject[] ARModel;


    public int ModelLevel { get => modelLevel; set => modelLevel = value; }

    public GameObject OnScreenModel { set => onScreenModel = value; }
    
    public Transform TransformAnchor { get => transformAnchor; set => transformAnchor = value; }


    public static ARManager Instance;

    #region Initialization
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
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region Public Funtions
    /// <summary>
    /// Get model by providing ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject GetARModel(int id)
    {
        return ARModel[id];
    }
    /// <summary>
    /// To get model by script maintained id
    /// </summary>
    /// <returns></returns>
    public GameObject GetARModel()
    {
        return ARModel[modelLevel];
    }
    public void resetModel()
    {
        Destroy(onScreenModel);

    }
    public void invokeOnScreenModel(GameObject model)
    {
        //Invoke Model proximity and initialize model
        onScreenModel = model;
        onScreenModel.GetComponent<ProximityController>().setProximitySensor(true,playerObj);
    }
    public void changeModel(int modelLevel)
    {
        Destroy(onScreenModel);
        this.modelLevel = modelLevel;
    }
    public void playAnimation(string newState)
    {
        onScreenModel.GetComponent<Animator>().Play(newState);
    }
    #endregion


    #region Debug

#if UNITY_EDITOR
    private void Start()
    {
        onScreenModel.GetComponent<ProximityController>().setProximitySensor(true, playerObj);
    }
    

#endif

    #endregion

}
