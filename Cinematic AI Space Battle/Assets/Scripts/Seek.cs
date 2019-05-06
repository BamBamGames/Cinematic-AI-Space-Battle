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
    public Transform _target;
    public bool _followingPath = false;
 

    private void Start()
    {
        try {
            _followingPath = true;
            path = GetComponent<Paths>();
            target = path.NextWaypoint();
        } catch {
            _followingPath = false;
            target = _target.position;
            Debug.Log("someshit");
        }
    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            
            Gizmos.DrawLine(transform.position, _target.position);
        }
    }
    
    public override Vector3 Calculate()
    {
        return boid.SeekForce(_target.position);    
    }

    public void Update()
    {
        if (_followingPath && Vector3.Distance(transform.position, target) < 2)
        {
            if (gameObject.name == "HQ") {

                GetComponent<Seek>().enabled = false;
                GetComponent<FollowPath>().enabled = false;
                this.enabled = false;
                GetComponent<Boid>().enabled = false;
            } else {

                path.AdvanceToNext();
                target = path.NextWaypoint();

            }
        }
    }
}