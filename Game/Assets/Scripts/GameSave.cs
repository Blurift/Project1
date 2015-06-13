using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    //The default savestate when first opening a game or resetting save data.
    public static GameSave Default()
    {
        GameSave gs = new GameSave();

        Level level1 = new Level() { LevelName = "Streets", LevelsToUnlock = 0 };

        gs.Levels = new Level[]{level1,
            level1,level1,level1,level1,level1,level1,level1};

        return gs;
    }
}

[System.Serializable]
public class Level
{
    [SerializeField]
    public string LevelName = "";
    [SerializeField]
    public int LevelsToUnlock = 0;
    [SerializeField]
    public bool ConquerCompleted = false;
    [SerializeField]
    public int ScoreFromSurvival = 0;
}

