using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("CINEMACHINE")]
    public GameObject CinemachineCameraTarget;
    public float TopClamp;
    public float BottomClamp;
    private GameObject _mainCamera;



    private Vector3 _lookAmt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
