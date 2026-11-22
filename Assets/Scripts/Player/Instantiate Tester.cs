using UnityEngine;
using UnityEngine.InputSystem;

public class InstantiateTester : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction _debugAction;

    [SerializeField] private GameObject prefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _debugAction = InputSystem.actions.FindAction("Debugger");
    }

    // Update is called once per frame
    void Update()
    {
        if(_debugAction.WasPressedThisFrame())
        {
            GameObject.Instantiate(prefab,transform.position,transform.rotation,transform);
        }
    }
}
