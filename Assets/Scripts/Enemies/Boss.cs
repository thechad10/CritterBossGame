using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Boss : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    protected int maxHealth;
    public int CurrentHealth { get; private set; }
    public float HealthPercentage => (float)CurrentHealth / maxHealth;
    [Header("Attacks")]
    [SerializeField]
    protected List<EnemyAttack> Attacks = new();
    public Image bossHealthBar;
    public string bossName;
    public string bossTitle;
    public bool IsAttacking { get; private set; }
    [Header("Death")]
    [SerializeField, Tooltip("The death sequence for the boss")]
    protected GameActionTrigger deathSeq;
    [SerializeField, Tooltip("The sound that plays when a boss gets hurt")]
    protected string soundName = "";
    public bool IsDead { get; private set; }
    public static Action OnBossDeath = delegate { };
    public SpriteRenderer bossRendererFlash;
    private Color originalColor;

    protected void OnEnable()
    {
        CurrentHealth = maxHealth;
        if (bossHealthBar)
            bossHealthBar.fillAmount = 1;

        originalColor = bossRendererFlash.color;
    }

    /// <summary>
    /// Change the health by a set amount, postive or negative
    /// </summary>
    /// <param name="amount">Negative -> lose health. Positive -> gain health.</param>
    public void ChangeHealth(int amount)
    {
        if (IsDead) return;
        FlashOnHit();
        CurrentHealth += amount;
        bossHealthBar.fillAmount = HealthPercentage;
        if (soundName != string.Empty)
            FindObjectOfType<SoundControl>().Play(soundName);
        if(CurrentHealth <= 0)
        {
            IsDead = true;
            PlayerWin();
        }
    }

    protected void StartAttack()
    {
        IsAttacking = true;
    }

    public void FinishAttack()
    {
        IsAttacking = false;
    }

    protected void PlayerWin()
    {
        Debug.Log($"YEEEOOOOOOWWWWCH! {name} HAS DIED!");
        deathSeq.PlaySequence();
        OnBossDeath();
    }

    private void FlashOnHit()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        bossRendererFlash.color = new Color(1f, 0.8f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        bossRendererFlash.color = originalColor;
    }
}
