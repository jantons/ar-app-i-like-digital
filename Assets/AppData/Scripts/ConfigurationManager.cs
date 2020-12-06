using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ARExample
{

    public class ConfigurationManager : MonoBehaviour
    {
        
        #region UI
        [SerializeField] Button callibrate_Btn;
        [SerializeField] Button save_floorlevel_Btn;
        [SerializeField] Button saveCallibration_Btn;
        [SerializeField] Button loadShow_Btn;
        [SerializeField] GameObject ConfigurationUI;
        [SerializeField] GameObject CallibrationUI;
        [SerializeField] TMP_InputField length_input;
        [SerializeField] TMP_InputField width_input;
        [SerializeField] TMP_InputField outIP;
        [SerializeField] Toggle host;
        float floor_level;

        #endregion
        TapToPlace tapToPlace;
        ARManager _arManager;
        [SerializeField]OSC_Adapter _oscAdpater;

        float room_Width, room_Length;

        public float Room_Width { get => room_Width; set => room_Width = value; }
        public float Room_Length { get => room_Length; set => room_Length = value; }

        void onCallibrateButtonInvoke()
        {
            ConfigurationUI.SetActive(false);
            CallibrationUI.SetActive(true);
            tapToPlace.Init();
        }
        void onSaveSpawnAnchor_Btn_Invoke()
        {
            // save spawn point
            CallibrationUI.SetActive(false);
            ConfigurationUI.SetActive(true);
            tapToPlace.SaveSpawnPosition();
        }
        void onLoadScene_Btn_Invoke()
        {
            ConfigurationUI.SetActive(false);

            if (host.isOn)
            {
                outIP.text = "192.168.178.34";
                _oscAdpater.InIt("192.168.178.34");
            }
            else
            {
                outIP.text = "127.0.0.1";
                _oscAdpater.InIt("127.0.0.1");
            }
            PlayerPrefs.SetString("outIP", outIP.text);

            tapToPlace.SaveSpawnPosition();
            _arManager.LoadShow();

        }
        private void Awake()
        {
            tapToPlace = GetComponent<TapToPlace>();
            
        }
        private void Start()
        {
            //PlayerPrefs.DeleteAll();
            _arManager = ARManager.Instance;
            FetchData();
            ConfigurationUI.SetActive(true);
                callibrate_Btn.onClick.AddListener(() => onCallibrateButtonInvoke());
                save_floorlevel_Btn.onClick.AddListener(() => onSaveSpawnAnchor_Btn_Invoke());
                saveCallibration_Btn.onClick.AddListener(() => onSave_Btn_Invoke());
                loadShow_Btn.onClick.AddListener(() => onLoadScene_Btn_Invoke());
        }
        void FetchData()
        {
            GetConfigurations();
            width_input.text = Room_Width.ToString();
            length_input.text = Room_Length.ToString();
        }
        void onSave_Btn_Invoke()
        {
            SaveSettings(float.Parse(width_input.text), float.Parse(length_input.text));// Replace with position
        }

        public void SaveSettings(float width, float length)
        {
            Room_Width = width; Room_Length = length;

            PlayerPrefs.SetFloat("room_Width", room_Width);
            PlayerPrefs.SetFloat("room_Length", room_Length);
        }
        public void GetConfigurations()
        {
            if (!PlayerPrefs.HasKey("room_Width"))
                SaveSettings(1f, 1f);

            Room_Width = PlayerPrefs.GetFloat("room_Width");
            Room_Length = PlayerPrefs.GetFloat("room_Length");
        }


    }

}