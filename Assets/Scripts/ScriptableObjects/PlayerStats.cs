using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public readonly static int maxHealth = 3;
    public static int currentHealth = maxHealth;
    [Header("Movement")]
    public float speed;
}
