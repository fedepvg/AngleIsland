using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public delegate void OnLevelDataCharge();
    public static OnLevelDataCharge ChargeLevel;

    public int level;
    new public string name;

    public int nextLevel;
    public int previousLevel;

    public int levelThatComesFrom;

    private void Start()
    {
        ChargeLevel();
    }

}
