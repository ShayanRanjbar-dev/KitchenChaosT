using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    private Animator animator;
    private const string Is_Walking = "IsWalking"; 
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(Is_Walking, player.Iswalking());
    }
}
