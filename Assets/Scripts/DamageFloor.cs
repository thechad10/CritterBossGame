using System.Collections;
using UnityEngine;

public class DamageFloor : MonoBehaviour
{
    [SerializeField] private Transform respawnPosition;
    private PlayerHealth playerhealth;

    private void Start()
    {
        playerhealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().ModHealth(-1);
            StartCoroutine(nameof(DamageAndRespawn));
        }
    }

    IEnumerator DamageAndRespawn()
    {
        yield return new WaitForSeconds(0.1f);
        if (playerhealth.publicCurrentHealthPlayer > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().linearVelocityY = 50;
            GameObject.FindWithTag("Player").transform.position = respawnPosition.position;
        }
    }
}