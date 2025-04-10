public interface ISceneLoader
{
    public void Load(int index, float delay = 0f);
    public void Load(string name, float delay = 0f);
    public void RestartCurrent(float delay = 0f);
}