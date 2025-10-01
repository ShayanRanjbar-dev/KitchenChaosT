using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateScript : KitchenObject
{
    public event EventHandler<OnPlateObjectAddEventHandler> OnPlateObjectAdd;
    public class OnPlateObjectAddEventHandler : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }
    [SerializeField] private KitchenObjectsSO[] validObjects;
    private List<KitchenObjectsSO> plateObjects;

    private void Awake()
    {
        plateObjects = new List<KitchenObjectsSO>();
    }

    public bool TryGetObjects(KitchenObject kitchenObject)
    {
        if (validObjects.Contains(kitchenObject.GetKitchenObjectsSO()))
        {
            if (!plateObjects.Contains(kitchenObject.GetKitchenObjectsSO()))
            {
                plateObjects.Add(kitchenObject.GetKitchenObjectsSO());
                OnPlateObjectAdd?.Invoke(this,new OnPlateObjectAddEventHandler{
                    kitchenObjectsSO = kitchenObject.GetKitchenObjectsSO() } );
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }
    public List<KitchenObjectsSO> GetPlateObjectsSO()
    {
        return plateObjects;
    }
}
