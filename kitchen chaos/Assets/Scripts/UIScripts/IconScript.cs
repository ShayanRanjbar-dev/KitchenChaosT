using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetImageObject(KitchenObjectsSO kitchenObjects)
    {
        image.sprite = kitchenObjects.sprite;
    }
}
