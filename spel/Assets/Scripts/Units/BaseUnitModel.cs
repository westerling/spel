using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObjectInfo))]
public class BaseUnitModel : MonoBehaviour {

    public float distanceToTarget;
    public TaskList task;
    ObjectInfo objectInfo;
    public ActionList actionList;
    public NavMeshAgent agent;

    private void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //AL = FindObjectOfType<ActionList>();
        //objectInfo = GetComponent<ObjectInfo>();
        //task = TaskList.Idle;
    }
}
