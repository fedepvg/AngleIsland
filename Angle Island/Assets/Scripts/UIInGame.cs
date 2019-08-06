using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    public delegate void OnGoToMenu();
    public static OnGoToMenu ReturnToMenu;

    public delegate void OnQuitGame();
    public static OnQuitGame QuitGame;

    public GameObject PausePanel;

    private void Start()
    {
        CommandTerminal.Terminal.Shell.AddCommand("pause", PauseGame, 0, 0, "Enter Pause Menu");
        CommandTerminal.Terminal.Shell.AddCommand("continue", ContinueGame, 0, 0, "Close Pause Menu");
        CommandTerminal.Terminal.Shell.AddCommand("menu", GoToMenu, 0, 0, "Go To Menu");
    }

    public void GoToMenu(CommandTerminal.CommandArg[] args)
    {
        ReturnToMenu();
    }

    public void Quit()
    {
        QuitGame();
    }

    public void PauseGame(CommandTerminal.CommandArg[] args)
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame(CommandTerminal.CommandArg[] args)
    {
        PausePanel.SetActive(false);
        CommandTerminal.Terminal.Buffer.Clear();
        Time.timeScale = 1;
    }
}
