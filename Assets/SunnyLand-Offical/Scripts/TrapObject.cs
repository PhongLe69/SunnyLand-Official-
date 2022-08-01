using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TrapObject : MonoBehaviour
{
    //int decateAmount = 25;
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //FindObjectOfType<HealthBar>().LoseHealth(decateAmount);
            FindObjectOfType<PlayerHealthManager>().LoseLife();
            /*GetComponent<BoxCollider2D>().enabled = false;
            DisableCollider();
            GetComponent<BoxCollider2D>().enabled = true;*/
        }
    }

    /*IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(2f);
    }*/

    /*private IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            //FindObjectOfType<HealthBar>().LoseHealth(decateAmount);
            FindObjectOfType<PlayerHealthManager>().LoseLife();
            collider.enabled = false;
            yield return new WaitForSeconds(2);
            collider.enabled = true;
        }
    }*/
}

    