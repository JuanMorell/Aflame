[System.Serializable]
public class SaveCurrentLevel
{
    public int CurrentLevel { get; set; }

    public SaveCurrentLevel()
    {
        CurrentLevel = 0;
    }

    public SaveCurrentLevel(int curentLevel)
    {
        CurrentLevel = curentLevel;
    }
}