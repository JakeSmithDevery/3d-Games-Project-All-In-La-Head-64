using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{

    public int PlayerLevel = 1;
    public int CurrentExp = 0;
    public int AvailablePoints = 0;

    public int HealthPoints = 0;

    public int AmmoPoints = 0;

    public int SpeedPoints = 0;

    public int neededExp = 100;



    // Update is called once per frame
    private void Update()
    {
        
    }
}
