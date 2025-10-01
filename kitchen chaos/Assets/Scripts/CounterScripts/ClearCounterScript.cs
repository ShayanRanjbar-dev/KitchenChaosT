using UnityEngine;

public class ClearCounterScript : BaseCounter
{
    public override void Interact(PlayerScript player)
    {
        if (!HasKithcenObject())
        {
            if (player.HasKithcenObject())
            {
                player.GetKithcenObject().SetKithcenObjectParent(this);
            }
            else
            {
            }
        }

        else
        {
            if (!player.HasKithcenObject())
                GetKithcenObject().SetKithcenObjectParent(player);
            else
            {
                if (player.GetKithcenObject().CheckIsPlate(out PlateScript plate))
                {
                    if (plate.TryGetObjects(GetKithcenObject()))
                    {
                        GetKithcenObject().DestoryKitchenObject();
                    }
                }
                else if (GetKithcenObject().CheckIsPlate(out plate))
                {
                    if (plate.TryGetObjects(player.GetKithcenObject()))
                    {
                        player.GetKithcenObject().DestoryKitchenObject();
                    }
                }
                
            }
        }


    }
}

