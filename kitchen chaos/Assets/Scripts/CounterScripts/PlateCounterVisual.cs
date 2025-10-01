using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounterScript plateCounter;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform plateVisual;
    private List<GameObject> gameObjects;

    private void Awake()
    {
        gameObjects = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateAdd += PlateCounter_OnPlateAdd;
        plateCounter.OnPlateRemove += PlateCounter_OnPlateRemove;
    }

    private void PlateCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject gameObject = gameObjects.Last();
        gameObjects.Remove(gameObject);
        Destroy(gameObject);
    }

    private void PlateCounter_OnPlateAdd(object sender, System.EventArgs e)
    {
        Transform plateTransform = Instantiate(plateVisual, spawnPos);
        float plaTeOffset = 0.1f;
        plateTransform.localPosition = new Vector3(0,plaTeOffset * gameObjects.Count,0);
        gameObjects.Add(plateTransform.gameObject);
    }
}
