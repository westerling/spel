using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionList : MonoBehaviour {

    public void Move(NavMeshAgent agent, RaycastHit hit)
    {
        var destination = hit.point;
            agent.destination = destination;
    }

    public void Harvest(NavMeshAgent agent, RaycastHit hit, TaskList task, GameObject targetNode)
    {
        agent.destination = hit.collider.gameObject.transform.position;
        task = TaskList.Gathering;
        targetNode = hit.collider.gameObject;
    }
}
