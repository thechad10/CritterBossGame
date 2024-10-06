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
/*    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("PlayerProj"))
        {
            var rb = collision.GetComponent<Rigidbody2D>();
            Vector3 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * suck.suckStrength, ForceMode2D.Force);
            //rb.linearVelocityX += direction.normalized.x * suck.suckStrength;
            //rb.linearVelocityY += direction.normalized.y * suck.suckStrength;
            Debug.Log(rb.linearVelocity);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerProj"))
        {
            keepSucking = true;
            var rb = collision.GetComponent<Rigidbody2D>();
            StartCoroutine(Suck(rb, collision));
/*
            var rb = collision.GetComponent<Rigidbody2D>();
            Vector3 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * suck.suckStrength, ForceMode2D.Force);
            //rb.linearVelocityX += direction.normalized.x * suck.suckStrength;
            //rb.linearVelocityY += direction.normalized.y * suck.suckStrength;
            Debug.Log(rb.linearVelocity);
        */}
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
            //rb.AddForce(direction.normalized * suck.suckStrength, ForceMode2D.Force);
            Debug.Log(direction);
            rb.linearVelocityX = direction.x * suck.suckStrength;
            //rb.linearVelocityY = direction.y * suck.suckStrength;
            //Debug.Log(rb.linearVelocity);
            yield return null;
        }
    }
}
