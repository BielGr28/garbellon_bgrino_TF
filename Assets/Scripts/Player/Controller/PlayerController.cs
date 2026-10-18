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
    public bool Crouching = false;
    public bool Sliding  = false;
    public float SlideSpeed;
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
    private float _coyoteTimer;
    private float _fallTimer;
    private float _slideTimer;
    private Vector3 lookDirection;

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
        _slideTimer = SlideTime;
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
        //_controller.isGrounded 
    }


    //Escollir velocitat segons input, girar, check sprint input...
    void Move()
    {

        // _contoler.Move() expects a Vector 3 value.
        // We need to provide a Vector3 and use our speed to multiply it by Time.deltaTime.

        if(_sprintAction.IsPressed())
        {
            Debug.Log("SPRINTING");
            _speed = SprintSpeed;
        }
        else
        {
            _speed = MoveSpeed;
        }
        

        //TODO: Implementar slide, legir una direcció, només linia recta.

        Vector2 inputValue = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(inputValue.x, 0.0f, inputValue.y).normalized;

        //float currentSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        //float speedOffset = 0.1f;
        //float SpeedChangeRate = 10.0f;

        move = Vector3.ClampMagnitude(move,1f);

        /*if(currentSpeed < _speed - speedOffset || currentSpeed > _speed + speedOffset)
        {
            _speed = Mathf.Lerp(currentSpeed, _speed, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
               if(_sprintAction.IsPressed())
        {
            Debug.Log("SPRINTING");
            _speed = SprintSpeed;
        }
        else
        {
            _speed = MoveSpeed;
            if(_sprintAction.IsPressed())
            {
                
                _speed = SprintSpeed;
            }
            else
            {
                _speed = MoveSpeed;
            }
        }   
        */

        if(move != Vector3.zero)
        {
           move = transform.right * inputValue.x + transform.forward * inputValue.y;
        }

        //move += Vector3.up * _verticalVelocity;
        _controller.Move( move *_speed * Time.deltaTime);
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
            Debug.Log("Jump");
           _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
           
        }
        Debug.Log(_verticalVelocity);
        _verticalVelocity += Gravity * Time.deltaTime;
        _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f)* Time.deltaTime);
    }

    void CrouchAndSlide()
    {
        if(Grounded && _crouchAction.WasPressedThisFrame())
        {
            Crouching = true;
            _cameraController.AdjustCameraRoot();
            if(_speed > MoveSpeed)
            {
                Slide();
            }
        }
        else if(Grounded && _crouchAction.WasReleasedThisFrame())
        {
            Crouching = false;
            _cameraController.AdjustCameraRoot();
        }  
    }

    private void Slide()
    {
        _speed = SlideSpeed;
        _slideTimer -= Time.deltaTime;
        if(_slideTimer < 0.0f)
        {
            Debug.Log("Stop Sliding");
            _speed = MoveSpeed;
        }

    }

   /* private IEnumerator Slide()
    {
        
    }*/


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
