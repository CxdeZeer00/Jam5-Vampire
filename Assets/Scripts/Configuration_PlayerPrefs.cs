using UnityEngine;
using UnityEngine.UI;

public class Configuration_PlayerPrefs : MonoBehaviour //Zoe García
{
    #region ///Sensivity///
    public Slider sliderSensivity;
    private string keySensivity = "SensivityCamera";
    #endregion
    #region ///Volume///
    public Slider sliderVolume;
    private string keyVolume = "MasterVolume";
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey(keySensivity))
        {
            sliderSensivity.value= PlayerPrefs.GetFloat(keySensivity); //q vea cual es el % del slider y lo convierta a la sensibilidad
        }
        else
        {
            sliderSensivity.value = 100f; //deja la sensibilidad por defecto q pusimos al principio si no lo modificas
        }
        sliderSensivity.onValueChanged.AddListener(SaveSensivity); //lo guarda cada q lo cambias.

        if(PlayerPrefs.HasKey(keyVolume))
        {
            float volumePlayer = PlayerPrefs.GetFloat(keyVolume);
            sliderVolume.value = volumePlayer;
            AudioListener.volume = volumePlayer;
        }
        else
        {
            sliderVolume.value = 1f;
            AudioListener.volume = 1f;
        }
        sliderVolume.onValueChanged.AddListener(SaveVolume);
    }

    public void SaveSensivity(float sliderValue)
    {
        PlayerPrefs.SetFloat(keySensivity, sliderValue); //		Ej: PlayerPrefs.Set"string"(“nombreJugador(lo q declaras)”, “Danielito”value);
        PlayerPrefs.Save();

        Camera_Movement scriptCamara = FindFirstObjectByType<Camera_Movement>(); //vale el error inicial era porq en el script de Po! puse q mirase los valores del slider en el start, osea q habia q configurarlo ANTES de entrar en la escena. esto es para poder configurarlo en el pause
        if (scriptCamara != null)
        {
            scriptCamara.Speed = sliderValue;
        }
    }

    public void SaveVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat (keyVolume, sliderValue);
        PlayerPrefs.Save();

        AudioListener.volume = sliderValue;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
