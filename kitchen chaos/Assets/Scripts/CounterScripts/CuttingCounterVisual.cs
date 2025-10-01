using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounterScript cuttingCounter;
    private Animator animator;
    private const string Cut = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnPlayerCut += CuttingCounter_OnPlayerCut;
    }
private void CuttingCounter_OnPlayerCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(Cut);
    }

}
