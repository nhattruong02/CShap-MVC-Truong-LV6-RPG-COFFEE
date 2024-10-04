using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    Transform targer;
    NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; set => agent = value; }

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    public void MoveToPoint(Vector3 point)
    {
        Agent.SetDestination(point);
    }

    public bool EndOfPath()
    {
        if (!Agent.pathPending && Agent.remainingDistance <= Agent.stoppingDistance)       
        {
            return true;
        }
        return false;
    }
}
