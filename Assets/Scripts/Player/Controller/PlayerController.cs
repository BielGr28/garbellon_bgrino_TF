using System;
using System.Collections;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;


/*public enum PlayerState
{
    IDLE,
    MOVING,
    CROUCHING,
    SLIDING,
    DIE
}*/

//Controls the player movement.
public class PlayerController : MonoBehaviour
{
    [Header("PLAYER INPUTS")]
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;

    [Header("PLAYER STATS")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float SlideSpeed;
    public float SpeedChange;
    //private PlayerState CurrentState{get;set;} = PlayerState.IDLE;
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
    public PlayerHealth _playerHealth;
    private float _speed;
    private float _verticalVelocity;
    private float _slideVelocity;
    private Vector3 _slideDirection;
    private float _coyoteTimer;
 
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
        _playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(_verticalVelocity);
        Move();
        Jump();
        GroundCheck();
        Slide();
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
        else if(Crouching)
        {
            _speed = MoveSpeed * 0.8f;
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
       
        Crouch(move);
    }

    void Jump()
    {
        if(Grounded)
        {
            _coyoteTimer = CoyoteTime;
            if(_verticalVelocity < -2.0f)
            {
                _verticalVelocity = -2.0f;
            }
        }
        else
        {
            _coyoteTimer -= Time.deltaTime; 
        }

        if(_coyoteTimer > 0f && _jumpAction.WasPressedThisFrame())
        {  
            if(Crouching)
            {
                Crouching = false; 
                Sliding = false;
                _cameraController.cinemachineCamera.Lens.FieldOfView = _cameraController.BaseFOV;
                _controller.height = 2.0f; 
                _cameraController.AdjustCameraRoot();
               
            } 

            
            //The Unity Documentation provides this formular in order to calculate the player jump. 
            _verticalVelocity = Mathf.Sqrt(JumpHeight * -3f * Gravity);
        }

        _verticalVelocity +=  Gravity *2f * Time.deltaTime;

        _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f)* Time.deltaTime);

        //If the player touches the celing while jumping, imitdeatly goes down.
        if( (_controller.collisionFlags & CollisionFlags.Above) !=0)
        {
            _verticalVelocity  = 0.0f;
        }
    }

    void Crouch(Vector3 movementVector) //(float currentSpeed)
    {
        //Crouching false when not grounded;
        float currentSpeed = movementVector.magnitude;
        if(Grounded && _crouchAction.WasPressedThisFrame())
        {
            Crouching = true;
            _cameraController.AdjustCameraRoot();
            _controller.height = CrouchingHeigth;
            Debug.Log(currentSpeed);
            if(currentSpeed > 0.0f)
            {
                Debug.Log("Start Slide");
                _slideDirection = movementVector;
                _slideVelocity = _speed * 1.5f;
                _cameraController.cinemachineCamera.Lens.FieldOfView = _cameraController.SlideFOV;
                Sliding = true;
            }

        }
        else if(Grounded && Crouching && _crouchAction.WasReleasedThisFrame())
        {
            Crouching = false;
            Sliding = false;
            _cameraController.cinemachineCamera.Lens.FieldOfView = _cameraController.BaseFOV;
            _controller.height = 2.0f;
            _cameraController.AdjustCameraRoot();
            
        }
  
    }  

    private void Slide()
    {
        if(Sliding && Grounded)
        {
           // _cameraController.cinemachineCamera.Lens.FieldOfView = Mathf
            _slideVelocity -= _speed *1.5f * Time.deltaTime;
            _controller.Move(_slideDirection * _slideVelocity * Time.deltaTime);
            _cameraController.cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(_cameraController.cinemachineCamera.Lens.FieldOfView,_cameraController.BaseFOV,2f * Time.deltaTime);

           // Debug.Log(_speed);

            if(_slideVelocity <= _speed * 0.7f)
            {   
                Sliding = false;
                _cameraController.cinemachineCamera.Lens.FieldOfView = _cameraController.BaseFOV;
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

    /*private void TryChangeState(PlayerState stateToChange)
    {
        if(CanChange(stateToChange))
        {
            Debug.Log($"PREVIOUS STATE: {CurrentState} || NEW STATE: {stateToChange}");
            CurrentState = stateToChange; 
        }
        
        //if(currentState = PlayerState.IDLE)
    }

    private bool CanChange(PlayerState newState)
    {   
        //Rules to Change between states
        if(CurrentState == PlayerState.IDLE || CurrentState == PlayerState.WALKING)
        {
            if(newState == PlayerState.SLIDING) return false;
        }

        if(CurrentState == PlayerState.SPRINTING)
        {
            if(newState == PlayerState.CROUCHING) return false;
        }

        if(CurrentState == PlayerState.CROUCHING)
        {
            if(newState == PlayerState.SLIDING) return false;
            if(newState == PlayerState.SPRINTING) return false;
        }

        if(CurrentState == PlayerState.SLIDING)
        {
            return true;
        }

        return true;
    }*/

}
