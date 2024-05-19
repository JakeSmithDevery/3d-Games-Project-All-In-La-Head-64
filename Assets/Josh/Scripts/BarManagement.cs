using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class BarManagement : MonoBehaviour
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
        float fillAmount = (float)PlayerStats.CurrentExp / (float)PlayerStats.neededExp;
        mask.fillAmount = fillAmount;
    }

}
