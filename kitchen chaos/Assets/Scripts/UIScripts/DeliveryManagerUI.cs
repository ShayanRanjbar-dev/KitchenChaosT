using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform deliveryRecipeTemplate;


    private void Start()
    {
        DeliveryManegerScript.Instance.OnDeliveryAdd += Instance_OnDeliveryAdd;
        DeliveryManegerScript.Instance.OnDeliveryRemove += Instance_OnDeliveryRemove;
        deliveryRecipeTemplate.gameObject.SetActive(false);
    }

    private void Instance_OnDeliveryRemove(object sender, DeliveryManegerScript.OnDeliveryRemoveEventHandler e)
    {
        RemoveDelivery(e.deliveryRequestList);
    }

    private void Instance_OnDeliveryAdd(object sender, System.EventArgs e)
    {
        AddNewDelivery();
    }

    private void AddNewDelivery()
    {
        foreach (Transform child in transform)
        {
            if (child == deliveryRecipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipesSO recipe in DeliveryManegerScript.Instance.GetDeliveryList())
        {
            Transform newDeliveryTemplate = Instantiate(deliveryRecipeTemplate, transform);
            newDeliveryTemplate.gameObject.SetActive(true);
            newDeliveryTemplate.GetComponent<DeliveryRecipeTUI>().SetImgName(recipe);
        }
    }
    private void RemoveDelivery(RecipesSO recipe)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<DeliveryRecipeTUI>().RemoveUI(recipe))
            {
                break;
            }
        }
    }
}
