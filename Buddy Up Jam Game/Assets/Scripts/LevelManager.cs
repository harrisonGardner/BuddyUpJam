using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static int currentLevel = 0;
    public static bool messagesRead = false;

    public static int amountOfLevels = 3;

    public static void SetLevel(int level)
    {
        currentLevel = level;
    }

    public static int GetLevel()
    {
        return currentLevel;
    }
}
