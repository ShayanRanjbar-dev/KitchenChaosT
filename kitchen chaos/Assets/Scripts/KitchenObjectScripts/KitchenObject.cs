using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectsSO kitchenObjectsSO;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectsSO GetKitchenObjectsSO() { return kitchenObjectsSO; }
    public KitchenObject GetKitchenObjects(){return this;}

    public void SetKithcenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        transform.parent = kitchenObjectParent.GetSpawnPos();
        transform.localPosition = Vector3.zero;
        kitchenObjectParent.SetKitchenObjects(this);
    }

    public void DestoryKitchenObject()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool CheckIsPlate(out PlateScript plate) {
        if (this is PlateScript)
        {
            plate = this as PlateScript;
            return true;
        }
        else
        {
            plate = null;
            return false;
        }

    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObject, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObject.prefabObject);
        KitchenObject kitchenObject1 = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject1.SetKithcenObjectParent(kitchenObjectParent);
        return kitchenObject1;
    }
}
