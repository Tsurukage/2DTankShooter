using UnityEngine;

public class Laser : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    LineRenderer lineRenderer;
    public LayerMask layerMask;
    private Ray2D ray;
    private RaycastHit2D hit;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        ray = new Ray2D(transform.position, transform.up);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, layerMask);

            if (hit)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector2.Distance(ray.origin, hit.point);
                Vector2 updateDirection = Vector2.Reflect(ray.direction, hit.normal);
                ray = new Ray2D(hit.point + updateDirection * 0.01f, updateDirection);
                if (hit.collider.tag != "Obstacle")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
    public void EnableLine(bool isShow)
    {
        lineRenderer.enabled = isShow;
    }
}