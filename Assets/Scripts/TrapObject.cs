using UnityEngine;

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
            FindObjectOfType<LifeCount>().LoseLife();
        }
    }
}
    