using UnityEngine;

public class EventArea : MonoBehaviour
{
    [SerializeField] private GameObject[] barriers; 

    public void LockArea()
    {
        foreach(GameObject bar in barriers)
        {
            bar.SetActive(true);
        }
    }

    public void UnlockArea()
    {
        foreach(GameObject bar in barriers)
        {
            bar.SetActive(false);
        }
    }
}
