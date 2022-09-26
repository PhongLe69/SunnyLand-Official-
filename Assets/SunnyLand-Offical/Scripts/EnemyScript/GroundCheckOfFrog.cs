using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckOfFrog : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public bool isGrounded;

    private void OnTriggerStay2D(Collider2D collider)
    {
        isGrounded = collider != null && (((1 << collider.gameObject.layer) & platformLayerMask) != 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
