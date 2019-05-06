using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to run and direct the last scene of the program
public class EndSceneDirector : MonoBehaviour {

    public GameObject _ship;
    public GameObject _shipParts;
    public List<GameObject> _shipPartsList;
    public GameObject _effectsParent;
    public List<GameObject> _effects;
    public float _waitBetweenEffcects = 3f;
    public float power;
    public Transform explosionPos;
    public float radius;
    public float timer;

    public GameObject _enemyShip;
    public GameObject _camera;

    public AudioClip _endSequence;
    public AudioSource _endSource;

	// Use this for initialization
	void Start () {
        _endSource = gameObject.AddComponent<AudioSource>();
        _endSource.clip = _endSequence;
        _endSource.volume = 1f;
        _endSource.Play(0);

        _enemyShip.GetComponent<FollowPath>().enabled = true;
        _camera.GetComponent<FollowPath>().enabled = true;
        GetEffects();
        StartCoroutine(TriggerEffects());
        StartCoroutine(Timer());
	}

    private void GetEffects() {
        foreach (Transform t in _effectsParent.transform) {
            _effects.Add(t.gameObject);
        }
    }

    private bool _shipExploded = false;
	// Update is called once per frame
	void Update () {
        if (!_shipExploded && timer > 3) {
            _shipExploded = true;
            GetShipParts();
        }

        if (timer > 15) {
            SceneDirector.Instance.StartScreenFadeOut();

        }

        if (timer > 20) {
            Application.Quit();
        }
	}

    IEnumerator Timer() {
        while (true) {
            yield return new WaitForSecondsRealtime(1f);
            timer++;
        }
    }

    IEnumerator TriggerEffects() {
        int i = 0;
        while (true) {
            yield return new WaitForSeconds(_waitBetweenEffcects);
            if (i < _effects.Count) {

                _effects[i].SetActive(true);
                i++;
            }
            
            //yield return new WaitForSeconds(_waitBetweenEffcects);
        }
    }

    private void GetShipParts() {
        Debug.Log("xplosion");
        foreach(Transform t in _shipParts.transform) {
            //t.parent = null;
            _shipPartsList.Add(t.gameObject);
            
        }

        foreach (GameObject t in _shipPartsList) {
            //t.parent = null;
            t.gameObject.AddComponent<BoxCollider>().isTrigger = true;
            Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            //rb.mass = 10000f;
            //rb.AddExplosionForce(power, explosionPos.position, radius, 0F);
            Vector3 dir = Random.insideUnitCircle * 5;
            rb.AddForce(dir.normalized * 50);
        }
    }

    /*public float radius = 5.0F;
    public float power = 10.0F;

    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }
    */
}
