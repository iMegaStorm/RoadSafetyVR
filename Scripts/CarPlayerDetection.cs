using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerDetection : MonoBehaviour
{
    [SerializeField] CarMovement carMovement;
    [SerializeField] AudioSource audioSource;
    float timer = 5;

    private void Update()
    {
        if(carMovement.personInDanger)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        VRPlayer vrPlayer = other.GetComponent<VRPlayer>();
        if (other.CompareTag("Player") && vrPlayer.isOnRoad)
        {
            carMovement.Decceleration();
            carMovement.personInDanger = true;

            if (timer >= 5 && !audioSource.isPlaying)
            {
                audioSource.Play();
                timer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            carMovement.personInDanger = false;
        }
    }
}
