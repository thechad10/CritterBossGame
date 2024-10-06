using TMPro;
using UnityEngine;

public class IFrameBlinker : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private float opacityValue = 1;
    private bool flip;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerHealth.publicIsImmunePlayer)
        {
            opacityValue = Mathf.PingPong(Time.time * 4, 0.75f) + 0.25f;

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacityValue);
        }
        else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            opacityValue = 1f;
        }
    }
}