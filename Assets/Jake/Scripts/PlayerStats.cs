using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    int PlayerLevel = 1;
    
    [SerializeField]
    int CurrentExp = 0;
    [SerializeField]
    int AvailablePoints = 0;
    [SerializeField]
    int HealthPoints = 0;
    [SerializeField]
    int ManaPoints = 0;
    [SerializeField]
    int SpeedPoints = 0;
    [SerializeField]
    int neededExp = 100;

    

    // Update is called once per frame
    void Update()
    {
        if (CurrentExp >= neededExp)
        {
            PlayerLevel += 1;

            AvailablePoints += 1;

            neededExp = PlayerLevel * 100;
        }
        
    }
}
