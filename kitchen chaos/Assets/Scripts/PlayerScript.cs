using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerScript Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnObjectPickup;
    
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private InputScript gameInput;
    [SerializeField] private LayerMask clearlayer;
    [SerializeField] private Transform spawnPosition;

    Vector3 lastDir;
    private bool isWalking;
    private bool canMove;
    private BaseCounter baseCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance must be null!");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInterAction += GameInput_OnInterAction;
        gameInput.OnAlternativeInteractAction += GameInput_OnAlternativeInteractAction;
    }

    private void GameInput_OnAlternativeInteractAction(object sender, EventArgs e)
    {
        if (!GameHandler.Instance.IsGamePlaying()) { return; }
        if (baseCounter != null)
        {
            baseCounter.AlternativeInteract(this);
        }
    }

    private void GameInput_OnInterAction(object sender, System.EventArgs e)
    {
        if (!GameHandler.Instance.IsGamePlaying()) { return; }
        if (baseCounter != null)
        {
            baseCounter.Interact(this);
        }
    }

    private void Update()
    {
        MoveHandle();
        Interact();

    }
    private void Interact()
    {
        Vector2 moveVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(moveVector.x, 0, moveVector.y);
        float maxDistance = 2f;
        if (moveDir != Vector3.zero)
        {
            lastDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastDir, out RaycastHit hitinfo, maxDistance, clearlayer))
        {
            if (hitinfo.transform.TryGetComponent<BaseCounter>(out BaseCounter clearcount))
            {
                baseCounter = clearcount;
                SetSelectedCounter(clearcount);
            }
            else
            {
                baseCounter = null;
                SetSelectedCounter(null);
            }
        }
        else
        {
            baseCounter = null;
            SetSelectedCounter(null);
        }
    }
    private void MoveHandle()
    {

        float playerHieght = 2f;
        float playerRadius = 0.7f;
        float playerMoveDistance = playerSpeed * Time.deltaTime;
        Vector2 moveVector = gameInput.GetInputVectorNormalized();
        Vector3 moveDir = new Vector3(moveVector.x, 0, moveVector.y);
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, playerRadius, moveDir, playerMoveDistance);
        if (!canMove)
        {
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, playerRadius, moveDirx, playerMoveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            }
            else
            {
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, playerRadius, moveDirz, playerMoveDistance);
                if (canMove)
                {
                    moveDir = moveDirz;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * playerMoveDistance;
        }
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, 10 * Time.deltaTime);
        }
        isWalking = moveDir != Vector3.zero;


    }
    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.baseCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public bool Iswalking()
    {
        return isWalking;
    }
    public Transform GetSpawnPos()
    {
        return spawnPosition;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public bool HasKithcenObject()
    {
        return kitchenObject != null;
    }
    public KitchenObject GetKithcenObject()
    {
        return kitchenObject;
    }
    public void SetKitchenObjects(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (this.kitchenObject != null)
        {
            OnObjectPickup?.Invoke(this, EventArgs.Empty);
        }
    }
}
