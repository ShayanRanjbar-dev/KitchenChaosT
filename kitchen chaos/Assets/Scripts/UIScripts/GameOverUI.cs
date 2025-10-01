using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveryText;
    void Start()
    {
        gameObject.SetActive(false);
        GameHandler.Instance.OnGameOverEnter += GameHandler_OnGameOverEnter;
    }

    private void GameHandler_OnGameOverEnter(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        deliveryText.text = "You Deliverd " + DeliveryManegerScript.Instance.ReturnDeliverySuccess().ToString() + " Recipes";
    }

}
