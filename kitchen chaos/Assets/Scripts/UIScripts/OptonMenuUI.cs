using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptonMenuUI : MonoBehaviour
{
    public static OptonMenuUI instance { get; private set; }

    public event EventHandler<float> OnMusicChanged;

    public event EventHandler<float> OnSoundEffectChanged;
    

    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private TextMeshProUGUI soundEffectSliderText;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        instance = this;
        backButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            PauseMenuUI.instance.gameObject.SetActive(true);
        });
        soundEffectSlider.onValueChanged.AddListener(SoundEffectSliderValueChanged);
        musicSlider.onValueChanged.AddListener(MusicSliderValueChanged);
    }
    private void Start()
    {
        soundEffectSlider.value = SoundManagerScript.instance.GetSoundEffectVolume();
        musicSlider.value = MusicManagerScirpt.instance.GetMusicVolume();
        gameObject.SetActive(false);
    }
    private void MusicSliderValueChanged(float value)
    {
        OnMusicChanged?.Invoke(this, value);
        musicSliderText.text = (100 * value).ToString("0");
    }
    private void SoundEffectSliderValueChanged(float value)
    {
        OnSoundEffectChanged?.Invoke(this, value);
        soundEffectSliderText.text = (100 * value).ToString("0");
    }
}
