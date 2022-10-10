using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed;
    public float maxSpeed;
    public float curSpeed;
    public int range;
    public bool carIsStopped = false;
    public bool crossing;
    public bool isParked;
    public bool personInDanger;

    [Header("Other")]
    public LayerMask crossingLayerMask;
    public RoadCrossing roadCrossing;
    int layerMask = 1 << 7;
    Player player;
    VRPlayer vrPlayer;
    //public float test;
    //public Vector3 startPos;
    //public Quaternion startRotation;
    private Animator anim;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        vrPlayer = GameObject.FindObjectOfType<VRPlayer>();
        anim = GetComponent<Animator>();
        //startPos = transform.position;
        //startRotation = transform.parent.rotation;
        //startRotation = gameObject.GetComponent<SpawnSystem>().transform.rotation;
        //startRotation = 0;
        //Debug.Log(transform.parent.rotation);
    }

    private void Update()
    {
        if(carIsStopped)
            curSpeed = 0;

        if (!isParked)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * range, out hit, range, layerMask))
            {
                Decceleration();
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * range, out hit, range, crossingLayerMask)) //Public Crossing
            {
                crossing = hit.transform.gameObject.GetComponent<RoadCrossing>().CrossingBool();

                if (crossing == true)
                    Decceleration();
                else
                    Acceleration();
            }
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * range, out hit, range, 1 << 9)) //Traffic Crossing
            {
                bool trafficCrossing = hit.transform.gameObject.GetComponent<TrafficLightSystem>().preparingToStop;

                if (trafficCrossing == true)
                {
                    Decceleration();
                }
                else
                {
                    Acceleration();
                }
            }
            else
            {
                Acceleration();
            }
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.yellow);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * curSpeed * Time.deltaTime);


    }

    void Acceleration()
    {
        carIsStopped = false;
        anim.SetBool("isMoving", true);
        //Debug.Log(anim.speed);
        if (curSpeed < maxSpeed && !personInDanger)
        {
            curSpeed += Time.deltaTime;
        }
    }

    public void Decceleration()
    {        
        if (curSpeed > 5 && carIsStopped == false) //Apply more decceleration to cars going faster
        {
            curSpeed -= Time.deltaTime * 30; //was 7
        }
        else if (curSpeed > minSpeed && carIsStopped == false) //Normal amount of decceleration
        {
            curSpeed -= Time.deltaTime * 20; //was 7
        }
        else if(curSpeed <= 0) //If car speed is less than or equal to 0, set the speed to 0 and car has stopped
        {
            anim.SetBool("isMoving", false);
            curSpeed = 0;
            carIsStopped = true;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "RespawnSystem")
    //    {
    //        //transform.position = startPos;
    //        //transform.rotation = startRotation;
    //        //gameObject.GetComponent<Patrol>().waypointIndex = 0;
    //        GameObject.Destroy(gameObject);
    //    }


    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "PublicCrossing")
    //    {
    //        //roadCrossing = other.gameObject.GetComponent<RoadCrossing>();
    //        crossing = other.gameObject.GetComponent<RoadCrossing>().CrossingBool();
    //        if (crossing == true)
    //        {
    //            Decceleration();
    //            //curSpeed = 0;
    //            Debug.Log("Decceleration");
    //        }
    //        else
    //        {
    //            carIsStopped = false;
    //            Acceleration();
    //            Debug.Log("Acceleration");
    //        }
    //    }
    //    else if (other.tag == "TrafficCrossing")
    //    {
    //        bool trafficCrossing = other.gameObject.GetComponent<TrafficLightSystem>().preparingToStop;
    //        if (trafficCrossing == true)
    //            Decceleration();
    //    }
    //}
}
