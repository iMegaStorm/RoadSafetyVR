using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("Components")]
    public GameObject winConditionText;
    public GameObject loseConditionText;

    public static HUD instance;

    private void Awake()
    {
        instance = this;
    }

}
