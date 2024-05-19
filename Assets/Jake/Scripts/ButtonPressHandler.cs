using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPressHandler : MonoBehaviour
{
    public Button StartButton;
    public Button AddAmmo;
    public Button AddHealth;
    public Button AddSpeed;

    public PlayerStats PlayerStats;
    public GameManager GameManager;
    void Start()
    {
        PlayerStats = FindAnyObjectByType<PlayerStats>();
        GameManager = FindAnyObjectByType<GameManager>();
        // Make sure to assign the Button component in the Inspector
        if (StartButton == null)
        {
            Debug.LogError("Button reference is missing!");
            return;
        }

        // Add a listener to call the OnButtonPressed method when the button is clicked
        StartButton.onClick.AddListener(OnStartButtonPressed);
        AddAmmo.onClick.AddListener(OnAddAmmoPressed);
        AddHealth.onClick.AddListener(OnAddHealthPressed);
        AddSpeed.onClick.AddListener(OnAddHSpeedPressed);

    }

    void OnStartButtonPressed()
    {
        // Code to execute when the button is pressed
        SceneManager.LoadScene("MapDemoScene");
        Debug.Log("Button was pressed!");
    }

    void OnAddHealthPressed()
    {
        if (PlayerStats.AvailablePoints > 0) 
        {
            PlayerStats.AvailablePoints--;

            PlayerStats.HealthPoints++;

            GameManager.SaveGame();
        }
    }

    void OnAddAmmoPressed()
    {
        if (PlayerStats.AvailablePoints > 0)
        {
            PlayerStats.AvailablePoints--;

            PlayerStats.AmmoPoints++;

            GameManager.SaveGame();
        }
    }

    void OnAddHSpeedPressed()
    {
        if (PlayerStats.AvailablePoints > 0)
        {
            PlayerStats.AvailablePoints--;

            PlayerStats.SpeedPoints++;

            GameManager.SaveGame();
        }
    }
}
