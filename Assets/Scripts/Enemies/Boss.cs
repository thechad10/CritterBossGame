using System;
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
    public bool IsAttacking { get; private set; }
    public bool IsDead { get; private set; }
    public static Action OnBossDeath = delegate { };

    protected void OnEnable()
    {
        CurrentHealth = maxHealth;
        if (bossHealthBar)
            bossHealthBar.fillAmount = 1;
    }

    /// <summary>
    /// Change the health by a set amount, postive or negative
    /// </summary>
    /// <param name="amount">Negative -> lose health. Positive -> gain health.</param>
    public void ChangeHealth(int amount)
    {
        CurrentHealth += amount;
        bossHealthBar.fillAmount = HealthPercentage;
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
        OnBossDeath();
    }
}
