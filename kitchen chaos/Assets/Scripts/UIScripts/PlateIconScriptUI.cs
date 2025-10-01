using UnityEngine;
using UnityEngine.UI;

public class PlateIconScriptUI : MonoBehaviour
{
    [SerializeField] private PlateScript plate;
    [SerializeField] private Transform iconTemplate;


    private void Start()
    {
        plate.OnPlateObjectAdd += Plate_OnPlateObjectAdd;
        iconTemplate.gameObject.SetActive(false);
    }

    private void Plate_OnPlateObjectAdd(object sender, PlateScript.OnPlateObjectAddEventHandler e)
    {
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectsSO kitchenObjects in plate.GetPlateObjectsSO())
        {
            Transform newIcon = Instantiate(iconTemplate, transform);
            newIcon.GetComponent<IconScript>().SetImageObject(kitchenObjects);
            newIcon.gameObject.SetActive(true);
        }
    }
}
