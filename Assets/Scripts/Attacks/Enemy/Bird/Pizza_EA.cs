using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Pizza_EA : EnemyAttack
{
    [SerializeField]
    private float spinSpeed;
    [SerializeField]
    private bool isSpinning;
    [SerializeField]
    private float spinTime;
    private float timer;
    [SerializeField]
    private Transform centerPoint;
    [SerializeField]
    private List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    [SerializeField]
    private List<Collider2D> colliders = new List<Collider2D>();
    [SerializeField]
    private float idleTime;
    private float idleTimer;
    [SerializeField]
    private float lineStartThick = 0.15f;
    [SerializeField]
    private float lineEndThick = 0.35f;
    [SerializeField]
    private Color spriteColor;
    [SerializeField, Tooltip("Used for the Color change rate")]
    private AnimationCurve colorChangeCurve;


    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        IsAttacking = true;
        isSpinning = true;
        timer = 0;
        idleTimer = 0;
        enableSprites();
    }
    private void Update()
    {
        if (!IsAttacking) 
        { 

            return;
        }
        if (timer >= spinTime) 
        {
            isSpinning = false;
            idleTimer += Time.deltaTime;
            updateThick();
            if (idleTimer > idleTime * 4)
            {
                EnableColliders();
                spriteColor = Color.white;
                enableSprites();
            }
            if(idleTimer > idleTime)
            {
                spriteColor = Color.red;
                resetThick();
                disableAll();
                IsAttacking = false;
                gameObject.GetComponent<Boss>().FinishAttack();
                timer = 0;
                idleTimer = 0;
                return;
            }
        }
        else if (isSpinning)
        {
            timer += Time.deltaTime;
            spriteColor.g = colorChangeCurve.Evaluate(1 - Mathf.Clamp(timer / spinTime, 0, 1));
            spriteColor.a = colorChangeCurve.Evaluate(Mathf.Clamp(timer / spinTime, 0, 1));
            foreach (SpriteRenderer sprite in renderers)
            {
                sprite.color = spriteColor;
            }
            centerPoint.rotation = Quaternion.Euler(0, 0, centerPoint.rotation.eulerAngles.z + spinSpeed * Time.deltaTime);
        }

    }
    private void resetThick()
    {
        foreach (Collider2D sprite in colliders)
        {
            sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, lineStartThick, sprite.transform.localScale.z);
        }
    }
    private void updateThick()
    {
        foreach (Collider2D sprite in colliders)
        {
            sprite.transform.localScale = Vector3.Lerp(new Vector3(sprite.transform.localScale.x, lineStartThick, sprite.transform.localScale.z), new Vector3(sprite.transform.localScale.x, lineEndThick, sprite.transform.localScale.z), idleTimer / idleTime);
        }
    }
    private void enableSprites()
    {
        foreach(SpriteRenderer sprite in renderers)
        {
            sprite.enabled = true;
            sprite.color = spriteColor;
        }
    }
    private void EnableColliders()
    {
        foreach(Collider2D collider in colliders) 
        {
            collider.enabled = true;
        }
    }
    private void disableAll()
    {
        foreach (SpriteRenderer sprite in renderers)
        {
            sprite.enabled = false;
        }
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
}