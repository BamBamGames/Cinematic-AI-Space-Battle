using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasive : SteeringBehaviour {

    public float distance = 15.0f;
    public float radius = 10;
    public float jitter = 100;

    Vector3 target;
    Vector3 worldTarget;

    public EvasiveType evasiveType = EvasiveType.Stock;

    public enum EvasiveType{
        Stock,
        Random
    }

    public void OnDrawGizmos() {
        Vector3 localCP = Vector3.forward * distance;
        Vector3 worldCP = transform.TransformPoint(localCP);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, worldCP);
        Gizmos.DrawWireSphere(worldCP, radius);

        Vector3 localTarget = (Vector3.forward * distance) + target;
        worldTarget = transform.TransformPoint(localTarget);

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(worldTarget, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, worldTarget);

    }

    Vector3 _disp = Vector3.zero;
    public int intervalInSecondsManuever = 10;
    [Range(0, 100)]
    public int _randomTimeSecsMax;

    public override Vector3 Calculate() {
        //Vector3 disp = jitter * Random.insideUnitSphere * Time.deltaTime;
        target += _disp;
        target.Normalize();
        target *= radius;

        Vector3 localTarget = (Vector3.forward * distance) + target;

        worldTarget = transform.TransformPoint(localTarget);
        return worldTarget - transform.position;

    }

    IEnumerator EvasiveManuevers() {

        while (true) {
            if (evasiveType == EvasiveType.Stock) {
                _disp = jitter * Random.insideUnitSphere * Time.deltaTime;
                yield return new WaitForSeconds(intervalInSecondsManuever);
            } else if (evasiveType == EvasiveType.Random) {
                int randomTime = Random.Range(0, _randomTimeSecsMax);
                _disp = jitter * Random.insideUnitSphere * Time.deltaTime;
                yield return new WaitForSeconds(randomTime);
            }
           
        }
    }

    // Start is called before the first frame update
    void Start() {
        target = Random.insideUnitSphere * radius;
        StartCoroutine(EvasiveManuevers());
    }

    public void StartEvasiveManuevers(int interval) {
        intervalInSecondsManuever = interval;
        StartCoroutine(EvasiveManuevers());
    }

    public void ChooseDirectionOnce() {
        StopAllCoroutines();
    }

    
}
