using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/LevelObject", order = 1)]
public class Level : ScriptableObject
{

    public string LevelName;

    public int[] levelLayout;

    public int nextLevel;

    public Level(string name, int[] layout, int next) {
        LevelName = name;
        levelLayout = layout;
        nextLevel = next;
    }
}
