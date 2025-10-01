using UnityEngine;

public class DeliveryCounterScript : BaseCounter
{
    public static DeliveryCounterScript Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(PlayerScript player)
    {
        if (player.HasKithcenObject())
        {
            if (player.GetKithcenObject().CheckIsPlate(out PlateScript plate))
            {
                DeliveryManegerScript.Instance.DeliveringRequest(plate);
                player.GetKithcenObject().DestoryKitchenObject();
            }
        }
    }
}
