using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Arrive : SteeringBehaviour {
    public Vector3 targetPosition = Vector3.zero;
    public float slowingDistance = 30f;

    public GameObject targetGameObject = null;

    public override Vector3 Calculate() {
        return boid.ArriveForce(targetPosition, slowingDistance);
    }

    public void Update() {
        if (targetGameObject != null) {
            targetPosition = targetGameObject.transform.position;
        }
    }
}