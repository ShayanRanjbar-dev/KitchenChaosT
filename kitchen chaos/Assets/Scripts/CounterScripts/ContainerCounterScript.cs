using System;
using UnityEngine;

public class ContainerCounterScript : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public event EventHandler OnPlayerGrab;
    public override void Interact(PlayerScript player)
    {
        if (!player.HasKithcenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);
            OnPlayerGrab?.Invoke(this, EventArgs.Empty);
        }
    }
}
