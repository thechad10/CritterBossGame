using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public float speed;
    public readonly static int maxHealth = 3;
    public static int currentHealth = maxHealth;
}
