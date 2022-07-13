using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthProfile
{
    int health;

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

    public delegate void HealthEvent();
    public event HealthEvent OnHealthChange;
    public event HealthEvent OnHealthZero;

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public virtual void ResetHealth() { }
}
