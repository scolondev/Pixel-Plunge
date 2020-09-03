[System.Serializable]
public class World
{
    public string name; //Name of the world
    public string prefix = "Perfect"; //Prefix
    public string suffix = "Diamond"; //Suffix

    public int maxLevels = 10; //Maximum Amount of levels in a world
    public int minLevels = 10; //Minimum Amount of levels in a world
    public int levelCount = 10; //Levels in the gate
    public int seed = 0; //The seed to generate this gate
    public int progress; //Progress in the gate
    public string setPiece;

    public Level[] levels; //Levels in the world
}
