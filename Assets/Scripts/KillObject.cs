using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Boss_Projectile"))
        {
            Destroy(collision.gameObject);
        }
    }
}
