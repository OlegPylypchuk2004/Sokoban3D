using UnityEngine;

public static class SessionData
{
    public static LevelData Level { get; set; }

    static SessionData()
    {
        Level = Resources.Load<LevelData>("Data/Levels/level_1");
    }
}