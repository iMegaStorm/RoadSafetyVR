using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int curRisk;
    [SerializeField] int maxRisk;

    ConditionManager conditionManager;
    [SerializeField] RiskBar risk;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Losing");
            curRisk = 100;
            risk.UpdateRiskBar(curRisk, maxRisk);
        }
    }
}
