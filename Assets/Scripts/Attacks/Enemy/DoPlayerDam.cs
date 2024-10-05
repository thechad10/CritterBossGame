using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;
        if(go.TryGetComponent<PlayerHealth>(out PlayerHealth pHealth))
        {
            pHealth.ModHealth(1);
        }
    }
}
