using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ButtonInteraction"))
        {
            other.GetComponent<Interactable>().UpdateBool();
        }
    }
}
