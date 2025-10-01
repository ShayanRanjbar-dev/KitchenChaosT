using UnityEngine;

public class GameDataResetScript : MonoBehaviour
{
    private void Awake()
    {
        BaseCounter.OnGameReset();
        TrashCounterScript.OnGameReset();
        CuttingCounterScript.OnGameReset();
        PlayerAudioScript.OnGameReset();

    }
}
