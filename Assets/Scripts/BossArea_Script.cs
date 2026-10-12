using UnityEngine;

public class BossArea_Script : MonoBehaviour
{
    [SerializeField] private float scalingFactor;
    public float MAXAREA;
    public Vector3 originalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(1* scalingFactor * Time.deltaTime,0,1* scalingFactor * Time.deltaTime) ;
        if(transform.localScale.x >= MAXAREA)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    
}
