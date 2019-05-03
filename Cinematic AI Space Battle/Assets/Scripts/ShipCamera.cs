using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering;

public class ShipCamera : MonoBehaviour {

    public static ShipCamera Instance;

    protected Transform _target;
    protected Vector3 _tragetPos;

    private float _lerpSpeed = 10f;

    public static ShipCamera Create(Vector3 targetPos, Transform target) {
        GameObject go = new GameObject();
        ShipCamera camera = go.AddComponent<ShipCamera>();
       
        
        Camera physicalCam = go.AddComponent<Camera>();
        //physicalCam.transform.parent = target;
        physicalCam.transform.localRotation = target.rotation;
        physicalCam.fieldOfView = 80f;
        PostProcessingBehaviour pp = go.AddComponent<PostProcessingBehaviour>();
        pp.profile = CameraEffects.Instance.postProcessingProfile;
        camera._target = target;
        //camera._tragetPos = targetPos;
        camera._tragetPos = go.transform.position = target.transform.position - target.transform.forward * 7f;

        return camera;
    }

	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        AdjustCameraPosition();
	}

    private void AdjustCameraPosition() {
        _tragetPos = _target.transform.position - _target.transform.forward * 7f;
        _tragetPos = new Vector3(_tragetPos.x, _tragetPos.y + 2f, _tragetPos.z);
        transform.LookAt(_target);

        transform.position = Vector3.Lerp(transform.position, _tragetPos,Time.deltaTime * _lerpSpeed);
    }
}
