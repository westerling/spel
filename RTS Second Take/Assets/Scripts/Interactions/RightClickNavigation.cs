using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RightClickNavigation : Interaction
{
    public float relaxDistance = 5;

    private NavMeshAgent agent;
    private Vector3 target = Vector3.zero;
    private bool selected = false;
    private bool isActive = false;


    public override void Deselect()
    {
        selected = false;
    }

    public override void Select()
    {
        selected = true;
    }

    public void SentToTarget()
    {
        agent.SetDestination(target);
        agent.isStopped = false;
        isActive = true;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(selected && Input.GetMouseButtonDown(1))
        {
            var tempTarget = RTSManager.Current.ScreenPointToMapPos(Input.mousePosition);
            if(tempTarget.HasValue)
            {
                target = tempTarget.Value;
                SentToTarget();
            }
        }
        if (isActive && Vector3.Distance(target, transform.position) < relaxDistance)
        {
            agent.isStopped = true;
            isActive = false;
        }
    }
}
