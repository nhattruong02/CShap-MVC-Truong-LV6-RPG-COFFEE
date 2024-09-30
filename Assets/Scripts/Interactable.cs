using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // distance player get object

    public float radius = 3f;

    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public bool isEmpty = true;

    public bool IsEmpty {  get => isEmpty; private set => isEmpty = value; }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

/*    private void OnTriggerEnter(Collider other)
    {
        isEmpty = false;
    }
    private void OnTriggerExit(Collider other)
    {
            isEmpty = true;

    }*/
}
