using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProfile
{
    int maxHealth;

    int health;

    public delegate void HealthEvent();
    public event HealthEvent OnHealthChange;
    public event HealthEvent OnHealthZero;
    public int MaxHealth { get => maxHealth; protected set => maxHealth = value; }



    public int Health { get => health;
        protected set
        {
            health = value;

            Mathf.Clamp(health, 0, MaxHealth);

            //Invoke the event if the player reach zero health
            if (health == 0)
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

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;
    }

    public virtual void ResetHealth() { }
}
