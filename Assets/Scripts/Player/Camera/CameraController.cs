using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("PLAYER INPUTS")]
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _lookAction;
    private InputAction _escAction;
    
    [Header("CINEMACHINE")]
    public GameObject CameraRoot;
    private float _cameraSensitivity;
    public float RotationSpeed;
    public float TopClamp;
    public float BottomClamp;
    public float CrounchingOffset;
    
    
    private PlayerController _playerController;
    private float _cameraRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _lookAction = InputSystem.actions.FindAction("Look");
        _escAction = InputSystem.actions.FindAction("Esc");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        ChangeCursorState();
    }

    public void AdjustCameraRoot()
    {
        if(_playerController.Crouching)
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
        if(Cursor.lockState != CursorLockMode.None)
        {
            Vector2 lookValue = _lookAction.ReadValue<Vector2>();
            _cameraSensitivity += lookValue.y * RotationSpeed;
            float cameraX = lookValue.x * RotationSpeed;
            _cameraRotation = lookValue.x * RotationSpeed;
            //float bodyRotation = lookValue.x * RotationSpeed *2f;

            _cameraSensitivity = Mathf.Clamp(_cameraSensitivity, BottomClamp, TopClamp);

            /*
            Funciona perfecte pero el jugador es moura mentre fa el slide.
            CameraRoot.transform.localRotation = Quaternion.Euler(_cameraSensitivity,0.0f,0.0f);
            transform.Rotate(Vector3.up * _cameraRotation);

            La camera és mou sola. Pero el cos no, llavors he de trobar una manera que es relacioni. 
            CameraRoot.transform.localRotation = Quaternion.Euler(_cameraSensitivity,_cameraRotation,0.0f);
            

            */

            //Allows the camera to rotate.
            CameraRoot.transform.localRotation = Quaternion.Euler(_cameraSensitivity,0.0f,0.0f);
            if(!_playerController.Sliding)
            {
                //Makes the whole player body rotate, including the camera.

                transform.Rotate(Vector3.up * _cameraRotation);
                //transform.Rotate(Vector3.up * cameraX);
                //transform.rotation = Quaternion.Euler(0.0f,_cameraRotation * 1.0f,0.0f);
            }
   

            //Debug.Log($"CAMERA ROTATION: {CameraRoot.transform.localRotation}  || PLAYER ROTATIOn: {transform.rotation}");
            
        }
    }

    private void ChangeCursorState()
    {
        if(_escAction.WasPressedThisFrame())
        {
            if(Cursor.lockState == CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } 
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
                
        }
    }

}
