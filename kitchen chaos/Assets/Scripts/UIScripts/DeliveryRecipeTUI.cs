using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryRecipeTUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconHolder;
    [SerializeField] Image recipeImage;

    private void Start()
    {
        recipeImage.gameObject.SetActive(false);
    }

    public void SetImgName(RecipesSO recipe)
    {
        recipeName.text = recipe.orderName;
        foreach (KitchenObjectsSO kitchenObjects in recipe.ingridients)
        {
            Image img = Instantiate(recipeImage, iconHolder);
            img.sprite = kitchenObjects.sprite;
            img.gameObject.SetActive(true);
        }
    }
    public bool RemoveUI(RecipesSO recipe)
    {
        if (recipe.orderName == recipeName.text) {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
