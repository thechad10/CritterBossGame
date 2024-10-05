using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class DamageModifier : MonoBehaviour
{
    [Tooltip("Positive for Heal, Negative for Damage")]
    [SerializeField]
    private int healthMod;
    private PlayerHealth pHealth;
    private Boss bhealth;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerHealth>(out pHealth))
        {
            pHealth.ModHealth(healthMod);
        }
        else if(other.TryGetComponent<Boss>(out bhealth))
        {
            bhealth.ChangeHealth(healthMod);
        }
    }
}
