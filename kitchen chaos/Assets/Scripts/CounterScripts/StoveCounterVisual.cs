using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounterScript stoveCounter;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject stoveOn;

    private void Start()
    {
        stoveCounter.OnStoveObject += StoveCounter_OnStoveObject;
    }

    private void StoveCounter_OnStoveObject(object sender, StoveCounterScript.OnStoveObjectEventHandler e)
    {
        if (e.state == StoveCounterScript.States.cooking || e.state == StoveCounterScript.States.cooked)
        {
            Show();
        }
        else { Hide(); }
    }
    private void Show() 
    {
        particle.SetActive(true);
        stoveOn.SetActive(true);
    }
    private void Hide() {
        particle.SetActive(false);
        stoveOn.SetActive(false);
    }
}
