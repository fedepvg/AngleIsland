using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int actualLevel;
    int nextLevel;
    int prevLevel;
    string actualLevelName;
    int actualLevelThatComesFrom;
    public LevelData actualLevelData;

    int savedLevelThatComesFrom;

    public static LevelManager instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            LevelData.ChargeLevel = StartNewLevel;
        }
    }

    void StartNewLevel(LevelData levData)
    {
        actualLevelData = levData;
        actualLevel = levData.level;
        nextLevel = levData.nextLevel;
        prevLevel = levData.previousLevel;
        actualLevelName = levData.name;
        actualLevelThatComesFrom = levData.levelThatComesFrom;
    }

    public int GetActualLevel()
    {
        return actualLevel;
    }

    public int GetLevelThatComesFrom()
    {
        return actualLevelThatComesFrom;
    }

    void Update()
    {

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        actualLevelData.levelThatComesFrom = savedLevelThatComesFrom;
        savedLevelThatComesFrom = actualLevel;
    }

    public void GoToNextLevel()
    {
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
