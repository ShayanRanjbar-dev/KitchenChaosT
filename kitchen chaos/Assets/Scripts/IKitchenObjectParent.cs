using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetSpawnPos();
    public void ClearKitchenObject();
    public bool HasKithcenObject();
    public KitchenObject GetKithcenObject();
    public void SetKitchenObjects(KitchenObject kitchenObject);
}
