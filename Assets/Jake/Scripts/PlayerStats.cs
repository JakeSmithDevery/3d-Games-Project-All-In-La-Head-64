using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int PlayerLevel = 1;
    public int CurrentExp = 0;
    public int AvailablePoints = 0;

    public int HealthPoints = 0;

    public int AmmoPoints = 0;

    public int SpeedPoints = 0;

    public int neededExp = 100;



    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void HandleExpChange(int newExperiance)
    {
        CurrentExp += newExperiance;
        if (CurrentExp >= neededExp) 
        {
            LevelUp();
        
        }
    }

    public void LevelUp()
    {
        AvailablePoints++;

        PlayerLevel++;

        CurrentExp = 0;
        neededExp += 100;
    }

    // Update is called once per frame
    private void Update()
    {

        
    }
}
