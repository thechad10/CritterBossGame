using System;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [Header("Health")]
    protected int _maxHealth;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }
    public float HealthPercentage => MaxHealth / CurrentHealth;
    
    public static Action OnBossDeath = delegate { }; 

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
