using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WayPointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(WayPoints wayPoints, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }

        Gizmos.DrawSphere(wayPoints.transform.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(wayPoints.transform.position + (wayPoints.transform.right * wayPoints.width / 2f),
                        wayPoints.transform.position - (wayPoints.transform.right * wayPoints.width / 2f));

        if (wayPoints.previousWaypoint != null)
        {
            Gizmos.color = Color.red;

            Vector3 offset = wayPoints.transform.right * wayPoints.width / 2f;
            Vector3 offsetTo = wayPoints.previousWaypoint.transform.right * wayPoints.previousWaypoint.width / 2f;

            Gizmos.DrawLine(wayPoints.transform.position + offset, wayPoints.previousWaypoint.transform.position + offsetTo);

        }
        if (wayPoints.nextWaypoint != null)
        {
            Gizmos.color = Color.green;

            Vector3 offset = wayPoints.transform.right * -wayPoints.width / 2f;
            Vector3 offsetTo = wayPoints.nextWaypoint.transform.right * -wayPoints.nextWaypoint.width / 2f;

            Gizmos.DrawLine(wayPoints.transform.position + offset, wayPoints.nextWaypoint.transform.position + offsetTo);

        }

    }
}
