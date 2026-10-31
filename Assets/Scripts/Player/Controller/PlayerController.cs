using System.Collections;
//using System.Numerics;
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
    //private Vector3 _movementAmt;

    [Header("PLAYER STATS")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float SlideSpeed;
    public float SpeedChange;
    public bool Crouching = false;
    public float CrouchingHeigth;
    public bool Sliding  = false;
    public float SlideDamping;
    public float JumpHeight;
    public float Gravity;

    [Space(10)]
    public float JumpBuffer;
    public float CoyoteTime;
    public float SlideTime;
    public float FallTime;


    private CharacterController _controller;
    private CameraController _cameraController;
    private float _speed;
    private float _bufferTimer;
    private float _verticalVelocity;
    private float _slideVelocity;
    private Vector3 _slideDirection;
    private float _coyoteTimer;
    private float _fallTimer;
 
    [Header("GROUND CHECK")]
    public bool Grounded = true;
    public float GroundedOffset = -0.1f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;



   /*void OnEnable()
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
        _cameraController = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Sliding);
        //Debug.Log($"CURRENT VELOCITY {_controller.velocity}");
        
        Move();
        Jump();
        GroundCheck();
        Slide();
        //Debug.Log(_slideVelocity);
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
        // _contoler.Move() expects a Vector 3 value.
        // We need to provide a Vector3 and use our speed to multiply it by Time.deltaTime.

        if(_sprintAction.IsPressed())
        {
            _speed = SprintSpeed;
        }
        else
        {
            _speed = MoveSpeed;
        }
        
        Vector2 inputValue = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(inputValue.x, 0.0f, inputValue.y).normalized;


        move = Vector3.ClampMagnitude(move,1f);
        if(move != Vector3.zero)
        {
            move = transform.right * inputValue.x + transform.forward * inputValue.y;
        }
        
        if(!Sliding)
        {
            _controller.Move( move *_speed * Time.deltaTime);
        }  
       
        Crouch(move.magnitude);
    }

    void Jump()
    {
        if(Grounded)
        {
            _coyoteTimer = CoyoteTime;
            if(_verticalVelocity < -2f)
            {
                _verticalVelocity = -2f;
            }
        }
        else
        {
            _coyoteTimer -= Time.deltaTime; 
        }

        if(_coyoteTimer > 0f && _jumpAction.WasPressedThisFrame())
        {
            Sliding = false;
            //The Unity Documentation provides this formular in order to calculate the player jump.
           _verticalVelocity = Mathf.Sqrt(JumpHeight * -3f * Gravity);
           
        }

        _verticalVelocity += Gravity *1.5f * Time.deltaTime;
        _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f)* Time.deltaTime);
    }

    void Crouch(float currentspeed)
    {
        
        if(Grounded && _crouchAction.WasPressedThisFrame())
        {
            Crouching = true;
            _cameraController.AdjustCameraRoot();
            _controller.height = CrouchingHeigth;
            Debug.Log(currentspeed);
            //No agafa bé la velocitat.
            //Ns que cony fer la veritat, he de trobar un amanera de detecar b'e la velocitat - moviemnt
            //Poder millor ficar-ho dins del move pq puc agafar el move de referencia.
            //float currentSpeed =  _controller.velocity.magnitude;
            //Debug.Log($"PLAYER VELOCITY (Crouch): {_controller.velocity.magnitude}");
            if(currentspeed > 0.0f)
            {
                Debug.Log("Start Slide");
                _slideDirection = transform.forward;
                _slideVelocity = _speed * 1.5f;
                Sliding = true;
            }

        }
        else if(Grounded && _crouchAction.WasReleasedThisFrame())
        {
            Crouching = false;
            _controller.height = 2.0f;
            _cameraController.AdjustCameraRoot();
            Sliding = false;
        }  
    }  

    private void Slide()
    {
        if(Sliding && Grounded)
        {
            _slideVelocity -= _speed *1.5f * Time.deltaTime;
            _controller.Move(_slideDirection * _slideVelocity * Time.deltaTime);

            Debug.Log(_speed);

            if(_slideVelocity <= _speed * 0.7f)
            {
                Sliding = false;
                Debug.Log("STOP SLIDING");
            }

        }       
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
