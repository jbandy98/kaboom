using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo {

    public int level;
    public int bombCount;
    public float bombDelay;

    public static List<LevelInfo> populateGameData()
    {
        List<LevelInfo> levelInfo = new List<LevelInfo>();
        LevelInfo level1 = new LevelInfo();
        level1.level = 1;
        level1.bombCount = 10;
        level1.bombDelay = 0.4f;
        levelInfo.Add(level1);
        LevelInfo level2 = new LevelInfo();
        level2.level = 2;
        level2.bombCount = 20;
        level2.bombDelay = 0.3f;
        levelInfo.Add(level2);
        LevelInfo level3 = new LevelInfo();
        level3.level = 3;
        level3.bombCount = 30;
        level3.bombDelay = 0.2f;
        levelInfo.Add(level3);
        LevelInfo level4 = new LevelInfo();
        level4.level = 4;
        level4.bombCount = 40;
        level4.bombDelay = 0.15f;
        levelInfo.Add(level4);
        LevelInfo level5 = new LevelInfo();
        level5.level = 5;
        level5.bombCount = 50;
        level5.bombDelay = 0.1f;
        levelInfo.Add(level5);
        LevelInfo level6 = new LevelInfo();
        level6.level = 6;
        level6.bombCount = 60;
        level6.bombDelay = 0.08f;
        levelInfo.Add(level6);
        LevelInfo level7 = new LevelInfo();
        level7.level = 7;
        level7.bombCount = 70;
        level7.bombDelay = 0.06f;
        levelInfo.Add(level7);
        LevelInfo level8 = new LevelInfo();
        level8.level = 8;
        level8.bombCount = 80;
        level8.bombDelay = 0.05f;
        levelInfo.Add(level8);
        LevelInfo level9 = new LevelInfo();
        level9.level = 9;
        level9.bombCount = 90;
        level9.bombDelay = 0.05f;
        levelInfo.Add(level9);

        return levelInfo;
    }

    public static LevelInfo getLevelInfo(int level, List<LevelInfo> levelInfo)
    {
        foreach (LevelInfo levInfo in levelInfo)
        {
            if (levInfo.level == level)
            {
                return levInfo;
            }
        }
        Debug.LogError("Did not find a matching level");
        return null;
    }
}
