using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    public HealthScript healthScript;
    public Image healthBar;

    private void OnEnable()
    {
        healthScript.onHealthChange += UpdateUI;
    }

    private void OnDisable()
    {
        healthScript.onHealthChange -= UpdateUI;
    }

    public void UpdateUI(bool headShot,float health)
    {
        healthBar.fillAmount = health / 100;
    }
}
