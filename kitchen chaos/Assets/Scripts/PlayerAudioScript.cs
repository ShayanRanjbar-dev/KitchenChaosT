using System;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public static void OnGameReset()
    {
        OnPlayerMove = null;
    }
    public static event EventHandler OnPlayerMove;
    private float soundPlayTime = 0.1f;
    void Update()
    {
        if (PlayerScript.Instance.Iswalking())
        {
            soundPlayTime -= Time.deltaTime;
            if (soundPlayTime <= 0)
            {
                OnPlayerMove?.Invoke(this, EventArgs.Empty);
                soundPlayTime = 0.1f;
            }
        }
    }
}
