using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/LevelObject", order = 1)]
public class Level : ScriptableObject
{
    public string LevelName;

    public int[] levelLayout;
}
