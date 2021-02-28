using UnityEngine;

[CreateAssetMenu(fileName = "GameParameter", menuName = "GameParameter")]
public class GameParameter : ScriptableObject
{
    public int initCardNum;
    public int gameTime;
    public int schieldNum;
    public int cpuLevel;
    public int playerHP;
}
