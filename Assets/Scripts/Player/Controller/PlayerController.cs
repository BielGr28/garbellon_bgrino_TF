
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Controls the player movement.
public class PlayerController : MonoBehaviour
{
    [Header("PLAYER INPUTS")]
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;
    private Vector3 _movementAmt;

    [Header("PLAYER STATS")]
    public float MoveSpeed;
    public float SprintSpeed;
    public bool Sliding;
    public float SlideSpeed;
    public float JumpHeight;
    public float Gravity;

    [Space(10)]
    public float JumpBuffer;
    public float CoyoteTime;
    public float SlideTime;
    public float FallTime;


    private CharacterController _controller;
    private float _speed;
    private float _bufferTimer;
    private float _verticalVelocity;
    private float _coyoteTimer;
    private float _fallTimer;

    [Header("GROUND CHECK")]
    public bool Grounded = true;
    public float GroundedOffset = -0.1f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;



   /* void OnEnable()
    {
        //assing our input map
        InputSystem.actions.FindActionMap("PlayerInp").Enable();
    }

    void OnDisable()
    {
        InputSystem.actions.FindActionMap("PlayerInp").Disable();
    }*/

    void Awake()
    {

        //assing player inputs
        _moveAction = InputSystem.actions.FindAction("Move");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _crouchAction = InputSystem.actions.FindAction("Crouch");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        GroundCheck();
        CrouchAndSlide();
    }

    //Tells us if the player is touching the ground or not.
    void GroundCheck()
    {
        Vector3 groundCheckPos = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(groundCheckPos, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }


    //Escollir velocitat segons input, girar, check sprint input...
    void Move()
    {
        
    }

    void Jump()
    {
        if(Grounded)
        {
            _coyoteTimer = CoyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime; 
        }

        if(_coyoteTimer > 0f && _jumpAction.WasPressedThisFrame())
        {
            Debug.Log("Jump");
           _verticalVelocity = Mathf.Sqrt(JumpHeight * -3f * -9.8f);
           
        }
        else
        {
            
        }
        _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f)* Time.deltaTime);
    }

    void CrouchAndSlide()
    {
        
    }


    void OnDrawGizmosSelected()
    {
        Color groundedColor = new Color(0.0f,1.0f,0.0f,0.50f);
        Color notGroundedColor = new Color(1.0f,0.0f,0.0f,0.50f);

        if(Grounded)
            Gizmos.color = groundedColor;
        else
            Gizmos.color = notGroundedColor;    

        
        Gizmos.DrawSphere(new Vector3(transform.position.x,transform.position.y - GroundedOffset, transform.position.z),GroundedRadius);
    }
}
