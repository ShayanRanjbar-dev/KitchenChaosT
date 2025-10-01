using UnityEngine;
using UnityEngine.UI;

public class PlayTimerUI : MonoBehaviour
{
    [SerializeField] private Image FillColor;



    private void Update()
    {
        FillColor.fillAmount = GameHandler.Instance.ReturnPlayTime();
    }
}
