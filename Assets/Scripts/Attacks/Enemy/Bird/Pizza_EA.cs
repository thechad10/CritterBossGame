using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Pizza_EA : EnemyAttack
{
    [SerializeField]
    private float spinSpeed;
    [SerializeField]
    private bool isSpinning;
    private bool stoppedSpinning;
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
    private float lineStartThick = 1f;
    [SerializeField]
    private float lineEndThick = 3f;
    [SerializeField]
    private Color spriteColor;
    [SerializeField, Tooltip("Used for the Color change rate")]
    private AnimationCurve colorChangeCurve;
    private Animator birdAnimator;

    public override void Attack()
    {
        if (IsAttacking)
        {
            Debug.LogError($"{this} is already attacking! Don't call this.");
            return;
        }
        birdAnimator = GetComponent<Animator>();
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
            if (idleTimer > idleTime / 4 && !stoppedSpinning)
            {
                stoppedSpinning = true;
                birdAnimator.Play("Phoenix_Pizza");
                EnableColliders();
                enableSprites();
            }
            if(idleTimer > idleTime)
            {
                resetThick();
                disableAll();
                IsAttacking = false;
                birdAnimator.Play("Phoenix_Idle");
                gameObject.GetComponent<Boss>().FinishAttack();
                timer = 0;
                idleTimer = 0;
                stoppedSpinning = false;
                return;
            }
        }
        else if (isSpinning)
        {
            timer += Time.deltaTime;
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
        foreach (Collider2D collider in colliders) 
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