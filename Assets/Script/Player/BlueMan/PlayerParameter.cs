using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameter", menuName = "PlayerParameter")]
public class PlayerParameter : ScriptableObject
{
    public int PLAYER_HP;
    public float RUN_SPEED;
    public float JUMP_POWER;
    public float HIGH_JUMP_POWER;
    public Vector2 KNOCKBACK_POWER;
}