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

    BoxCollider chairCollider;

    bool hasInteracted = false;

    public bool isEmpty = true;

    public bool IsEmpty {  get => isEmpty; private set => isEmpty = value; }
    private void Start()
    {
        chairCollider = this.gameObject.GetComponent<BoxCollider>();
    }
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
    
    public void checkIsEmpty(bool isEmpty)
    {
        if (!isEmpty)
        {
            chairCollider.enabled = false;
        }
        else
        {
            chairCollider.enabled = true;
        }
    }
}
