using UnityEngine;

public class BaseCounterVisualScript : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] counterVisuals;

    private void Start()
    {
        PlayerScript.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerScript.OnSelectedCounterChangedEventArgs e)
    {
        if (baseCounter == e.selectedCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }
    private void Show()
    {
        foreach (GameObject visuals in counterVisuals)
        {

            visuals.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visuals in counterVisuals)
        {

            visuals.SetActive(false);
        }
    }
}
