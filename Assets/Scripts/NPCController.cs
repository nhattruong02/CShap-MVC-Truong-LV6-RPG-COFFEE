using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class NPCController : MonoBehaviour
{
    [SerializeField] NPCAnimation anim;
    public LayerMask movementMask;
    PlayerMotor motor;
    public float moveRadius = 10f;
    public float waitTime = 2f;
    private float timer;
    bool isMove = true;
    /*    bool isSitting;
    */
    Interactable interactable;
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        anim = GetComponent<NPCAnimation>();
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                MoveToRandomPosition();
                timer = 0f;
            }
        }

    }
    void MoveToRandomPosition()
    {
        anim.StandUp(false);

        Vector3 randomPos = RandomNavmeshLocation(moveRadius);

        motor.MoveToPoint(randomPos);
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            return hit.position;
        }
        return transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, moveRadius);
    }

    private void OnTriggerEnter(Collider collider)
    {
        interactable = collider.GetComponent<Interactable>();
        if (collider.gameObject.CompareTag(Common.Chair))
        {
            interactable.checkIsEmpty(false);
            isMove = false;
            motor.MoveToPoint(interactable.interactionTransform.position);
            StartCoroutine(SitDown(interactable));
        }
    }




    IEnumerator SitDown(Interactable interactable)
    {
        yield return new WaitUntil(() => motor.EndOfPath() == true);
        this.transform.rotation = interactable.interactionTransform.rotation;
        anim.SitDown(true);
        /*        isSitting = true;*/
        StartCoroutine(StandUp());
    }

    IEnumerator StandUp()
    {
        yield return new WaitForSeconds(5);
        anim.StandUp(true);
        anim.SitDown(false);
        interactable.checkIsEmpty(true);
        isMove = true;
    }
}
