using System;
using UnityEngine;

public class PlateCounterScript : BaseCounter
{
    [SerializeField] private KitchenObjectsSO plateObject;
    public event EventHandler OnPlateAdd;
    public event EventHandler OnPlateRemove;
    private int plateCount;
    private int plateCountMax = 4 ;
    private float spawnTime;
    private float spawnTimeMax = 5f;

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnTimeMax)
        {
            if (plateCount < plateCountMax) 
            {
                plateCount++;
                OnPlateAdd?.Invoke(this, EventArgs.Empty);
            }
            spawnTime = 0f;
        }
    }
    public override void Interact(PlayerScript player)
    {
        if (!player.HasKithcenObject())
        {
            if (plateCount > 0)
            {
                plateCount--;
                KitchenObject.SpawnKitchenObject(plateObject, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
