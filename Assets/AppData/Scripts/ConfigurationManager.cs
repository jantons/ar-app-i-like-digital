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
        [SerializeField] Button up_Btn;
        [SerializeField] Button down_Btn;
        [SerializeField] Button saveCallibration_Btn;
        [SerializeField] GameObject ConfigurationUI;
        [SerializeField] GameObject CallibrationUI;
        [SerializeField] GameObject floor_marker;
        [SerializeField] TMP_InputField length_input;
        [SerializeField] TMP_InputField width_input;
        float floor_level;

        #endregion

        float room_Width, room_Length, room_floor;

        public float Room_Width { get => room_Width; set => room_Width = value; }
        public float Room_Length { get => room_Length; set => room_Length = value; }
        public float Room_floor { get => room_floor; set => room_floor = value; }

        void onCallibrateButtonInvoke()
        {
            ConfigurationUI.SetActive(false);
            CallibrationUI.SetActive(true);
            floor_marker.SetActive(true);
        }
        void onSaveFloorLevel_Btn_Invoke()
        {
            floor_level = floor_marker.transform.position.y;
            floor_marker.SetActive(false);
            CallibrationUI.SetActive(false);
            ConfigurationUI.SetActive(true);
        }

        private void Start()
        {
            //PlayerPrefs.DeleteAll();
            FetchData();
            ConfigurationUI.SetActive(true);
                callibrate_Btn.onClick.AddListener(() => onCallibrateButtonInvoke());
                save_floorlevel_Btn.onClick.AddListener(() => onSaveFloorLevel_Btn_Invoke());
                saveCallibration_Btn.onClick.AddListener(() => onSave_Btn_Invoke());
                up_Btn.onClick.AddListener(() => OnUp_Btn_Invoke());
                down_Btn.onClick.AddListener(() => OnDown_Btn_Invoke());
        }
        void OnUp_Btn_Invoke()
        {
            floor_marker.transform.position = floor_marker.transform.position + new Vector3(0,0.1f,0);
        }
        void OnDown_Btn_Invoke()
        {
            floor_marker.transform.position = floor_marker.transform.position + new Vector3(0, -0.1f, 0);
        }
        void FetchData()
        {
            GetConfigurations();
            width_input.text = Room_Width.ToString();
            length_input.text = Room_Length.ToString();
            floor_marker.transform.position = new Vector3(floor_marker.transform.position.x, Room_floor, floor_marker.transform.position.z);
        }
        void onSave_Btn_Invoke()
        {
            SaveSettings(float.Parse(width_input.text), float.Parse(length_input.text), floor_marker.transform.position.y);
        }

        public void SaveSettings(float width, float length,float floor)
        {
            Room_Width = width; Room_Length = length; Room_floor = floor;

            PlayerPrefs.SetFloat("room_Width", room_Width);
            PlayerPrefs.SetFloat("room_Length", room_Length);
            PlayerPrefs.SetFloat("room_floor", room_floor);
        }
        public void GetConfigurations()
        {
            if (!PlayerPrefs.HasKey("room_Width"))
                SaveSettings(1f, 1f, -.5f);

            Room_Width = PlayerPrefs.GetFloat("room_Width");
            Room_Length = PlayerPrefs.GetFloat("room_Length");
            Room_floor = PlayerPrefs.GetFloat("room_floor");
        }
        public void LoadScene(string name) { SceneManager.LoadScene(name); }

    }

}