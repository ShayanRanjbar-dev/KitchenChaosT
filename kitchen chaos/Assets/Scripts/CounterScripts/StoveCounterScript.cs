using System;
using UnityEngine;

public class StoveCounterScript : BaseCounter,IProgressBarParent
{
    [SerializeField] private KitchenObjectCookSO[] kitchenObjectCooks;

    public event EventHandler<OnStoveObjectEventHandler> OnStoveObject;
    public class OnStoveObjectEventHandler : EventArgs{
        public States state;
    }

    public event EventHandler<IProgressBarParent.OnProgressBarValueChangedEventArgs> OnProgressBarValueChanged;

    private float cookTime;
    private float burnTime;
    private KitchenObjectCookSO kitchenObjectCook;
    public enum States
    {
        idle,
        cooking,
        cooked,
        bunrt
    }
    private States state;

    private void Start()
    {
        state = States.idle;
    }
    private void Update()
    {
        switch (state)
        {
            case States.idle:
                OnStoveObject?.Invoke(this, new OnStoveObjectEventHandler
                {
                    state = state
                });
                cookTime = 0f;
                burnTime = 0f;
                break;
            case States.cooking:
                cookTime += Time.deltaTime;
                OnStoveObject?.Invoke(this, new OnStoveObjectEventHandler
                {
                    state = state
                });
                OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
                {
                    progress = cookTime / kitchenObjectCook.cookingMaxProgress
                });
                if (cookTime >= kitchenObjectCook.cookingMaxProgress)
                {
                    GetKithcenObject().DestoryKitchenObject();
                    KitchenObject.SpawnKitchenObject(kitchenObjectCook.kitchenObjectsOutput, this);
                    kitchenObjectCook = GetKitchenCook(GetKithcenObject());
                    state = States.cooked;
                }
                break;
            case States.cooked:
                OnStoveObject?.Invoke(this, new OnStoveObjectEventHandler
                {
                    state = state
                });
                burnTime += Time.deltaTime;
                OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
                {
                    progress = burnTime / kitchenObjectCook.cookingMaxProgress
                });
                if (burnTime >= kitchenObjectCook.cookingMaxProgress)
                {
                    GetKithcenObject().DestoryKitchenObject();
                    KitchenObject.SpawnKitchenObject(kitchenObjectCook.kitchenObjectsOutput, this);
                    kitchenObjectCook = GetKitchenCook(GetKithcenObject());
                    state = States.bunrt;
                }
                break;
            case States.bunrt:
                OnStoveObject?.Invoke(this, new OnStoveObjectEventHandler
                {
                    state = state
                });
                break;
        }
    }


    public override void Interact(PlayerScript player)
    {
        if (!HasKithcenObject())
        {
            if (player.HasKithcenObject() && CanCookObject(player.GetKithcenObject()))
            {
                kitchenObjectCook = GetKitchenCook(player.GetKithcenObject());
                player.GetKithcenObject().SetKithcenObjectParent(this);
                state = States.cooking;
            }
        }
        else
        {
            if (!player.HasKithcenObject())
            {
                GetKithcenObject().SetKithcenObjectParent(player);
                state = States.idle;
                OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
                {
                    progress = 0f
                });
            }
            else 
            {
                if (player.GetKithcenObject().CheckIsPlate(out PlateScript plate))
                {
                    if (plate.TryGetObjects(GetKithcenObject()))
                    {
                        GetKithcenObject().DestoryKitchenObject();
                        state = States.idle;
                        OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
                        {
                            progress = 0f
                        });
                    }
                }
            }
        }
    }
    private bool CanCookObject(KitchenObject kitchenObject)
    {
        KitchenObjectCookSO kitchenRecipe = GetKitchenCook(kitchenObject);
        return kitchenRecipe != null;
    }
    private KitchenObjectsSO HasKitchenCook(KitchenObject kitchenObject)
    {
        KitchenObjectCookSO kitchenCook = GetKitchenCook(kitchenObject);
        if (kitchenCook != null)
        {
            return kitchenCook.kitchenObjectsOutput;
        }
        else
        {
            return null;
        }
    }
    private KitchenObjectCookSO GetKitchenCook(KitchenObject kitchenObject)
    {
        foreach (KitchenObjectCookSO kitchenCook in kitchenObjectCooks)
        {
            if (kitchenObject.GetKitchenObjectsSO() == kitchenCook.kitchenObjectsInput)
            {
                return kitchenCook;
            }
        }
        return null;
    }
}
