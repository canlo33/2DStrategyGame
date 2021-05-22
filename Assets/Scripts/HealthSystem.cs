using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDied;
    [SerializeField]private int maxHealth;
    private int currentHealth;    

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void Damage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead())
            OnDied?.Invoke(this, EventArgs.Empty);
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth < 0) currentHealth = 0;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
    public void HealFull()
    {
        currentHealth = maxHealth;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
    public void Initialize(int maxHealth, bool updateCurrentHealth)
    {
        this.maxHealth = maxHealth;
        if (updateCurrentHealth)
            currentHealth = maxHealth;
    }
    public bool IsHealthFull()
    {
        return currentHealth == maxHealth;
    }
    public bool IsDead()
    {
        return currentHealth == 0;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetCurrentHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }

}
