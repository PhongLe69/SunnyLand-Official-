using System;
using TMPro;
using UnityEngine;

public class GemPicker : MonoBehaviour
{
    private float gem = 0;

    public TextMeshProUGUI textGems;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.transform.tag == "Gem")
        {
            OnGemPickUp();

            //Destroy(other.gameObject);
        }*/

        PickedUp pickedUp;
        bool isPickUp = other.gameObject.TryGetComponent(out pickedUp);

        if (isPickUp)
        {
            PickUpType pickUpType = pickedUp.OnPickUp();

            //On PickUp
            switch (pickUpType)
            {
                case PickUpType.Gem:
                    OnGemPickUp();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnGemPickUp()
    {
        gem++;
        textGems.text = gem.ToString();
    }

    public bool PickUpItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Currency":
                return true;
            default:
                Debug.LogWarning($"WARNING: No handler implemented for tag {obj.tag}.");
                return false;
        }
    }
}
