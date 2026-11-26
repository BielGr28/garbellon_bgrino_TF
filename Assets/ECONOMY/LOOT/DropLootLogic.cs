
using System.Collections;
using UnityEngine;

public class DropLootLogic : MonoBehaviour
{
    [Header("References")]
    private DropsType dropsType;
    private Collider playerCol;
    [Header("Drop Effect")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float dropDuration = 0.5f;
    private bool hasLanded;
    private Vector3 startFloatPos;
    [Header("Loot Follow Player")]
    private float playerDistance;
    [SerializeField] private float detectionRange;
    [SerializeField] private float followSpeed;
    private bool isLooted;


    void Awake()
    {
        dropsType = GetComponent<DropsType>();
        playerCol = GameObject.FindWithTag("Player").transform.parent.GetComponent<Collider>();
        hasLanded = false; 
        isLooted = false;
    }
    private void Update()
    {
        FloatingLoot();
        LootFollowPlayer();
    }


#region Drop Logic
    public void Initialize(Vector3 targetPosition)
    {
        StartCoroutine(JumpRoutine(targetPosition));
    }

    private IEnumerator JumpRoutine(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;

        float timer = 0f;

        while (timer < dropDuration)
        {
            timer += Time.deltaTime;

            float t = timer / dropDuration;

            // Moviment horitzontal
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);

            // Arc parabòlic
            currentPos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = currentPos;

            yield return null;
        }

        transform.position = targetPosition;
        startFloatPos = targetPosition;
        hasLanded = true;
    }

   private void FloatingLoot() //Loot floating effect
    {
        if (hasLanded && !isLooted)
        {
            Vector3 pos = startFloatPos;
            pos.y += Mathf.Sin(Time.time * 3f) * 0.2f;
            transform.position = pos;

            CoinSpinAnim();
        }
    }
#endregion Drop Logic


#region Specific functions
    private void LootFollowPlayer() //Allows follow player when in range
    {
        if(hasLanded)
        {
            playerDistance = Vector3.Distance(transform.position, playerCol.transform.position);
            if(playerDistance <= detectionRange)
            {
                isLooted = true;
                GetComponent<DisableObjectOnTIme>().enabled = false; //when player reached distance no need to destroy loot

                float incrementalSpeed = Mathf.Lerp(followSpeed/2, followSpeed*1.5f, 1f-(playerDistance/detectionRange)); //Loot speed increases on "chase"

                transform.position = Vector3.MoveTowards(transform.position, playerCol.bounds.center, incrementalSpeed * Time.deltaTime); //Loot moves to player
            }
        }
        
    }

    void CoinSpinAnim()
    {
        if(dropsType.currentDrop == DropsType.Drop.Coin)
        {
            transform.Rotate(0f, 0f, 90f * Time.deltaTime, Space.Self);
        }
    }
#endregion Specific functions
}   
