using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PedestrianCrossing : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] waitText;
    [SerializeField] RawImage[] greenMan;
    [SerializeField] RawImage[] redMan;
    [SerializeField] Animator buttonAnim;
    [SerializeField] TrafficLightSystem[] trafficLightSystems;
    [SerializeField] PedestrianCrossing[] trafficBoxes;
    [SerializeField] GameObject crossing;
    [SerializeField] ConditionManager conditionManager;
    public bool isPressed;

    private void Start()
    {
        //
        //greenMan.color = new Color(255, 255, 255);
        
    }

    private void Update()
    {
        if(isPressed)
        {
            VRPlayer.instance.count = 1;
            //if (trafficLightSystems[0].trafficTimer < 15)
            //{
            //    trafficLightSystems[0].trafficLightSwitch = false;
            //    trafficLightSystems[1].trafficLightSwitch = true;
            //}
            if (trafficLightSystems[0].trafficTimer > 17.5f && trafficLightSystems[0].trafficTimer <= 33f) //Lights Red
            {
                //Debug.Log("Happening");
                crossing.SetActive(true);
                //HUD.instance.winConditionText.SetActive(true);

                for (int i = 0; i < 2; i++)
                {
                    greenMan[i].color = new Color(1, 1, 1);
                    redMan[i].color = new Color(0, 0, 0);
                    waitText[i].color = new Color32(0, 0, 0, 50);
                }

                //Reset
                if (trafficLightSystems[0].trafficTimer >= 17.5f && trafficLightSystems[0].trafficTimer <= 19.5f && trafficLightSystems[0].trafficLightSwitch == true)
                {
                    //Debug.Log("Reset");
                    isPressed = false;
                }

            }
            else if(trafficLightSystems[0].trafficTimer <= 17.5f) // Not red
            {
                for(int i = 0; i < 2; i++)
                {
                    //Debug.Log("Else if Happening");
                    redMan[i].color = new Color(1, 1, 1);
                    waitText[i].color = new Color32(217, 210, 85, 255);
                }
            }
        }
        else
        {
            //Debug.Log("Else " + trafficLightSystems[0].trafficTimer);
            Debug.Log("Else Called");
            crossing.SetActive(false);
            if (VRPlayer.instance.count == 1 && !isPressed)
            {
                Debug.Log("If Called");
                HUD.instance.winConditionText.SetActive(false);
                VRPlayer.instance.isCrossing = false;
                VRPlayer.instance.count = 0;
            }

            for (int i = 0; i < 2; i++)
            {
                redMan[i].color = new Color(0, 0, 0);
                greenMan[i].color = new Color(0, 0, 0);
                waitText[i].color = new Color32(0, 0, 0, 50);
            }
        }
    }

    public void CrossingSystem()
    {
        //buttonAnim.SetTrigger("Button");
        for (int i = 0; i < 2; i++)
        {
            trafficBoxes[i].isPressed = true;
        }
        StartCoroutine(Crossing());
    }

    IEnumerator Crossing()
    {
        if (trafficLightSystems[0].trafficTimer < 15 && trafficLightSystems[0].trafficLightSwitch == true) // If its a green light, wait before switching
        {
            Debug.Log("Waiting 2 seconds");

            yield return new WaitForSeconds(2);
            trafficLightSystems[0].trafficLightSwitch = false; //Increase time
            trafficLightSystems[1].trafficLightSwitch = false; //Increase time
            trafficLightSystems[2].trafficLightSwitch = true; //Decrease time
            trafficLightSystems[3].trafficLightSwitch = true; //Decrease time
        }
        else if (trafficLightSystems[0].trafficTimer > 15.5f && trafficLightSystems[0].trafficTimer <= 17f && trafficLightSystems[0].trafficLightSwitch == true) //Lights Amber
        {
            Debug.Log("Waiting 6 seconds");

            yield return new WaitForSeconds(6);
            trafficLightSystems[0].trafficLightSwitch = false; //Increase time
            trafficLightSystems[1].trafficLightSwitch = false; //Increase time
            trafficLightSystems[2].trafficLightSwitch = true; //Decrease time
            trafficLightSystems[3].trafficLightSwitch = true; //Decrease time
        }
        else if (trafficLightSystems[0].trafficTimer > 17.5f && trafficLightSystems[0].trafficTimer <= 32.5f && trafficLightSystems[0].trafficLightSwitch == true) //Lights Red
        {

            Debug.Log("Changing");

            trafficLightSystems[0].trafficLightSwitch = false; //Increase time
            trafficLightSystems[1].trafficLightSwitch = false; //Increase time
            trafficLightSystems[2].trafficLightSwitch = true; //Decrease time
            trafficLightSystems[3].trafficLightSwitch = true; //Decrease time
        }

    }


}
