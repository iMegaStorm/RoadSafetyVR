using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int speed;
    //public List<Transform> waypoints = new List<Transform>();

    public int waypointIndex;
    private float dist;
    public bool spawnedInCar;

    void Start()
    {
        var randomWaypointPath = Random.Range(0, 10);
        //Debug.Log(randomWaypointPath);

        if(randomWaypointPath < 6 && !spawnedInCar)
        {
            waypoints = new Transform[gameObject.GetComponentInParent<SpawnPointPatrol>().waypoints.Length]; //Sets the cars array to the size of the spawnpoint array
                                                                                                             //Loops through the array to match the spawnpoint array
            for (var i = 0; i < gameObject.GetComponentInParent<SpawnPointPatrol>().waypoints.Length; i++)
            {
                //Debug.Log(GetComponentInParent<SpawnPointPatrol>().waypoints[i]);
                waypoints[i] = gameObject.GetComponentInParent<SpawnPointPatrol>().waypoints[i];
            }
        }
        else if (!spawnedInCar)
        {
            waypoints = new Transform[gameObject.GetComponentInParent<SpawnPointPatrol>().waypoints2.Length]; //Sets the cars array to the size of the spawnpoint array
                                                                                                              //Loops through the array to match the spawnpoint array
            for (var i = 0; i < gameObject.GetComponentInParent<SpawnPointPatrol>().waypoints2.Length; i++)
            {
                //Debug.Log(GetComponentInParent<SpawnPointPatrol>().waypoints[i]);
                waypoints[i] = gameObject.GetComponentInParent<SpawnPointPatrol>().waypoints2[i];
            }
        }



        if (waypoints == null)
            return;

        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex >= waypoints.Length)
            return;

        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
        PatrolAI();
    }

    void PatrolAI()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;

        if (waypointIndex >= waypoints.Length)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        transform.LookAt(waypoints[waypointIndex].position);
    }
}

