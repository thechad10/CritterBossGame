using UnityEngine;

public class DamageFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddForceY(500, ForceMode2D.Impulse);
            collision.GetComponent<PlayerHealth>().ModHealth(-1);
        }
    }
}