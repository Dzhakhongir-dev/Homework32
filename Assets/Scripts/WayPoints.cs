using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public WayPoints previousWaypoint;
    public WayPoints nextWaypoint;

    [Range(0f, 10f)]
    public float width = 0f;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 10f));
    }
}
