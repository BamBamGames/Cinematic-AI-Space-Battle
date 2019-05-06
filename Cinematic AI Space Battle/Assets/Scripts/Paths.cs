using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{

    public List<Transform> transforms = new List<Transform>();
    private List<Vector3> waypoints = new List<Vector3>();

    public int next = 0;
    public bool looped = true;

    /*
    public void OnDrawGizmos()
    {
        int count = looped ? (transform.childCount + 1) : transform.childCount;
        Gizmos.color = Color.cyan;
        for (int i = 1; i < count; i++)
        {
            Transform prev = transform.GetChild(i - 1);
            Transform next = transform.GetChild(i % transform.childCount);
            Gizmos.DrawLine(prev.transform.position, next.transform.position);
            Gizmos.DrawSphere(prev.position, 1);
            Gizmos.DrawSphere(next.position, 1);
        }
    }
    */

    // Use this for initialization
    void Start()
    {
        foreach (Transform t in transforms) {
            waypoints.Add(t.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 NextWaypoint()
    {
        return waypoints[next];
    }

    public void AdvanceToNext()
    {
        if (looped)
        {
            next = (next + 1) % waypoints.Count;
        }
        else
        {
            if (next != waypoints.Count - 1)
            {
                next++;
            }
        }
    }

    public bool IsLast()
    {
        return next == waypoints.Count - 1;
    }

}