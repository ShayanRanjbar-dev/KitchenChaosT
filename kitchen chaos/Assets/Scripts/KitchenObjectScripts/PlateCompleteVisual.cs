using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [System.Serializable]
    private struct PlateVisualComplete
    {
        public GameObject completeVisual;
        public KitchenObjectsSO kitchenObjects;
    }
    [SerializeField] private PlateScript plate;
    [SerializeField] private List<PlateVisualComplete> plateVisualComplete;

    private void Start()
    {
        plate.OnPlateObjectAdd += Plate_OnPlateObjectAdd;
        foreach (PlateVisualComplete plateStruct in plateVisualComplete)
        {
            plateStruct.completeVisual.SetActive(false);
        }
    }

    private void Plate_OnPlateObjectAdd(object sender, PlateScript.OnPlateObjectAddEventHandler e)
    {
        foreach (PlateVisualComplete plateStruct in plateVisualComplete)
        {
            if (plateStruct.kitchenObjects == e.kitchenObjectsSO)
            {
                plateStruct.completeVisual.SetActive(true);
            }
        }
    }
}
