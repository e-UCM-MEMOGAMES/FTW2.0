using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class InitGame : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("language")];
        if (PlayerPrefs.HasKey("musicVolume"))
            mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        if (PlayerPrefs.HasKey("soundVolume"))
            mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
    }
}
