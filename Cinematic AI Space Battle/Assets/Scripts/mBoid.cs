using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class mBoid : MonoBehaviour
{

//////https://github.com/skooter500/GE1-2018-2019/blob/master/GE1Examples/Assets/SwayManager1.cs

    TransformAccessArray boidTransforms;

    public NativeArray<Vector3> destinations;
    public NativeArray<Vector3> velocities;
    public float slowingDistance;
    public float maxSpeed;
    public float mass;
    public float timeDelta;

    public int maxJobs = 100000000;
    public int jobNumber = 0;

    BoidJob job;
    JobHandle jh;

//    public static mBoid Instance;

    private bool _behavioursStarted = false;

    void Update () {

        if(_behavioursStarted){
            job = new BoidJob() {
                slowingDistance = this.slowingDistance,
                maxSpeed = this.maxSpeed,
                mass = this.mass,
                timeDelta = Time.deltaTime,
                destinations = this.destinations,
                velocities = this.velocities
            };

            jh = job.Schedule(boidTransforms);
            JobHandle.ScheduleBatchedJobs();

        }
	}

    private void LateUpdate()
    {
        jh.Complete();
    }

    private void Awake(){
//        Instance = this;
        boidTransforms = new TransformAccessArray(0, -1);
        destinations = new NativeArray<Vector3>(maxJobs, Allocator.Persistent);
        velocities = new NativeArray<Vector3>(maxJobs, Allocator.Persistent);
    }


    public void AddNewBoids(GameObject boidT, Vector3 dest) {
        boidTransforms.Add(boidT.transform);
        //theta[numJobs] = 0;
        destinations[jobNumber] = dest;
        velocities[jobNumber] = Vector3.zero;
        jobNumber++;
    }

    public void StartBehaviours(){
        _behavioursStarted = true;
    }

    public void Reset() {
        jh.Complete();
        jobNumber = 0;
        boidTransforms.Dispose();
        velocities.Dispose();
        destinations.Dispose();

        boidTransforms = new TransformAccessArray(0, -1);
        destinations = new NativeArray<Vector3>(maxJobs, Allocator.Persistent);
        velocities = new NativeArray<Vector3>(maxJobs, Allocator.Persistent);
    }

    public void OnDestroy()
    {
        boidTransforms.Dispose();
        velocities.Dispose();
        destinations.Dispose();
    }

    
}

// 1. We have List<Vector3> targetPoints;
// 2. Spawn targetPoints.Count boids on random location(s)
// 3. Make all boids go to their individual target point
public struct BoidJob : IJobParallelForTransform
{
    public NativeArray<Vector3> destinations;
    public NativeArray<Vector3> velocities;
    public float slowingDistance;
    public float maxSpeed;
    public float mass;
    public float timeDelta;

    public void Execute(int i, TransformAccess t)
    {
        //////////////////////////////////////
        
        Vector3 toTarget = destinations[i] - t.localPosition;
        float dist = toTarget.magnitude;

        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / dist);
        Vector3 force = desired - velocities[i];

        var acceleration = force / mass;
        velocities[i] = velocities[i] + (acceleration * timeDelta);

        if (velocities[i].magnitude > 0.01f || true) {
//            t.localPosition += velocities[i] * timeDelta;
        }
        else {
//            t.position += new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        }

        t.localPosition = Vector3.Lerp(t.localPosition, destinations[i], timeDelta*4);
    }
}
