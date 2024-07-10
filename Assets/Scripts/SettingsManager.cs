using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    List<Locale> lcs;
    [SerializeField]
    TMP_Dropdown dropdown;
    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider soundSlider;
    void Start()
    {
        lcs = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < lcs.Count; ++i)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = lcs[i].LocaleName });
        }

        Debug.Log(lcs.IndexOf(LocalizationSettings.SelectedLocale));

        dropdown.value = -1;
        dropdown.value = lcs.IndexOf(LocalizationSettings.SelectedLocale);

        if (PlayerPrefs.HasKey("musicVolume"))
            Load();
        
        ChangeMusicVolume();
        ChangeSoundVolume();        
    }

    //metodo que se llama cuando se selecciona un idioma en el dropdown
    public void OnDropDownChanged(TMP_Dropdown dropDown)
    {
        LocalizationSettings.SelectedLocale = lcs[dropDown.value];
    }

    //cambia el volumen de la musica
    public void ChangeMusicVolume()
    {
        mixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    //cambia el volumen de los sonidos
    public void ChangeSoundVolume()
    {
        mixer.SetFloat("SFX", Mathf.Log10(soundSlider.value) * 20);
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
    }

    //carga de 
    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
}
