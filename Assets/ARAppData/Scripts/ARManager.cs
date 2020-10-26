﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ARExample
{
    public class ARManager : MonoBehaviour
    {
        int modelLevel = 0;
        [SerializeField] GameObject onScreenModel;
        GameObject playerObj;
        public GameObject Player { get => playerObj; }

        [SerializeField] GameObject[] ARModel;


        public int ModelLevel { get => modelLevel; set => modelLevel = value; }

        public GameObject OnScreenModel { set => onScreenModel = value; }



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
            onScreenModel.GetComponent<ProximityController>().EnableProximity(3f,playerObj.transform);
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

        public void InstantiateModel(Transform spawnReference)
        {
            onScreenModel = Instantiate(GetARModel(), spawnReference.position, Quaternion.identity);

            Vector3 originalRotation = onScreenModel.transform.eulerAngles;
            onScreenModel.transform.LookAt(Camera.main.transform.position, -Vector3.up);
            Vector3 newRotation = onScreenModel.transform.eulerAngles;
            onScreenModel.transform.eulerAngles = new Vector3(originalRotation.x, newRotation.y, originalRotation.z);

            onScreenModel.GetComponent<ProximityController>().EnableProximity(3f, playerObj.transform);

        }
        #endregion


        #region Debug

#if UNITY_EDITOR
        private void Start()
        {
            //onScreenModel.GetComponent<ProximityController>().setProximitySensor(true, playerObj);
            onScreenModel.GetComponent<ProximityController>().EnableProximity(3f, playerObj.transform);
        }


#endif

        #endregion

    }
}