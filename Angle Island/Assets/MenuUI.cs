using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public delegate void OnPlayGame();
    public static OnPlayGame PlayGame;

    public delegate void OnQuitGame();
    public static OnQuitGame QuitGame;

    public void Play()
    {
        PlayGame();
    }

    public void Quit()
    {
        QuitGame();
    }
}
