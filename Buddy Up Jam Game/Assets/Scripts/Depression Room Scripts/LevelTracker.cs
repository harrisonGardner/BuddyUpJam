using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelTracker
{
    private static int currentLevel = 0;

    public static void SetLevel(int level)
    {
        currentLevel = level;
    }

    public static int GetLevel()
    {
        return currentLevel;
    }
}
