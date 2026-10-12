using UnityEngine;

public class Charging_Script : MonoBehaviour
{
    private GameObject player;
    private float detectionArea = 10.0f;
    public Color color;

    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform.position);
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private Vector3 FindPlayer()
    {
        Vector3 playerPositionOnAttack;
        playerPositionOnAttack = player.transform.position;
        transform.LookAt(playerPositionOnAttack);
        return playerPositionOnAttack;;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position,detectionArea);
    }

    
}
