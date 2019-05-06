using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the audio files that can be used throughout the AI battle
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
