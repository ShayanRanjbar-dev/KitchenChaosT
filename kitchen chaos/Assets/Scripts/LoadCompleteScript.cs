using UnityEngine;

public class LoadCompleteScript : MonoBehaviour
{

    private bool isFirstUpdate = true;
    void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            SceneLoader.OnLoadingComplete();
        }
    }
}
