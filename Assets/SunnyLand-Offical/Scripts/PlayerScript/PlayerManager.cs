using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool PickedupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Gem":
                return true;
            default:
                return false;
        }
    }
}
