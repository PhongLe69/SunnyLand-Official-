using UnityEngine;

public class PickedUp : MonoBehaviour
{
    [SerializeField]
    private PickUpType type = PickUpType.Gem;

    public AudioClip soundEffect;
    public GameObject pickupEffect;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager manager = collision.GetComponent<PlayerManager>();
        if (manager)
        {
            bool pickedUp = manager.PickedupItem(gameObject);
            if (pickedUp)
            {
                //anim.Play("PickedUp");
                //Destroy(gameObject, 0.1f);

                RemoveItem();
            }
        }
    }*/

    public PickUpType OnPickUp()
    {
        RemoveItem();
        return type;
    }

    private void RemoveItem()
    {
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        Destroy(gameObject);
    }

    /*private void PickUp()
    {
        anim.SetTrigger("PickedUp");
        pickupEffect.SetActive(false);
    }

    public void PickedUpItem()
    {
        PickUp();
    }*/
}

public enum PickUpType
{
    Gem,
    None,
} 
