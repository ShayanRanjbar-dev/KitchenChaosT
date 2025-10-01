using UnityEngine;
using UnityEngine.UI;

public class CuttingProgressUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject hasProgressBar;
    private IProgressBarParent proggressBarParent;

    private void Awake()
    {
        proggressBarParent = hasProgressBar.GetComponent<IProgressBarParent>();
    }
    private void Start()
    {
        proggressBarParent.OnProgressBarValueChanged += progressBarParent_OnProgressBarValueChanged;
        progressBar.fillAmount = 0f;
        Hide();
    }

    private void progressBarParent_OnProgressBarValueChanged(object sender, IProgressBarParent.OnProgressBarValueChangedEventArgs e)
    {
        progressBar.fillAmount = e.progress;
        if (e.progress <= 0 || e.progress >= 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);

    }
}
