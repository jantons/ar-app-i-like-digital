using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
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

        public myAnimator C_ModelAnimator { get => onScreenModel.GetComponent<myAnimator>(); }


        public static ARManager Instance;

        [SerializeField] GameObject findPlane;
        OSC_StreamingController osc_Controller;
        Transform spawnTransform;
        float floorLevel;

        #region Environment Boundaries
        [Header("Room Scale")]
        [SerializeField] float r_width_X;
        [SerializeField] float r_length_Z;
        public float R_width_X { get => r_width_X; set => r_width_X = value; }
        public float R_length_Z { get => r_length_Z; set => r_length_Z = value; }

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

        
        #endregion

        private void Start()
        {
#if UNITY_EDITOR
            //onScreenModel.GetComponent<ProximityController>().setProximitySensor(true, playerObj);
            onScreenModel = GameObject.Find("Wolf");

#endif
            findPlane.SetActive(true);
            StartCoroutine("DelayDetection");

        }

        IEnumerator DelayDetection()
        {
            yield return new WaitForSeconds(3f);
            findPlane.SetActive(false);
            var plane = GameObject.Find("AR Plane Debug Visualizer");
            if (plane == null)
            {
                floorLevel = -1;
            }
            else
            {
                floorLevel = plane.transform.position.y;

            }
            
            spawnTransform = gameObject.transform;
            spawnTransform.position = new Vector3(0, floorLevel, -3);
            InstantiateModel(spawnTransform);
        }



        public void InstantiateModel(Transform spawnReference)
        {
            onScreenModel = Instantiate(GetARModel(), spawnReference.position, Quaternion.identity);

            Vector3 originalRotation = onScreenModel.transform.eulerAngles;
            onScreenModel.transform.LookAt(Camera.main.transform.position, -Vector3.up);
            Vector3 newRotation = onScreenModel.transform.eulerAngles;
            onScreenModel.transform.eulerAngles = new Vector3(originalRotation.x, newRotation.y, originalRotation.z);

            #region OSC
                osc_Controller = OSC_StreamingController.instance;
                osc_Controller.M_Controller_Instance = onScreenModel.GetComponent<ModelController>();
                osc_Controller.Init();
            #endregion

            onScreenModel.GetComponent<ProximityController>().EnableProximity(3f, playerObj.transform);

            

        }

    }
}