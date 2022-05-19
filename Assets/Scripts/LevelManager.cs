using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static int currentLevelID; // Use this when we implement the map
    public static string currentLevelName;
    public static List<int> clearedLevels;

    public static void loadNewLevel(string levelName) {
        // front end stuff here
        currentLevelName = levelName;
        SceneManager.LoadScene(levelName);
    }

    public static void saveGame()
    {
        PlayerPrefs.SetInt("LevelsCompleted", Conditions.levelsCompleted);
        //PlayerPrefs.SetInt("CurrentLevelID", currentLevelID);
        PlayerPrefs.SetString("CurrentLevelName", currentLevelName);
        PlayerPrefs.SetString("ClearedLevels", string.Join("/n", clearedLevels));
    }

    public static void loadGame()
    {
        Conditions.levelsCompleted = PlayerPrefs.GetInt("LevelsCompleted");
        //currentLevelID = PlayerPrefs.GetInt("CurrentLevelID");
        currentLevelName = PlayerPrefs.GetString("CurrentLevelName");
        string[] clearedLevelsData = PlayerPrefs.GetString("ClearedLevels").Split("/n");
        for (int i = 0; i < clearedLevelsData.Length; i++)
        {
            clearedLevels.Add(int.Parse(clearedLevelsData[i]));
        }
        Debug.Log(Conditions.levelsCompleted + " " + currentLevelName + "clearedLevels");
    }
}
