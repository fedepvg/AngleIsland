using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public delegate void OnPlayAgain();
    public static OnPlayAgain PlayAgain;

    public delegate void OnQuitGame();
    public static OnQuitGame QuitGame;

    public void Play()
    {
        PlayAgain();
    }

    public void Quit()
    {
        QuitGame();
    }
}
