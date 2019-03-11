using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    //public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;
    Paths path;
 

    private void Start()
    {
        path = GetComponent<Paths>();
        target = path.NextWaypoint();
    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            
            Gizmos.DrawLine(transform.position, target);
        }
    }
    
    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);    
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, target) < 2)
        {
            path.AdvanceToNext();
            target = path.NextWaypoint();
        }
    }
}