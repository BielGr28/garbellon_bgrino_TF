
using System.Collections;
using UnityEngine;

public class DropLootLogic : MonoBehaviour
{
    [Header("Drop Effect")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float dropDuration = 0.5f;
    private bool hasLanded;


    private void Update()
    {
        if (!hasLanded)
            return;

        // Petit efecte de flotació
        Vector3 pos = transform.position;

        pos.y += Mathf.Sin(Time.time * 3f) * 0.003f;

        transform.position = pos;
    }


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
        hasLanded = true;
    }
}   
