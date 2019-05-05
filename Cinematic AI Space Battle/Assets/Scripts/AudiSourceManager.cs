using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiSourceManager : MonoBehaviour {

    public AudioClip _engine;
    public AudioClip _blaster;
    public AudioClip _explosion;
    public AudioClip _fire;

    public static AudiSourceManager Instance;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
