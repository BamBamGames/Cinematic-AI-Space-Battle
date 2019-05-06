using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Implemented from class, no need to reimplement
public class FollowPath : SteeringBehaviour {

    public Paths path;

    Vector3 nextWaypoint;

    public void OnDrawGizmos() {
        if (isActiveAndEnabled && Application.isPlaying) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, nextWaypoint);
        }
    }

    public void Start() {

    }

    public override Vector3 Calculate() {
        nextWaypoint = path.NextWaypoint();
        if (Vector3.Distance(transform.position, nextWaypoint) < 3) {

            if (gameObject.name == "HQ") {
                
                GetComponent<Boid>().enabled = false;
                enabled = false;
            }

            if (gameObject.name == "MainCamera") {
    
            }

            path.AdvanceToNext();
        }

        if (!path.looped && path.IsLast()) {
            Debug.Log("ARRIVING FORCE");
            return boid.ArriveForce(nextWaypoint, 50);
        } else {
            Debug.Log("Seeking force");
            return boid.SeekForce(nextWaypoint);
        }
    }
}