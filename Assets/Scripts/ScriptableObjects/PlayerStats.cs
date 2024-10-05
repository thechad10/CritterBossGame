using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public static int maxHealth = 3;
    public static int currentHealth;
    [Header("Movement")]
    public float speed;
}
