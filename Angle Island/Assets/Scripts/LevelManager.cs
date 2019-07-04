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
    LevelData actualLevelData;

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
        }
    }

    private void Start()
    {
        LevelData.ChargeLevel=StartNewLevel;
    }

    void StartNewLevel()
    {
        actualLevelData = GameObject.Find("LevelData").GetComponent<LevelData>();
        actualLevel = actualLevelData.level;
        nextLevel = actualLevelData.nextLevel;
        prevLevel = actualLevelData.previousLevel;
        actualLevelName = actualLevelData.name;
        actualLevelThatComesFrom = actualLevelData.levelThatComesFrom;
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
        StartNewLevel();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        actualLevelData.levelThatComesFrom = savedLevelThatComesFrom;
        StartNewLevel();
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
