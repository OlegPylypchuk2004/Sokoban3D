using System;

public interface ISceneLoader
{
    public event Action LoadStarted;
    public event Action LoadCompleted;

    public void Load(int index, float delay = 0f);
    public void Load(string name, float delay = 0f);
    public void RestartCurrent(float delay = 0f);
}