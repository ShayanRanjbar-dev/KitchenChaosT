using UnityEngine;
using UnityEngine.Rendering;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance { get; private set; }
    [SerializeField] private SoundEffectSO soundEffectlist;

    private const string Sound_Effect_Volume_Prefs = "SoundEffects";
    private float volume = 1.0f;

    private void Awake()
    {
        instance = this;
        volume = PlayerPrefs.GetFloat(Sound_Effect_Volume_Prefs, 1f);
    }
    private void Start()
    {
        OptonMenuUI.instance.OnSoundEffectChanged += OptionMenu_OnSoundEffectChanged;
        PlayerScript.Instance.OnObjectPickup += Player_OnObjectPickup;
        DeliveryManegerScript.Instance.OnDeliveryFail += Delivery_OnDeliveryFail;
        DeliveryManegerScript.Instance.OnDeliverySuccess += Delivery_OnDeliverySuccess;
        CuttingCounterScript.OnPlayerCutSound += CuttingCounter_OnPlayerCutSound;
        BaseCounter.OnObjectDrop += BaseCounter_OnObjectDrop;
        TrashCounterScript.OnTrashDrop += TrashCounter_OnTrashDrop;
        PlayerAudioScript.OnPlayerMove += PlayerAudioScript_OnPlayerMove;

    }

    private void OptionMenu_OnSoundEffectChanged(object sender, float e)
    {
        volume = e;
        PlayerPrefs.SetFloat(Sound_Effect_Volume_Prefs, volume);
        PlayerPrefs.Save();
    }

    private void PlayerAudioScript_OnPlayerMove(object sender, System.EventArgs e)
    {
        PlayAudioClip(soundEffectlist.footStepSounds, PlayerScript.Instance.transform.position);
    }

    private void TrashCounter_OnTrashDrop(object sender, System.EventArgs e)
    {
        TrashCounterScript trashCounter = sender as TrashCounterScript;
        PlayAudioClip(soundEffectlist.trashSounds, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectDrop(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlayAudioClip(soundEffectlist.objectDropSounds, baseCounter.transform.position);
    }

    private void CuttingCounter_OnPlayerCutSound(object sender, System.EventArgs e)
    {
        CuttingCounterScript cuttingCounter = sender as CuttingCounterScript;
        PlayAudioClip(soundEffectlist.chopsSounds,cuttingCounter.transform.position);
    }

    private void Delivery_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounterScript deliveryCounter = DeliveryCounterScript.Instance;
        PlayAudioClip(soundEffectlist.deliverySuccessSounds, deliveryCounter.transform.position);
    }

    private void Delivery_OnDeliveryFail(object sender, System.EventArgs e)
    {
        DeliveryCounterScript deliveryCounter = DeliveryCounterScript.Instance;
        PlayAudioClip(soundEffectlist.deliveryFailedSounds, deliveryCounter.transform.position);
    }

    private void Player_OnObjectPickup(object sender, System.EventArgs e)
    {
        PlayerScript player = sender as PlayerScript;
        PlayAudioClip(soundEffectlist.objectPickupSounds,player.transform.position);
    }
    private void PlayAudioClip(AudioClip[] audio, Vector3 position, float volumeMultiplier = 1f)
    {
        PlayAudioClip(audio[Random.Range(0, audio.Length)], position, volumeMultiplier);
    }
    private void PlayAudioClip(AudioClip audio, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audio, position, volumeMultiplier);
    }
    public float GetSoundEffectVolume()
    {
        return volume;
    }
}
