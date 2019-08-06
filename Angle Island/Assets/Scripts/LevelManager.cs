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

    void Start()
    {
        MenuUI.PlayGame = GoToNextLevel;
        MenuUI.QuitGame = QuitGame;
        UIInGame.ReturnToMenu = GoToMenu;
        UIInGame.QuitGame = QuitGame;
        UIGameOver.PlayAgain = GoToNextLevel;
        UIGameOver.QuitGame = QuitGame;
        SpaceShip.GameEnd = GoToGameOver;
    }

    void StartNewLevel(LevelData levData)
    {
        actualLevelData = levData;
        actualLevel = levData.level;
        nextLevel = levData.nextLevel;
        prevLevel = levData.previousLevel;
        actualLevelName = levData.name;
        actualLevelThatComesFrom = savedLevelThatComesFrom;
        if(actualLevel==3)
        {
            nextLevel = savedLevelThatComesFrom;
        }
    }

    public int GetActualLevel()
    {
        return actualLevel;
    }

    public int GetLevelThatComesFrom()
    {
        return actualLevelThatComesFrom;
    }

    void GoToGameOver()
    {
        nextLevel = 3;
        GoToNextLevel();
    }

    public void GoToMenu()
    {
        nextLevel = 0;
        GoToNextLevel();
    }

    public void GoToNextLevel()
    {
        savedLevelThatComesFrom = actualLevel;
        SceneManager.LoadScene(nextLevel);
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
