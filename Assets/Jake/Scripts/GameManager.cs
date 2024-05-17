using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel = 1;

    public PlayerStats playerStats; // Reference to the PlayerStats script
    public string saveFilePath;

    private void Start()
    {

        playerStats = FindAnyObjectByType<PlayerStats>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = $"{Application.dataPath}/StatData.json";

           

            if (playerStats == null)
            {
                Debug.LogError("PlayerStats reference is null in GameManager.");
            }
            else
            {
                Debug.Log("PlayerStats reference is not null in GameManager.");
            }

            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadGameData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            currentLevel = data.currentLevel;

            // Load player stats from the save data
            playerStats.CurrentExp = data.currentXP;
            playerStats.AvailablePoints = data.availableAttributePoints;
            playerStats.HealthPoints = data.currentHealthAttribute;
            playerStats.AmmoPoints = data.currentAmmoAttribute;
            playerStats.SpeedPoints = data.currentSpeedAttribute;

            Debug.Log("Game data loaded.");
        }
        else
        {
            SaveGameData();
            Debug.Log("No save file found. New save file created.");
        }
    }

    private void SaveGameData()
    {
        GameData data = new GameData();
        data.currentLevel = currentLevel;

        // Save player stats to the save data
        data.currentXP = playerStats.CurrentExp;
        data.availableAttributePoints = playerStats.AvailablePoints;
        data.currentHealthAttribute = playerStats.HealthPoints;
        data.currentAmmoAttribute = playerStats.AmmoPoints;
        data.currentSpeedAttribute = playerStats.SpeedPoints;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);

        Debug.Log("Game data saved.");
    }

    // Call this method whenever you want to save the game data
    public void SaveGame()
    {
        SaveGameData();
    }

    // Call this method whenever you want to load the game data
    public void LoadGame()
    {
        LoadGameData();
    }

    // Set the PlayerStats reference
    public void SetPlayerStats(PlayerStats stats)
    {
        playerStats = stats;
    }
}

[System.Serializable]
public class GameData
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int availableAttributePoints = 0;
    public int currentHealthAttribute = 0;
    public int currentAmmoAttribute = 0;
    public int currentSpeedAttribute = 0;   
}
