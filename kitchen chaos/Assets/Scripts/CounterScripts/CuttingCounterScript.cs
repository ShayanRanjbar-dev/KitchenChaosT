using System;
using UnityEngine;

public class CuttingCounterScript : BaseCounter,IProgressBarParent
{
    new public static void OnGameReset()
    {
        OnPlayerCutSound = null;
    }
    public static event EventHandler OnPlayerCutSound;
    [SerializeField] private KitchenObjectSliceSO[] kitchenObjectRecipes;
    private KitchenObjectsSO KitchenObjectsOutput;
    private int cutProgress;
    public event EventHandler OnPlayerCut;

    public event EventHandler <IProgressBarParent.OnProgressBarValueChangedEventArgs> OnProgressBarValueChanged;

    public class OnProgressBarValueChangedEventArgs : EventArgs
    {
        public float progress;
    }
    public override void Interact(PlayerScript player)
    {
        if (!HasKithcenObject())
        {
            if (player.HasKithcenObject() && CanSliceObject(player.GetKithcenObject()))
            {
                player.GetKithcenObject().SetKithcenObjectParent(this);
                cutProgress = 0;
                KitchenObjectSliceSO kitchenObjectprogres = GetKitchenRecipe(GetKithcenObject());
                OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
                {
                    progress = (float)cutProgress / kitchenObjectprogres.cuttingMaxProgress
                });
            }
        }

        else
        {
            if (!player.HasKithcenObject())
            {
                GetKithcenObject().SetKithcenObjectParent(player);
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
                        OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
                        {
                            progress = 0f
                        });
                        GetKithcenObject().DestoryKitchenObject();
                    }
                }
            }
        }


    }
    public override void AlternativeInteract(PlayerScript player)
    {
        if (HasKithcenObject() && CanSliceObject(GetKithcenObject()))
        {
            OnPlayerCutSound?.Invoke(this,EventArgs.Empty);
            OnPlayerCut?.Invoke(this, EventArgs.Empty);
            cutProgress++;
            KitchenObjectSliceSO kitchenObjectprogres = GetKitchenRecipe(GetKithcenObject());
            OnProgressBarValueChanged?.Invoke(this, new IProgressBarParent.OnProgressBarValueChangedEventArgs
            {
                progress = (float)cutProgress / kitchenObjectprogres.cuttingMaxProgress
            });
            if (kitchenObjectprogres.cuttingMaxProgress <= cutProgress)
            {
                KitchenObjectsOutput = HasKitchenRecipe(GetKithcenObject());
                GetKithcenObject().DestoryKitchenObject();
                KitchenObject.SpawnKitchenObject(KitchenObjectsOutput, this);
            }
        }
    }
    private bool CanSliceObject(KitchenObject kitchenObject)
    {
        KitchenObjectSliceSO kitchenRecipe = GetKitchenRecipe(kitchenObject);
        return kitchenRecipe != null;
    }
    private KitchenObjectsSO HasKitchenRecipe(KitchenObject kitchenObject)
    {
        KitchenObjectSliceSO kitchenRecipe = GetKitchenRecipe(kitchenObject);
        if (kitchenRecipe != null)
        {
            return kitchenRecipe.kitchenObjectsOutput;
        }
        else {
            return null;
        }
    }
    private KitchenObjectSliceSO GetKitchenRecipe(KitchenObject kitchenObject)
    {
        foreach (KitchenObjectSliceSO kitchenRecipe in kitchenObjectRecipes)
        {
            if (kitchenObject.GetKitchenObjectsSO() == kitchenRecipe.kitchenObjectsInput)
            {
                return kitchenRecipe;
            }
        }
        return null;
    }
}
