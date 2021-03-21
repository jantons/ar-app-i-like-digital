using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
namespace ARExample
{
    public class ARManager : MonoBehaviour
    {
        Model modelType;
        public static ARManager Instance;
        
        int modelLevel = 0;
        GameObject onScreenModel;
        GameObject playerObj;
        [SerializeField] GameObject[] ARModel;
        public GameObject Player { get => playerObj; }

        Vector3 anchorPosition;
        OSC_Receive OSC_Receive;

        public int ModelLevel { get => modelLevel; set => modelLevel = value; }

        public GameObject OnScreenModel { set => onScreenModel = value; }

        public myAnimator C_ModelAnimator { get => onScreenModel.GetComponent<myAnimator>(); }


        #region Environment Boundaries
        [Header("Room Scale")]
         float r_width_X, r_length_Z;
        public float R_width_X { get => r_width_X; set => r_width_X = value; }
        public float R_length_Z { get => r_length_Z; set => r_length_Z = value; }
        public Vector3 AnchorPosition { get => anchorPosition; set => anchorPosition = value; }
        public Model ModelType { get => modelType; set => modelType = value; }


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
        GameObject getModel()
        {
            switch (ModelType)
            {
                case Model.Wolf:
                    {
                        return ARModel[0];
                        break;
                    }
                case Model.Eagle:
                    {
                        return ARModel[1];
                        break;
                    }
                case Model.Rabit:
                    {
                        return ARModel[2];
                        break;
                    }

                default:
                    {
                        return ARModel[0];
                        break;
                    }
            }
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
            anchorPosition = Vector3.zero;
            if (Instance != null)
            {
                GameObject.Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        private void Start()
        {
            OSC_Receive = OSC_Receive.Instance;
            ModelType = Model.Wolf;
        }
        #endregion

        public void LoadShow()
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

            onScreenModel= Instantiate(getModel(),anchorPosition, Quaternion.Euler(0, 0, 0));
            
            onScreenModel.GetComponent<ModelController>().RotateToCamera();

            #region OSC
            OSC_Receive.ModelController = onScreenModel.GetComponent<ModelController>();
            onScreenModel.GetComponent<PositionStreamOutlet>().init();

            #endregion
            if (ModelType == Model.Wolf)
            {
                onScreenModel.GetComponent<ProximityController>().EnableProximity(3f, playerObj.transform);
            }  
        }
        public void GetConfigurations()
        {
            R_width_X = PlayerPrefs.GetFloat("room_Width");
            R_length_Z = PlayerPrefs.GetFloat("room_Length");
        }

        public void SetModelToUse(Model type)
        {
            switch (type)
            {
                case Model.Wolf:
                    {
                        ModelType = Model.Wolf;
                        OnScreenModel = ARModel[0];
                        break;
                    }
                case Model.Eagle:
                    {
                        ModelType = Model.Eagle;
                        OnScreenModel = ARModel[1];
                        break;
                    }
                case Model.Rabit:
                    {
                        ModelType = Model.Rabit;
                        OnScreenModel = ARModel[2];
                        break;
                    }
            }
        }
    }
}
public enum Model
{ Wolf, Eagle, Rabit }