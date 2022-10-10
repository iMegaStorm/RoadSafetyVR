using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficLightSystem : MonoBehaviour
{
    [Header("Traffic Conditions")]
    [Tooltip(" 0/15 = Green, 17.5/32.5 = Red")]
    public float trafficTimer;
    [Tooltip("Unticked = Increase in time, Ticked = Decrease in time")]
    public bool trafficLightSwitch;
    public bool preparingToStop;

    [Header("Components")]
    [SerializeField] Material[] lightMaterials;
    [SerializeField] GameObject[] lights;

    void Update()
    {
        //Traffic Logic
        TrafficLights();

        //trafficTimer condition toggle
        if(trafficLightSwitch == false)
            trafficTimer += Time.deltaTime;
        else
            trafficTimer -= Time.deltaTime;
    }

    void TrafficLights()
    {
        if (trafficTimer > 0f && trafficTimer <= 15f) //Lights Green
        {
            preparingToStop = false;
            lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
            lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
            lights[2].GetComponent<MeshRenderer>().material = lightMaterials[2];
        }
        else if (trafficTimer > 15.5f && trafficTimer <= 17f) //Lights Amber
        {
            //Statement to control the oncoming cars actions
            if (trafficLightSwitch == true)
                preparingToStop = false;
            else
                preparingToStop = true;

            lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
            lights[1].GetComponent<MeshRenderer>().material = lightMaterials[1];
            lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
        }
        else if (trafficTimer > 17.5f && trafficTimer <= 32.5f) //Lights Red
        {
            preparingToStop = true;
            lights[0].GetComponent<MeshRenderer>().material = lightMaterials[0];
            lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
            lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
        }
        else if (trafficTimer > 32.5) //Switch statement to control the trafficTimer direction
        {
            trafficLightSwitch = true;
        }
        else if (trafficTimer < 0) //Switch statement to control the trafficTimer direction
        {
            trafficLightSwitch = false;
        }
        else //Lights Off
        {
            lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
            lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
            lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
        }
    }
}

//else
//{
//    trafficTimer -= Time.deltaTime;

//    if(trafficTimer <= 62.5f && trafficTimer > 32.5f) //Red
//    {
//        lights[0].GetComponent<MeshRenderer>().material = lightMaterials[0];
//        lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
//        lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    }
//    else if (trafficTimer < 32f && trafficTimer > 30.5f) //Amber
//    {
//        lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
//        lights[1].GetComponent<MeshRenderer>().material = lightMaterials[1];
//        lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    }
//    else if (trafficTimer < 30f && trafficTimer > 0f) //Green
//    {
//        lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
//        lights[1].GetComponent<MeshRenderer>().material = lightMaterials[1];
//        lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    }
//    else if(trafficTimer <= 0f)
//    {
//        cycleLights = false;
//    }
//    else // Off
//    {
//        lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
//        lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
//        lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    }
//}

//if (trafficTimer <= 90f && trafficTimer > 60f) // Red
//{
//    lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    lights[2].GetComponent<MeshRenderer>().material = lightMaterials[2];
//}
//else if (trafficTimer <= 60f && trafficTimer > 30f) // Amber
//{
//    lights[0].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    lights[1].GetComponent<MeshRenderer>().material = lightMaterials[1];
//    lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
//}
//else if (trafficTimer <= 30 && trafficTimer > 0f) //Green
//{
//    lights[0].GetComponent<MeshRenderer>().material = lightMaterials[0];
//    lights[1].GetComponent<MeshRenderer>().material = lightMaterials[3];
//    lights[2].GetComponent<MeshRenderer>().material = lightMaterials[3];
//}
//else
//{
//    trafficTimer = startingTimer;
//}

