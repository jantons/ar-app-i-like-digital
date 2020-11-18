using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
namespace ARExample
{
    public class ARManager : MonoBehaviour
    {
        public static ARManager Instance;
        [SerializeField] Transform spawnTransform; // Position where model has to be spawned
        int modelLevel = 0;
        GameObject onScreenModel;
        GameObject playerObj;
        [SerializeField] GameObject[] ARModel;
        public GameObject Player { get => playerObj; }

        public int ModelLevel { get => modelLevel; set => modelLevel = value; }

        public GameObject OnScreenModel { set => onScreenModel = value; }

        public myAnimator C_ModelAnimator { get => onScreenModel.GetComponent<myAnimator>(); }

        OSC_StreamingController osc_Controller;

        #region Environment Boundaries
        [Header("Room Scale")]
        [SerializeField] float r_width_X;
        [SerializeField] float r_length_Z;
        [SerializeField] float r_floor_Y;
        public float R_width_X { get => r_width_X; set => r_width_X = value; }
        public float R_length_Z { get => r_length_Z; set => r_length_Z = value; }
        public float R_floor_Y { get => r_floor_Y; set => r_floor_Y = value; }

        #endregion

        #region Public Funtions

        public GameObject GetCharacterModel(int id)
        {
            return ARModel[id];
        }
        public GameObject GetCharacterModel()
        {
            return ARModel[modelLevel];
        }
        public void resetModel()
        {
            Destroy(onScreenModel);
        }
        public void initCharacter(GameObject model)
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

        #endregion
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

        private void Start()
        {
            GetConfigurations();
           
            StartCoroutine("DelayInstantiate");

        }

        IEnumerator DelayInstantiate()
        {
            yield return new WaitForSeconds(3f);
            InstantiateModel();
        }
        public void InstantiateModel()
        {
            spawnTransform.position =  new Vector3(spawnTransform.position.x, R_floor_Y, spawnTransform.position.z);

            onScreenModel= Instantiate(GetCharacterModel(0),spawnTransform.position,spawnTransform.rotation);
            
            onScreenModel.GetComponent<ModelController>().RotateToCamera();

            #region OSC
            osc_Controller = OSC_StreamingController.instance;
            osc_Controller.M_Controller_Instance = onScreenModel.GetComponent<ModelController>();
            osc_Controller.Init();
            #endregion

            onScreenModel.GetComponent<ProximityController>().EnableProximity(3f, playerObj.transform);
        }
        public void GetConfigurations()
        {
            R_width_X = PlayerPrefs.GetFloat("room_Width");
            R_length_Z = PlayerPrefs.GetFloat("room_Length");
            R_floor_Y = PlayerPrefs.GetFloat("room_floor");
        }

    }
}