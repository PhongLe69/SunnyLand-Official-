using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthProfile
{
    public static readonly int DEFAULT_PLAYER_HEALTH = 4;

    #region Singleton implement
    private static PlayerHealth instance;

    private PlayerHealth() 
    {
        MaxHealth = DEFAULT_PLAYER_HEALTH;

        ResetHealth();   
    }

    public static PlayerHealth INSTANCE { get
        {
            if (instance == null)
                instance = new PlayerHealth();

            return instance;
        }
    }
    #endregion

    public override void ResetHealth()
    {
        Health = MaxHealth;
    }
}
