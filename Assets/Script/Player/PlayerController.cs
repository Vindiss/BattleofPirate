using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackRightAction;
    [SerializeField] private InputActionReference attackLeftAction;
    [FormerlySerializedAs("bordeRight")] [SerializeField] private GameObject bordeRightPrefab;
    [FormerlySerializedAs("bordeLeft")] [SerializeField] private GameObject bordeLeftPrefab;
    [SerializeField] private Transform bordeShootRight;
    [SerializeField] private Transform bordeShootLeft;
    [SerializeField] private float bordeForce = 100f;
    

    private Vector2 _moveInput;
    private bool _isAttackingLeft;
    private bool _isAttackingRight;
    
    private void OnEnable()
    {
        moveAction.action.performed += OnMove;
        moveAction.action.canceled += OnMoveCanceled;
        moveAction.action.Enable();
        
        attackRightAction.action.performed += AttackRight;
        attackRightAction.action.canceled += AttackRightCanceled;
        attackRightAction.action.Enable();
        
        attackLeftAction.action.performed += AttackLeft;
        attackLeftAction.action.canceled += AttackLeftCanceled;
        attackLeftAction.action.Enable();
    }
    
    private void OnDisable()
    {
        moveAction.action.performed -= OnMove;
        moveAction.action.canceled -= OnMoveCanceled;
        moveAction.action.Disable();
        
        attackRightAction.action.performed -= AttackRight;
        attackRightAction.action.canceled -= AttackRightCanceled;
        attackRightAction.action.Disable();
        
        attackLeftAction.action.performed -= AttackLeft;
        attackLeftAction.action.canceled -= AttackLeftCanceled;
        attackLeftAction.action.Disable();
    }
    private void AttackRightCanceled(InputAction.CallbackContext context)
    {
        _isAttackingRight = false;
    }

    private void AttackRight(InputAction.CallbackContext context)
    {
        _isAttackingRight = context.ReadValueAsButton();
        
        if (_isAttackingRight && GetComponent<ManagePlayer>().GetRelodRight() == 0 && Time.timeScale != 0f && GetComponent<UIUpgradePlayers>().GetUpgradded() == false)
        {
            GameObject borde = Instantiate(bordeRightPrefab, bordeShootRight.position, bordeShootRight.rotation);
            borde.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * bordeForce, ForceMode.Impulse);
            Destroy(borde, 5f);
            GetComponent<ManagePlayer>().ReloadRight();
        }
    }
    private void AttackLeftCanceled(InputAction.CallbackContext context)
        {
            _isAttackingLeft = false;
        }
    private void AttackLeft(InputAction.CallbackContext context)
        {
            _isAttackingLeft = context.ReadValueAsButton();
            
            if (_isAttackingLeft && GetComponent<ManagePlayer>().GetRelodLeft() == 0 && Time.timeScale != 0f && GetComponent<UIUpgradePlayers>().GetUpgradded() == false)
            {
                GameObject borde = Instantiate(bordeLeftPrefab, bordeShootLeft.position, bordeShootLeft.rotation);
                borde.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * -bordeForce, ForceMode.Impulse);
                Destroy(borde, 5f);
                GetComponent<ManagePlayer>().ReloadLeft();
            }
        }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    void Update()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // Rotation du joueur vers la direction calculée
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, GetComponent<ManagePlayer>().GetRotationSpeed() * Time.deltaTime);

            // Déplacement du joueur en avant dans la direction actuelle de la rotation
            transform.Translate(Vector3.forward * (GetComponent<ManagePlayer>().GetMoveSpeed() * Time.deltaTime), Space.Self);
        }
    }
}
