using UnityEngine;

public enum Type
{
    Destroy,
    Disable
}

public class DisableObjectOnTIme : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisapear;
    private float timer;
    public Type currentType;

    void Start()
    {
        timer = timeBeforeDisapear;
    }
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if(currentType == Type.Destroy)
        {
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if(currentType == Type.Disable)
        {
            if(timer <= 0)
            {
                gameObject.SetActive(false);
                timer = timeBeforeDisapear;
            }
        }

        
    }
}
