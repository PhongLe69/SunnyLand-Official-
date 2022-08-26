using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthProfile
{
    public static readonly int DEFAULT_PLAYER_HEALTH = 4;
    public static readonly float DEFAULT_PLAYER_INVICIBLE_DURATION = 0.3f;

    private static PlayerHealth instance;

    private PlayerHealth() 
    {
        ResetHealth();   
    }

    public static PlayerHealth INSTANCE { get
        {
            if (instance == null)
                instance = new PlayerHealth();

            instance.invincibleDuration = DEFAULT_PLAYER_INVICIBLE_DURATION;

            return instance;
        }
    }

    public override void ResetHealth()
    {
        Health = DEFAULT_PLAYER_HEALTH;
    }
}
