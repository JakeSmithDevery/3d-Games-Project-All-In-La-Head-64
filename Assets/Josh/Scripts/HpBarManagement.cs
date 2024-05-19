using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManagement : MonoBehaviour
{
    public PlayerLocomotion PlayerLocomotion;
    public PlayerStats PlayerStats;
    public Image mask;

    void Start()
    {
        PlayerLocomotion = FindAnyObjectByType<PlayerLocomotion>();
        PlayerStats = FindAnyObjectByType<PlayerStats>();
    }


    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)PlayerLocomotion.Health / (float)PlayerLocomotion.MaxHealth;
        mask.fillAmount = fillAmount;
    }
}
