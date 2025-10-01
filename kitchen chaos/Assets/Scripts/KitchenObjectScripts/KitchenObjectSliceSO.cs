using UnityEngine;

[CreateAssetMenu]
public class KitchenObjectSliceSO : ScriptableObject
{
    public KitchenObjectsSO kitchenObjectsInput;
    public KitchenObjectsSO kitchenObjectsOutput;
    public int cuttingMaxProgress;
}
