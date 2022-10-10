using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCrossing : MonoBehaviour
{
    public bool waitingToCross;
    public bool carCrossing;
    public Collider myCollider;

    public bool CrossingBool()
    {
        //Debug.Log("Waiting to cross " + waitingToCross);
        return waitingToCross;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Car" && waitingToCross == false)
        {
            carCrossing = true;
        }
        else if (other.tag == "Player" && carCrossing == false)
        {
            waitingToCross = true;
            //Debug.Log("Waiting");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
        {
            carCrossing = false;
            //Debug.Log("Car has left");
        }
        else if (other.tag == "Player")
        {
            waitingToCross = false;
            //Debug.Log("Waiting");
        }
    }
}
