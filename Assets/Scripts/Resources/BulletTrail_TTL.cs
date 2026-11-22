using UnityEngine;

public class BulletTrail_TTL : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Color start = lineRenderer.startColor;
        Color end = lineRenderer.endColor;

        start.a -= 2f * Time.deltaTime;
        end.a -= 2f * Time.deltaTime;

        lineRenderer.startColor = start;
        lineRenderer.endColor = end;
        //Debug.Log($"START {lineRenderer.startColor.a} // END {lineRenderer.endColor.a}");
    }//
}
