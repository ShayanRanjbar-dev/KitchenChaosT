using System;
using UnityEngine;

public class TrashCounterScript : BaseCounter
{
    new public static void OnGameReset()
    {
        OnTrashDrop = null;
    }
    public static event EventHandler OnTrashDrop;
    public override void Interact(PlayerScript player)
    {
        if (player.HasKithcenObject())
        {
            OnTrashDrop?.Invoke(this, EventArgs.Empty);
            player.GetKithcenObject().DestoryKitchenObject();
        }
    }
}
