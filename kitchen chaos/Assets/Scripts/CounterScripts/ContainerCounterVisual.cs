using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounterScript containerCounterScript;
    private Animator animator;
    private const string Open_Close = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounterScript.OnPlayerGrab += ContainerCounterScript_OnPlayerGrab;
    }

    private void ContainerCounterScript_OnPlayerGrab(object sender, System.EventArgs e)
    {
        animator.SetTrigger(Open_Close);
    }
}
