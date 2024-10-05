using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Boss : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth;
    public int CurrentHealth { get; private set; }
    public float HealthPercentage => CurrentHealth / (float)maxHealth;
    [Header("Attacks")]
    [SerializeField]
    protected List<EnemyAttack> Attacks = new();
    public Image bossHealthBar;

    
    public static Action OnBossDeath = delegate { };

    protected void OnEnable()
    {
        CurrentHealth = maxHealth;
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
        if(HealthPercentage <= 0)
        {
            PlayerWin();
        }
    }

    protected void PlayerWin()
    {
        OnBossDeath();
    }
}
