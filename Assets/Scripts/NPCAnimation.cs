using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NPCController))]
public class NPCAnimation : MonoBehaviour
{
    const float locomationAnimationSmoothTime = .1f;
    NavMeshAgent agent;
    Animator animator;
    float speedPercent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat(Common.speedPercent, speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
    }
    public void SitDown(bool isSitDown)
    {
        animator.SetBool(Common.sitDown, isSitDown);
    }

    public void StandUp(bool isStandUp)
    {
        animator.SetBool(Common.standUp, isStandUp);
    }

}