using UnityEngine;
using System.Collections;

public class SuckHitbox : MonoBehaviour
{
    [SerializeField, Tooltip("Suck_EA script associated with this script")]
    private Suck_EA suck;
    private bool keepSucking;
    private void Awake()
    {
        if (suck == null) Debug.LogError("There is no suck script attached here!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || 
            collision.gameObject.layer == LayerMask.NameToLayer("PlayerProj"))
        {
            keepSucking = true;
            if(collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                StartCoroutine(Suck(rb, collision));
            }
            else { return; }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        keepSucking = false;
    }

    private IEnumerator Suck(Rigidbody2D rb, Collider2D collision)
    {
        while(keepSucking)
        {
            Vector3 direction;
            direction = transform.position - collision.transform.position;
            direction = direction.normalized;
            rb.linearVelocityX = direction.x * suck.suckStrength;
            yield return null;
        }
    }
}
