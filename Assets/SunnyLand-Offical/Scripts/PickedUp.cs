using UnityEngine;

public class PickedUp : MonoBehaviour
{

    public AudioClip soundEffect;
    public GameObject pickupEffect;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager manager = collision.GetComponent<PlayerManager>();
        if (manager)
        {
            bool pickedUp = manager.PickedupItem(gameObject);
            if (pickedUp)
            {
                anim.Play("PickedUp");
                Destroy(gameObject, 0.1f);
                RemoveItem();
            }
        }
    }

    private void RemoveItem()
    {
        AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    

    
}
