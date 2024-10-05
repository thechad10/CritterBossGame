using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public float speed;
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
}
