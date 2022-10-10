using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int score;
    [SerializeField] float curRisk;
    [SerializeField] float maxRisk;
    [SerializeField] float interactHeight;
    [SerializeField] int interactRange;
    //public float riskTimer;
    public bool isCrossing;
    public bool isOnRoad;
    public int count = 0;


    [Header("Components")]
    [SerializeField] RiskBar risk;
    [SerializeField] ConditionManager conditionManager;

    public static VRPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (isOnRoad)
        {
            //riskTimer += Time.deltaTime;
            curRisk += Time.deltaTime;
            risk.UpdateRiskBar(curRisk, maxRisk);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeCrossing"))
        {
            isOnRoad = false; // Safety check to make sure its off
            HUD.instance.loseConditionText.SetActive(false);

            var roadCrossing = other.GetComponent<ScoreManager>();
            isCrossing = true;

            if (roadCrossing.isScoreCollected == false)
            {
                score += 50;
                roadCrossing.isScoreCollected = true;
            }
            HUD.instance.winConditionText.SetActive(true);
            GameManager.instance.UpdateScore(score);
        }
        else if (other.CompareTag("WinCondition"))
        {
            conditionManager.SetEndGameScreen(true, score);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TempCrossing"))
        {
            Debug.Log("Stay");
            isOnRoad = false; // Safety check to make sure its off
            isCrossing = true;
            HUD.instance.loseConditionText.SetActive(false);

            var roadCrossing = other.GetComponent<ScoreManager>();
            if (roadCrossing.isScoreCollected == false)
            {
                score += 50;
                roadCrossing.isScoreCollected = true;
            }
            HUD.instance.winConditionText.SetActive(true);
            GameManager.instance.UpdateScore(score);
        }
        else if (other.CompareTag("LoseCondition") && !isCrossing)
        {
            HUD.instance.loseConditionText.SetActive(true);

            if (curRisk >= maxRisk)
                conditionManager.SetEndGameScreen(false, score);

            if (curRisk < maxRisk)
                isOnRoad = true;
            else
                isOnRoad = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LoseCondition"))
        {
            //Debug.Log("Exited");
            isOnRoad = false;
            HUD.instance.loseConditionText.SetActive(false);
            //riskTimer = 0;
        }
        else if (other.CompareTag("TempCrossing"))
        {
            Debug.Log("Exit");
            HUD.instance.winConditionText.SetActive(false);
            isOnRoad = false; // Safety check to make sure its off
            isCrossing = false;
        }
        else if (other.CompareTag("SafeCrossing"))
        {
            Debug.Log(" SafeCrossing Exit");
            isCrossing = false;
            HUD.instance.winConditionText.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 temp = new Vector3(transform.position.x, interactHeight, transform.position.z);
        Debug.DrawRay(temp, transform.TransformDirection(Vector3.forward) * interactRange, Color.red);
    }
}
