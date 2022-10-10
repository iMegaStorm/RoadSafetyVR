using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiskBar : MonoBehaviour
{
    [SerializeField] private Image riskBarFill;

    public void UpdateRiskBar(float _riskBarSize, float _maxRisk)
    {
        riskBarFill.fillAmount = (float)_riskBarSize / _maxRisk;
    }
}
