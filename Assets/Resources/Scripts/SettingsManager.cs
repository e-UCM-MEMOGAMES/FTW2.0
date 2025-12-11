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
    /// <summary>
    /// Lista de idiomas disponibles
    /// </summary>
    List<Locale> lcs;
    /// <summary>
    /// El dropdown de idiomas
    /// </summary>
    [SerializeField]
    TMP_Dropdown dropdown;
    /// <summary>
    /// Mixer para controlar volumenes
    /// </summary>
    [SerializeField]
    AudioMixer mixer;
    /// <summary>
    /// Slider para controlar el volumen la musica
    /// </summary>
    [SerializeField]
    Slider musicSlider;
    /// <summary>
    /// Slider para controlar el volumen de los sonidos
    /// </summary>
    [SerializeField]
    Slider soundSlider;
    /// <summary>
    /// Sonido que se reproduce al mover los sliders
    /// </summary>
    [SerializeField]
    AudioSource sound;

    /// <summary>
    /// Indica si se ejecuta por primera vez
    /// </summary>
    bool first = true;
    void Awake()
    {
        lcs = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < lcs.Count; ++i)
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = lcs[i].LocaleName });
        

        int lid = lcs.IndexOf(LocalizationSettings.SelectedLocale);
        dropdown.value = lid;
        PlayerPrefs.SetInt("language", lid);

        if (PlayerPrefs.HasKey("musicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        if (PlayerPrefs.HasKey("soundVolume"))
            soundSlider.value = PlayerPrefs.GetFloat("soundVolume");

        ChangeMusicVolume();
        ChangeSoundVolume();

        first = false;
    }

    /// <summary>
    /// metodo que se llama cuando se selecciona un idioma en el dropdown
    /// </summary> 
    public void OnDropDownChanged(TMP_Dropdown dropDown)
    {
        LocalizationSettings.SelectedLocale = lcs[dropDown.value];
        PlayerPrefs.SetInt("language", dropDown.value);
    }


    /// <summary>
    /// cambia el volumen de la musica
    /// </summary> 
    public void ChangeMusicVolume()
    {
        if(!first) sound.Play();
        
        mixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
    /// <summary>
    /// cambia el volumen de los sonidos
    /// </summary> 
    public void ChangeSoundVolume()
    {
        if (!first) sound.Play();

        mixer.SetFloat("SFX", Mathf.Log10(soundSlider.value) * 20);
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
    }
}
