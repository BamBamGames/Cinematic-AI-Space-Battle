using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMaths : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector3 GetMeanVector(List<GameObject> objects) {

        if (objects.Count == 0)
            return Vector3.zero;

        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (GameObject go in objects) {
            x += go.transform.position.x;
            y += go.transform.position.y;
            z += go.transform.position.z;
        }
        return new Vector3(x / objects.Count, y / objects.Count, z / objects.Count);
    }
}
