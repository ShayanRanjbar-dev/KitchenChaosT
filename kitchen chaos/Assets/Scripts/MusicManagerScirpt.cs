using UnityEngine;
using UnityEngine.Rendering;

public class MusicManagerScirpt : MonoBehaviour
{
    public static MusicManagerScirpt instance { get; private set; }

    private AudioSource musicAudioSource;
    private const string Music_Volume_Prefs = "Musics";
    private float musicVolume = 1.0f;


    private void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
        instance = this;
        musicVolume = PlayerPrefs.GetFloat(Music_Volume_Prefs,1f);
        musicAudioSource.volume = musicVolume;
    }

    private void Start()
    {
        OptonMenuUI.instance.OnMusicChanged += OptionMenu_OnMusicChanged;
    }

    private void OptionMenu_OnMusicChanged(object sender, float e)
    {
        musicVolume = e;
        musicAudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat(Music_Volume_Prefs, musicVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
