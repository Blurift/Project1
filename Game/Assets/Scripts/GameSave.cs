using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameSave
{
    public Level[] Levels;

    public int CompletedLevels()
    {
        int count = 0;
        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].ConquerCompleted)
                count++;
        }
        return count;
    }

}

[System.Serializable]
public class Level
{
    public string LevelName = "";
    public int LevelsToUnlock = 0;
    public bool ConquerCompleted = false;
    public int ScoreFromSurvival = 0;
}

