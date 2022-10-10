using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int score;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float curRisk;
    [SerializeField] float maxRisk;
    [SerializeField] float interactHeight;
    [SerializeField] int interactRange;
    //public float riskTimer;
    private bool isCrossing;
    public bool isOnRoad;
    int layerMask = 1 << 10;

    [Header("Camera")]
    [SerializeField] public float cameraHeight;
    [SerializeField] public float mouseSens;
    float updatedMouseSens;
    float cameraPitch = 0f;

    [Header("Components")]
    [SerializeField] Camera myCamera;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform myTransform;
    [SerializeField] CharacterController controller;
    [SerializeField] RiskBar risk;
    [SerializeField] ConditionManager conditionManager;

    public static Player instance;

    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(!conditionManager.isGameOver)
            PlayerCamera();

        if (Input.GetMouseButtonDown(0))
            Interact();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        if (isOnRoad)
        {
            Debug.Log("OnRoad");
            //riskTimer += Time.deltaTime;
            curRisk += Time.deltaTime;
            risk.UpdateRiskBar(curRisk, maxRisk);
        }
    }

    void Interact()
    {
        Vector3 temp = new Vector3(transform.position.x, interactHeight, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(temp, transform.TransformDirection(Vector3.forward), out hit, interactRange, layerMask))
        {
            //hit.transform.gameObject.GetComponent<Interactable>().ButtonTrigger();
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    void PlayerCamera()
    {
        myCamera.transform.position = myTransform.transform.position - myTransform.transform.forward; //Set the camera to localBodies forward position
        myCamera.transform.LookAt(myTransform.forward); //Look at the transform
        myCamera.transform.position = new Vector3(myTransform.position.x, cameraHeight, myTransform.position.z); //Set the camera to the local players transform (so the camera isnt behind the player)
        //Debug.Log(myTransform.position.x + " | " + myTransform.position.y + " | " + myTransform.position.z);

        //Camera settings for looking around
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= mouseDelta.y * mouseSens;
        cameraPitch = Mathf.Clamp(cameraPitch, -45f, 45f);
        cameraTransform.localEulerAngles = Vector3.right * cameraPitch;
        myTransform.Rotate(Vector3.up * mouseDelta.x * mouseSens);
    }

    void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("SafeCrossing"))
    //    {
    //        isOnRoad = false; // Safety check to make sure its off
    //        HUD.instance.loseConditionText.SetActive(false);

    //        var roadCrossing = other.GetComponent<ScoreManager>();
    //        isCrossing = true;

    //        if(roadCrossing.isScoreCollected == false)
    //        {
    //            score += 50;
    //            roadCrossing.isScoreCollected = true;
    //        }
    //        HUD.instance.winConditionText.SetActive(true);
    //        GameManager.instance.UpdateScore(score);
    //    }
    //    else if (other.CompareTag("WinCondition"))
    //    {
    //        conditionManager.SetEndGameScreen(true, score);
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("LoseCondition") && !isCrossing)
    //    {
    //        HUD.instance.loseConditionText.SetActive(true);

    //        if (curRisk >= maxRisk)
    //            conditionManager.SetEndGameScreen(false, score);

    //        if (curRisk < maxRisk)
    //            isOnRoad = true;
    //        else
    //            isOnRoad = false;

    //        //if (riskTimer > 0.5 && curRisk < maxRisk)
    //        //{
    //        //    Debug.Log("Happening");
    //        //    curRisk++;
    //        //    riskTimer = 0;
    //        //    risk.UpdateRiskBar(curRisk, maxRisk);
    //        //}
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("LoseCondition"))
    //    {
    //        //Debug.Log("Exited");
    //        isOnRoad = false;
    //        HUD.instance.loseConditionText.SetActive(false);
    //        //riskTimer = 0;
    //    }
    //    else if(other.CompareTag("SafeCrossing"))
    //    {
    //        isCrossing = false;
    //        HUD.instance.winConditionText.SetActive(false);
    //    }
    //}

    private void OnDrawGizmos()
    {
        Vector3 temp = new Vector3(transform.position.x, interactHeight, transform.position.z);
        Debug.DrawRay(temp, transform.TransformDirection(Vector3.forward) * interactRange, Color.red);
    }
}
