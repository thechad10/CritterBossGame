using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int maxHealth;
    public int CurrentHealth { get; private set; }
    public float HealthPercentage => CurrentHealth / maxHealth;
    [Header("Attacks")]
    [SerializeField]
    protected List<EnemyAttack> Attacks = new();
    
    public static Action OnBossDeath = delegate { };

    protected void OnEnable()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Change the health by a set amount, postive or negative
    /// </summary>
    /// <param name="amount">Negative -> lose health. Positive -> gain health.</param>
    public void ChangeHealth(int amount)
    {
        CurrentHealth += amount;
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
