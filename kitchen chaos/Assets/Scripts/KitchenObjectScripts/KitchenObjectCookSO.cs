using UnityEngine;

[CreateAssetMenu]
public class KitchenObjectCookSO : ScriptableObject
{
    public KitchenObjectsSO kitchenObjectsInput;
    public KitchenObjectsSO kitchenObjectsOutput;
    public int cookingMaxProgress;
}
