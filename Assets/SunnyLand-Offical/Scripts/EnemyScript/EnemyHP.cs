using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = enemyHP;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy take damage: " + gameObject.name);

        currentHP -= damage;

        if (currentHP <= 0)
        {
            if (gameObject != null)
            {
                // Do something  
                Destroy(transform.parent.parent.gameObject);
            }
        }
    }
}