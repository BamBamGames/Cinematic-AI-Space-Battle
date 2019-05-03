using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraEffects : MonoBehaviour {

    public PostProcessingProfile postProcessingProfile;
    public static CameraEffects Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
