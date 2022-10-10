using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public int score;
    public InputDevice quitButton;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        InputDeviceCharacteristics rightController = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightController, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            quitButton = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        quitButton.TryGetFeatureValue(CommonUsages.menuButton, out bool quitButtonValue);
        if (quitButtonValue)
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    // updates the score text to show the current score
    public void UpdateScore(int _score)
    {
        score = _score;
    }
}
