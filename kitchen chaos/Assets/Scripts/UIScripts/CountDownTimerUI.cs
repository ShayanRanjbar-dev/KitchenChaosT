using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class CountDownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownTxt;

    void Start()
    {
        GameHandler.Instance.OnCountDownChanged += GameHandler_OnCountDownChanged;
    }

    private void GameHandler_OnCountDownChanged(object sender, System.EventArgs e)
    {
        gameObject.SetActive(GameHandler.Instance.IsCountDown());
    }

    void Update()
    {
        countDownTxt.text = math.ceil(GameHandler.Instance.ReturnCountdownTime()).ToString();
    }
}
