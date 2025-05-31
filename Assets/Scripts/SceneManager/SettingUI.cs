using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] public GameObject settingPanel;
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public AudioMixer masterMixer;
    private const string VolumePrefKey = "MasterVolume";

    void Start()
    {
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey);
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
        else
        {
            volumeSlider.value = 1f;
            SetVolume(1f);
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }
    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingPanel.SetActive(false);
    }
    public void SetVolume(float volume)
    {
        float dbVolume = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        masterMixer.SetFloat("MasterVolume", dbVolume);

        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }
}
