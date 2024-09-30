using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterAnimation anim;
    public Interactable focus;
    // Using LayerMask to ignore NotWalkable
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    bool isMove = true;
    bool isSitting;

    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<CharacterAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.StandUp(false);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementMask))
                {
                    motor.MoveToPoint(hit.point);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                anim.StandUp(false);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                        motor.MoveToPoint(interactable.interactionTransform.position);
                        StartCoroutine(SitDown(interactable));
                    }
                }

            }
        }
    }



    private void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    IEnumerator SitDown(Interactable interactable)
    {
        yield return new WaitUntil(() => motor.EndOfPath() == true);
        this.transform.rotation = interactable.interactionTransform.rotation;
        anim.SitDown(true);
        isMove = false;
        isSitting = true;
    }

    public void StandUp()
    {
        if (isSitting)
        {
            anim.SitDown(false);
            anim.StandUp(true);
            isMove = true;
        }
    }
}
