using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static void OnGameReset()
    {
        OnObjectDrop = null;
    }
    public static event EventHandler OnObjectDrop;
    [SerializeField] private Transform spawnPosition;
    private KitchenObject kitchenObject;
    public virtual void AlternativeInteract(PlayerScript player) { }
    public virtual void Interact(PlayerScript player) { }
    public Transform GetSpawnPos()
    {
        return spawnPosition;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKithcenObject()
    {
        return kitchenObject != null;
    }
    public KitchenObject GetKithcenObject()
    {
        return kitchenObject;
    }
    public void SetKitchenObjects(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (this.kitchenObject != null)
        {
            OnObjectDrop?.Invoke(this, EventArgs.Empty);
        }
    }
}
