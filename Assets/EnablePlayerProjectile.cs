using UnityEngine;

public class EnablePlayerProjectile : MonoBehaviour
{
    private float activeTimeKunai, activeTimeNinjaStar;
    private SpriteRenderer spriteRenderer;
    private Collider2D projCollider;
    private Rigidbody2D projRB;

    private enum ProjectileType // Add as needed
    {
        Kunai,
        Ninja_Star,
        Bullet
    }
    [SerializeField] private ProjectileType projectileType;
    private int currentSelection; 

    private void Start()
    {
        currentSelection = (int)projectileType;

        spriteRenderer = GetComponent<SpriteRenderer>();
        projCollider = GetComponent<Collider2D>();
        projRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentSelection == 0)
        {
            if (activeTimeKunai > 0)
                activeTimeKunai -= Time.deltaTime;
            if (activeTimeKunai <= 0)
                Kunai_DisableProjectileParameters();
        }

        if (currentSelection == 1)
        {
            if (activeTimeNinjaStar > 0)
                activeTimeNinjaStar -= Time.deltaTime;
            if (activeTimeNinjaStar <= 0)
                NinjaStar_DisableProjectileParameters();
        }

        /*if (currentSelection == 2)
        {
            //
        }*/
    }

    #region Kunai Set (currentSelection 0)
    public void Kunai_EnableProjectileParameters(float updateActiveTime, float updateThrowForce, bool updateSpriteDirection, float updateArcForce, float updateDropoffForce)
    {
        projCollider.enabled = true;

        spriteRenderer.enabled = true;
        spriteRenderer.flipX = updateSpriteDirection;

        activeTimeKunai = updateActiveTime;
        projRB.linearVelocityX = updateThrowForce;
        projRB.AddForceY(updateArcForce, ForceMode2D.Impulse);
        projRB.gravityScale = updateDropoffForce;
    }

    private void Kunai_DisableProjectileParameters()
    {
        projCollider.enabled = false;

        spriteRenderer.enabled = false; // maybe incorperate an animation that plays here, then the renderer disables after
        spriteRenderer.flipX = false;

        activeTimeKunai = 0;
        projRB.linearVelocityX = 0;
        projRB.linearVelocityY = 0;
        projRB.gravityScale = 0;
    }
    #endregion


    #region Ninja Star Set (currentSelection 1)
    public void NinjaStar_EnableProjectileParameters(float updateActiveTime, float updateThrowForce, bool updateSpriteDirection, float updateAngleForce)
    {
        projCollider.enabled = true;

        spriteRenderer.enabled = true;
        spriteRenderer.flipX = updateSpriteDirection;

        activeTimeNinjaStar = updateActiveTime;
        projRB.linearVelocityX = updateThrowForce;
        projRB.linearVelocityY = updateAngleForce;
    }

    private void NinjaStar_DisableProjectileParameters()
    {
        projCollider.enabled = false;

        spriteRenderer.enabled = false; // maybe incorperate an animation that plays here, then the renderer disables after
        spriteRenderer.flipX = false;

        activeTimeNinjaStar = 0;
        projRB.linearVelocityX = 0;
        projRB.linearVelocityY = 0;
    }
    #endregion


    #region Bullet Set (currentSelection 2)

    #endregion
}