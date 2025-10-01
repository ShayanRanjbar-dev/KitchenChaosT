using UnityEngine;

public class StoveSoundManagerScript : MonoBehaviour
{
    [SerializeField] StoveCounterScript StoveCounter;


    private void Start()
    {
        StoveCounter.OnStoveObject += StoveCounter_OnStoveObject;
    }

    private void StoveCounter_OnStoveObject(object sender, StoveCounterScript.OnStoveObjectEventHandler e)
    {
        if (e.state == StoveCounterScript.States.cooking || e.state == StoveCounterScript.States.cooked)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
