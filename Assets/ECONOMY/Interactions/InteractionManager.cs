using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//ATTACH TO PLAYER 

public class InteractionManager : MonoBehaviour
{

    [Header("Interaction Settings")]
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private Camera cam;

    [Header("Interactable references")]
    [SerializeReference] private TextMeshProUGUI promptText;
    [SerializeReference] private StoreManager storeManager;
    

    void Awake()
    {
        cam = Camera.main;
        promptText = GameObject.Find("Prompt_text").GetComponent<TextMeshProUGUI>();
        storeManager = GameObject.Find("StoreManager").GetComponent<StoreManager>();
    }

    void Update()
    {
        StoreInteraction();
    }

    void StoreInteraction()
    {
        promptText.text = string.Empty; //Resets prompt text

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null) //checks if is interactable and its a store
            {
                promptText.text =  hitInfo.collider.GetComponent<Interactable>().promptMessage;
                
                if(storeManager.isStoreOpened == false && Input.GetKeyDown(KeyCode.F))
                {
                    storeManager.OpenStore();
                }
                else if (storeManager.isStoreOpened == true && Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
                {
                    storeManager.CloseStore();
                }
            }
        }
    }

}
