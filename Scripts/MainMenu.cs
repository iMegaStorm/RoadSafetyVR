using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject instructionMenu;
    [SerializeField] GameObject mapMenu;
    public bool isMainMenu;
    public bool isInstructionMenu;
    public bool isMapMenu;
    public InputDevice primaryButton;
    public InputDevice backButton;
    public bool primaryButtonPressed;
    public bool secondaryButtonPressed;
    public int count;

    private bool forwardTrigger;
    public bool forwards;

    private bool backwardTrigger;
    public bool backwards;
    
    public float timer;

    private void Start()
    {
        //isMainMenu = true;
        //isInstructionMenu = false;
        //isMapMenu = false;

        //var inputDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        //foreach (var device in inputDevices)
        //{
        //    Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));

        //}

        //var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        //if (rightHandDevices.Count == 1)
        //{
        //    UnityEngine.XR.InputDevice device = rightHandDevices[0];
        //    Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));
        //}
        //else if (rightHandDevices.Count > 1)
        //{
        //    Debug.Log("Found more than one left hand!");
        //}

        //List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);

        //InputDeviceCharacteristics rightController = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        //InputDevices.GetDevicesWithCharacteristics(rightController, devices);

        //foreach (var item in devices)
        //{
        //    Debug.Log(item.name + item.characteristics);
        //}

        //if (devices.Count > 0)
        //{
        //    primaryButton = devices[0];
        //    backButton = devices[0];
        //}
    }

    private void Update()
    {
        //var gamepad = Gamepad.current;
        //if (gamepad == null)
        //    return;
        //primaryButton.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        //backButton.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue);

        //if (primaryButtonValue == true && primaryButtonPressed == false && secondaryButtonPressed == false)
        //{
        //    primaryButtonPressed = true;
        //}
        //else if (secondaryButtonValue == true && primaryButtonPressed == false && secondaryButtonPressed == false)
        //{
        //    secondaryButtonPressed = true;
        //}

        //if(primaryButtonPressed)
        //{
        //    timer += Time.deltaTime;
        //}
        //else if (secondaryButtonPressed)
        //{
        //    timer -= Time.deltaTime;
        //}


        //XR.inp
        //Debug.Log(primaryButtonValue);
        //if (isMainMenu && !isInstructionMenu && !isMapMenu)
        //{
        //    if (timer >= 0.5f)
        //    {

        //        Debug.Log("Called " + timer);
        //        timer = 0;
        //        primaryButtonValue = false;
        //        primaryButtonPressed = false;
        //        OnPlay();
        //        Invoke("OnPlay", 0.3f);
        //    }
        //    else if (timer <= -0.5f)
        //    {
        //        timer = 0;
        //        secondaryButtonPressed = false;
        //        OnQuit();
        //    }
        //}
        //else if (!isMainMenu && isInstructionMenu && !isMapMenu)
        //{
        //    if (timer >= 0.5f)
        //    {
        //        timer = 0;
        //        primaryButtonValue = false;
        //        primaryButtonPressed = false;
        //        OnNext();
        //        Invoke("OnNext", 0.3f);
        //    }
        //    else if (timer <= -0.5f)
        //    {
        //        timer = 0;
        //        secondaryButtonPressed = false;
        //        OnInstructionsBack();
        //    }
        //}
        //else if (!isMainMenu && !isInstructionMenu && isMapMenu)
        //{
        //    if (timer >= 0.5f)
        //    {
        //        timer = 0;
        //        primaryButtonValue = false;
        //        primaryButtonPressed = false;
        //        OnStartGame();
        //        Invoke("OnStartGame", 0.3f);
        //    }
        //    else if (timer <= -0.5f)
        //    {
        //        timer = 0;
        //        secondaryButtonPressed = false;
        //        OnMapBack();
        //    }
        //}

        if(forwardTrigger && !backwardTrigger)
        {
            forwards = true;
            timer += Time.deltaTime;
        }
        else if (backwardTrigger && !forwardTrigger)
        {
            backwards = true;
            timer -= Time.deltaTime;
        }
        else
        {
            forwardTrigger = false;
            backwardTrigger = false;
            timer = 0;
        }

        if (timer >= 0.5f)
        {
            timer = 0;
            count += 1;
            forwardTrigger = false;

        }
        else if (timer <= -0.5f)
        {
            timer = 0;
            count -= 1;
            backwardTrigger = false;
        }

        if (count == 1 && forwards) //Instructions Menu
        {
            mainMenu.SetActive(false);
            instructionMenu.SetActive(true);
            //isMainMenu = false;
            //isInstructionMenu = true;
            //isMapMenu = false;
            forwards = false;
        }
        else if (count == 2 && forwards) //Map Menu
        {
            instructionMenu.SetActive(false);
            mapMenu.SetActive(true);
            //isMainMenu = false;
            //isInstructionMenu = false;
            //isMapMenu = true;
            forwards = false;
        }
        else if (count == 3) //Go to Game
        {
            SceneManager.LoadScene(1);
        }

        if (count == -1 && backwards)
        {
            Debug.Log("Quit");
            Application.Quit();
            count = 0;
            backwards = false;
        }

        if (count == 0 && backwards)
        {
            //isMainMenu = true;
            //isInstructionMenu = false;
            //isMapMenu = false;
            instructionMenu.SetActive(false);
            mainMenu.SetActive(true);
            backwards = false;
        }
        else if (count == 1 && backwards)
        {
            //isMainMenu = false;
            //isInstructionMenu = true;
            //isMapMenu = false;
            mapMenu.SetActive(false);
            instructionMenu.SetActive(true);
            backwards = false;
        }
    }

    public void Forward()
    {
        forwardTrigger = true;
        forwards = true;
    }

    public void Backwards()
    {
        backwardTrigger = true;
        backwards = true;
    }

    //public void OnPlay()
    //{
    //    //Debug.Log("OnPlay");
    //    timer = 0f;
    //    isMainMenu = false;
    //    isInstructionMenu = true;
    //    isMapMenu = false;
    //    mainMenu.SetActive(false);
    //    instructionMenu.SetActive(true);
    //}

    //public void OnInstructionsBack()
    //{
    //    //Debug.Log("OnInstructionsBack");
    //    timer = 0f;
    //    isMainMenu = true;
    //    isInstructionMenu = false;
    //    isMapMenu = false;
    //    instructionMenu.SetActive(false);
    //    mainMenu.SetActive(true);
    //}

    //public void OnNext()
    //{
    //    //Debug.Log("OnNext");
    //    timer = 0f;
    //    isMainMenu = false;
    //    isInstructionMenu = false;
    //    isMapMenu = true;
    //    instructionMenu.SetActive(false);
    //    mapMenu.SetActive(true);
    //}

    //public void OnMapBack()
    //{
    //    //Debug.Log("OnMapBack");
    //    timer = 0f;
    //    isMainMenu = false;
    //    isInstructionMenu = true;
    //    isMapMenu = false;
    //    mapMenu.SetActive(false);
    //    instructionMenu.SetActive(true);
    //}

    //public void OnStartGame()
    //{
    //    //Debug.Log("OnStartGame");
    //    SceneManager.LoadScene(1);
    //}

    //public void OnQuit()
    //{
    //    Debug.Log("OnQuit");
    //    Application.Quit();
    //}
}
