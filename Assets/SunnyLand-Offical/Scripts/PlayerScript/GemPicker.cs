using TMPro;
using UnityEngine;

public class GemPicker : MonoBehaviour
{
    private float gem = 0;

    public TextMeshProUGUI textGems;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Gem")
        {
            gem++;
            textGems.text = gem.ToString();

            //Destroy(other.gameObject);
        }
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
