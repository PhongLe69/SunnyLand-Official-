using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalMethods;

public class Stomper : MonoBehaviour
{
    public int damageToDeal;
    private Rigidbody2D theRB2D;
    public float bounceForce;
    
    // Start is called before the first frame update
    void Start()
    {
        theRB2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hurtbox")
        {
            //Add a code to detect an onrunning coroutine. Cannot deal dmg to other enemies if the previous coroutine is still running
            StartCoroutine(DelayedInvoke(0.01f, () =>
            {
                if (!PlayerHealth.INSTANCE.IsInvincible)
                {
                    //remember to fix the bug that other.gameobject is null
                    try
                    {
                        other.gameObject.GetComponent<EnemyHP>().TakeDamage(damageToDeal);
                        theRB2D.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
                    }
                    catch { }
                }
            })
            );
        }
    }


}