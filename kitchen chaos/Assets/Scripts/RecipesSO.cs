using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipesSO : ScriptableObject
{
    public List<KitchenObjectsSO> ingridients;
    public string orderName;
}
