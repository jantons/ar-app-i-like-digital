using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script has to be attached on model with animator
/// </summary>
///
[RequireComponent(typeof(Animator))]
public class myAnimator : MonoBehaviour
{
    [SerializeField] GameObject model_Level_1;
    [SerializeField] GameObject model_Level_2;
    [SerializeField] GameObject model_Level_3;
    [SerializeField] GameObject model_Level_4;
    GameObject enabledModel;
    public GameObject GetEnabledModel { get => enabledModel; }
    Transform spawnedTransform;

    Animator modelAnimator;
    string currentState;

    private void Start()
    {
        DefaultLoad();
    }
    void DefaultLoad()
    {
        enableModel(1);
    }


    /// <summary>
    /// To get specific model reference which will have animator component attached to it.
    /// </summary>
    /// <param name="modelLevel">Different models depending on level of details ARMAanger will have the main reference </param>
    /// <returns></returns>
    GameObject getAnimatorModel (int modelLevel)
    {
        GameObject objectToReturn = default;
        switch (modelLevel)
        {
            case 1:
                {
                    objectToReturn = model_Level_1;
                    break;
                }
            case 2:
                {
                    objectToReturn = model_Level_2;
                    break;
                }
            case 3:
                {
                    objectToReturn = model_Level_3;
                    break;
                }
            case 4:
                {
                    objectToReturn = model_Level_4;
                    break;
                }
        }
        return objectToReturn;
    }
    /// <summary>
    /// Will enable model with animator on screen having input of model level
    /// </summary>
    /// <param name="modelLevel">input will be provided </param>
    public void enableModel(int modelLevel)
    {
        model_Level_1.SetActive(false);
        model_Level_2.SetActive(false);
        model_Level_3.SetActive(false);
        model_Level_4.SetActive(false);

        switch (modelLevel)
        {
            case 1:
                {
                    model_Level_1.SetActive(true);
                    modelAnimator = model_Level_1.GetComponent<Animator>();
                    break;
                }
            case 2:
                {
                    model_Level_2.SetActive(true);
                    modelAnimator = model_Level_2.GetComponent<Animator>();
                    break;
                }
            case 3:
                {
                    model_Level_3.SetActive(true);
                    modelAnimator = model_Level_3.GetComponent<Animator>();
                    break;
                }
            case 4:
                {
                    model_Level_4.SetActive(true);
                    modelAnimator = model_Level_4.GetComponent<Animator>();
                    break;
                }
        }
        spawnedTransform = modelAnimator.gameObject.GetComponent<Transform>();
        
    }

    public void playAnimation(string newState)
    {
       // if (currentState == newState) return;

        modelAnimator.Play(newState);
        ///modelAnimator.CrossFade(newState);
        currentState = newState;
    }
    public void resetModel()
    {
        modelAnimator.GetComponent<Transform>().position = new Vector3(0,0,0);
        modelAnimator.GetComponent<Transform>().eulerAngles = new Vector3(0, 150, 0);

    }
    public void OnReceivePush15(OscMessage message)
    {
        Debug.Log(message.GetFloat(0));
    }
}
