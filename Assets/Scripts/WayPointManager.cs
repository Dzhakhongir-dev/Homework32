using UnityEditor;
using UnityEngine;

public class WayPointManager : EditorWindow
{

    [MenuItem("Tools/Way Point Editor")]
    public static void Open()
    {
        GetWindow<WayPointManager>();
    }

    public Transform wayPointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("wayPointRoot"));

        if (wayPointRoot == null)
        {
            EditorGUILayout.HelpBox("Прикрепите Корень Трансформа - WayPointRoot", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWayPoint();
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WayPoints>())
        {
            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWayPointAfter();
            }
            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWayPointBefore();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWayPoint();
            }
        }
    }

    private void CreateWayPoint()
    {
        GameObject wayPointObject = new GameObject("Waypoint - " + wayPointRoot.childCount, typeof(WayPoints));

        wayPointObject.transform.SetParent(wayPointRoot, false);

        WayPoints wayPoints = wayPointObject.GetComponent<WayPoints>();
        if (wayPointRoot.childCount > 1)
        {
            wayPoints.previousWaypoint = wayPointRoot.GetChild(wayPointRoot.childCount - 2).GetComponent<WayPoints>();
            wayPoints.previousWaypoint.nextWaypoint = wayPoints;

            wayPoints.transform.position = wayPoints.previousWaypoint.transform.position;
            wayPoints.transform.forward = wayPoints.previousWaypoint.transform.forward;
        }
        Selection.activeObject = wayPoints.gameObject;

    }

    private void CreateWayPointAfter()
    {
        GameObject wayPointObject = new GameObject("Waypoint - " + wayPointRoot.childCount, typeof(WayPoints));
        wayPointObject.transform.SetParent(wayPointRoot, false);

        WayPoints newWaypoint = wayPointObject.GetComponent<WayPoints>();

        WayPoints selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoints>();

        wayPointObject.transform.position = selectedWaypoint.transform.position;
        wayPointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypoint = selectedWaypoint;

        if (selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        selectedWaypoint.nextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    private void CreateWayPointBefore()
    {
        GameObject wayPointObject = new GameObject("Waypoint - " + wayPointRoot.childCount, typeof(WayPoints));
        wayPointObject.transform.SetParent (wayPointRoot, false);

        WayPoints newWaypoint = wayPointObject.GetComponent<WayPoints>();
        
        WayPoints selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoints>();

        wayPointObject.transform.position = selectedWaypoint.transform.position;
        wayPointObject.transform.forward = selectedWaypoint.transform.forward;

        if (selectedWaypoint.previousWaypoint != null)
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }

    private void RemoveWayPoint()
    {
        WayPoints selectedWaypoint = Selection.activeGameObject.GetComponent<WayPoints>();

        if (selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }

        if (selectedWaypoint.previousWaypoint != null)
        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;

            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }


}
