using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManegerScript : MonoBehaviour
{
    public event EventHandler OnDeliveryAdd;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;
    public event EventHandler<OnDeliveryRemoveEventHandler> OnDeliveryRemove;
    public class OnDeliveryRemoveEventHandler : EventArgs {
        public RecipesSO deliveryRequestList;
    }
    public static DeliveryManegerScript Instance { get; private set; }

    [SerializeField] private DeliveryRescipeSO deliveryRescipesList;

    private List<RecipesSO> deliveryRequestList;
    private float deliveryRequestTime;
    private float deliveryRequestTimeMax = 4f;
    private int deliveryRequestCount;
    private int DeliverySuccess;

    private void Awake()
    {
        deliveryRequestList = new List<RecipesSO>();
        Instance = this;
    }
    private void Update()
    {
        if (deliveryRequestCount < deliveryRescipesList.deliveryRecipes.Count)
        {
            deliveryRequestTime += Time.deltaTime;
            if (deliveryRequestTime >= deliveryRequestTimeMax)
            {
                deliveryRequestList.Add(deliveryRescipesList.deliveryRecipes[UnityEngine.Random.Range(0, deliveryRescipesList.deliveryRecipes.Count())]);
                deliveryRequestTime = 0;
                deliveryRequestCount++;
                OnDeliveryAdd?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public void DeliveringRequest(PlateScript plate)
    {
        for (int i = 0; i < deliveryRequestList.Count; i++)
        {
            RecipesSO waitingDelivery = deliveryRequestList[i];
            if (waitingDelivery.ingridients.Count == plate.GetPlateObjectsSO().Count)
            {
                bool deliveryMatchPlate = true;
                foreach (KitchenObjectsSO ingridient in waitingDelivery.ingridients)
                {
                    bool hasIngridients = false;
                    foreach (KitchenObjectsSO plateObject in plate.GetPlateObjectsSO())
                    {
                        if (plateObject == ingridient)
                        {
                            hasIngridients = true;
                            break;
                        }
                        
                    }
                    if (!hasIngridients)
                    {
                        deliveryMatchPlate = false;
                    }
                }
                if (deliveryMatchPlate)
                {
                    OnDeliverySuccess?.Invoke(this,EventArgs.Empty);
                    OnDeliveryRemove?.Invoke(this,new OnDeliveryRemoveEventHandler
                    {
                        deliveryRequestList = deliveryRequestList[i]
                    });
                    deliveryRequestList.RemoveAt(i);
                    deliveryRequestCount--;
                    DeliverySuccess++;
                    return;
                }
            }
            
        }
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);
    }
    public List<RecipesSO> GetDeliveryList()
    {
        return deliveryRequestList;
    }
    public int ReturnDeliverySuccess()
    {
        return DeliverySuccess;
    }
}
