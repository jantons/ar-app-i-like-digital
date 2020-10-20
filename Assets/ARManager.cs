using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{
    int modelLevel = 0;
    public int ModelLevel { get => modelLevel; set => modelLevel = value; }

    [SerializeField]GameObject[] ARModelRoot;

    public GameObject GetARModel(int id)
    {
        return ARModelRoot[id];
    }
    public GameObject GetARModel()
    {
        return ARModelRoot[modelLevel];
    }

    GameObject onScreenModel;

    public GameObject OnScreenModel { set => onScreenModel = value; }

    Transform transformAnchor;
    public Transform TransformAnchor { get => transformAnchor; set => transformAnchor = value; }

    public static ARManager Instance;
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

    public void resetModel()
    {
        Destroy(onScreenModel);

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


}
