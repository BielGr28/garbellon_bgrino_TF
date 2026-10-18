using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("PLAYER INPUTS")]
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _lookAction;
    
    [Header("CINEMACHINE")]
    public GameObject CameraRoot;
    private float _cameraSensitivity;

    public float RotationSpeed;
    private Vector3 _cameraPosition;
    public float TopClamp;
    public float BottomClamp;
    private GameObject _mainCamera;
    private Vector3 _lookAmt;
    public float CrounchingOffset;
    
    
    private PlayerController _playerController;
    private float _cameraRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _cameraPosition = CameraRoot.transform.position;
        _lookAction = InputSystem.actions.FindAction("Look");
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    public void AdjustCameraRoot()
    {
        if(_playerController.Crouching || _playerController.Sliding)
        {
            CameraRoot.transform.position  =  CameraRoot.transform.position - Vector3.down * CrounchingOffset;
        }
        else
        {
            CameraRoot.transform.position = CameraRoot.transform.position - Vector3.up * CrounchingOffset;
        }
    }

    private void CameraRotation()
    {
        Vector2 lookValue = _lookAction.ReadValue<Vector2>();
        _cameraSensitivity += lookValue.y * RotationSpeed;
        _cameraRotation = lookValue.x * RotationSpeed;

        _cameraSensitivity = Mathf.Clamp(_cameraSensitivity, BottomClamp, TopClamp);

        CameraRoot.transform.localRotation = Quaternion.Euler(_cameraSensitivity,0.0f, 0.0f);

        transform.Rotate(Vector3.up * _cameraRotation);
    }

    /*private float ClampAngle(float angle, float minAngle, float maxAngle)
    {
        if(angle < -360f)
        {
            angle += 360f;
        }
        else if(angle > 360f)
        {
            angle -= 360f;
        }
    }*/
}
