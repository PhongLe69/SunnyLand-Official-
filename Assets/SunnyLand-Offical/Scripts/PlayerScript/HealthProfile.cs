using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProfile
{
    protected int health;
    protected float invincibleDuration;

    float takeDamageTime = 0;

    public int Health { get => health;
        protected set
        {
            health = value;

            //Invoke the event if the player reach zero health
            if (health <= 0)
            {
                if (OnHealthZero != null)
                    OnHealthZero.Invoke();

                ResetHealth();
            }

            //Invoke the event if the player changes health
            if (OnHealthChange != null)
                OnHealthChange.Invoke();
        }
    }

    public bool IsInvincible { get => (Time.time - takeDamageTime <= invincibleDuration); }

    public delegate void HealthEvent();
    public event HealthEvent OnHealthChange;
    public event HealthEvent OnTakeDamage;
    public event HealthEvent OnHealthZero;

    public void TakeDamage(int damage)
    {
        if (IsInvincible)
            return;

        Health -= damage;

        takeDamageTime = Time.time;

        if (OnTakeDamage != null)  
            OnTakeDamage.Invoke();
    }

    public virtual void ResetHealth() { }
}
