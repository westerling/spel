using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : BaseUnitModel {

    public TaskList task;
    public ResourceManager RM;
    public NodeManager.ResourceTypes heldRecoursesType;
    public bool isGathering = false;
    public bool isGatherer = false;
    public int heldResource;
    public int maxHeldResource;
  
    private ActionList AL;
    private NavMeshAgent agent;

    GameObject targetNode;
    GameObject[] drops;
    Vector3 offset;

    private Ray ray;
    private RaycastHit hit;

    void Start () {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();
        AL = FindObjectOfType<ActionList>();
        task = TaskList.Idle;
    }
	
	// Update is called once per frame
	void Update () {
        if (task == TaskList.Gathering)
        {
            offset = targetNode.transform.position - transform.position;
            distanceToTarget = offset.sqrMagnitude;

            if (distanceToTarget <= 3.5f * 3.5f)
            {
                Gather();
            }
        }

        if (targetNode == null)
        {
            if (heldResource != 0)
            {
                drops = GameObject.FindGameObjectsWithTag("Drops");
                agent.destination = GetClosestDropOff(drops).transform.position;
                offset = GetClosestDropOff(drops).transform.position - transform.position;
                distanceToTarget = offset.sqrMagnitude;
                drops = null;
                task = TaskList.Delivering;
            }
            else
            {
                task = TaskList.Idle;
            }
        }

        if (heldResource >= maxHeldResource)
        {
            
            DeliverResource();
        }

        if (Input.GetMouseButton(1) && GetComponent<ObjectInfo>().isSelected)
        {
            RightClick();
        }
    }
    GameObject GetClosestDropOff(GameObject[] dropOffs)
    {
        GameObject closestDrop = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject targetDrop in dropOffs)
        {
            Vector3 direction = targetDrop.transform.position - position;
            float distance = direction.sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDrop = targetDrop;
            }
        }
        return closestDrop;
    }

    private void RightClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                if(targetNode != null)
                {
                    targetNode.GetComponent<NodeManager>().gatherers--;
                }
                isGathering = false;
                AL.Move(agent, hit);
                task = TaskList.Moving;
            }

            else if (hit.collider.tag == "Resource")
            {
                AL.Move(agent, hit);
                task = TaskList.Gathering;
                targetNode = hit.collider.gameObject;
            }
            else if (hit.collider.tag == "Drops")
            {
                DeliverResource();
            }
        }
    }

    public void Gather()
    {
        isGathering = true;
        
        if (!isGatherer)
        {
            targetNode.GetComponent<NodeManager>().gatherers++;
            isGatherer = true;
        }
        heldRecoursesType = targetNode.GetComponent<NodeManager>().resourceType;
    }

    private void DeliverResource()
    {
        targetNode.GetComponent<NodeManager>().gatherers--;
        isGathering = false;
        drops = GameObject.FindGameObjectsWithTag("Drops");
        agent.destination = GetClosestDropOff(drops).transform.position;
        offset = GetClosestDropOff(drops).transform.position - transform.position;
        distanceToTarget = offset.sqrMagnitude;
        drops = null;
        task = TaskList.Delivering;
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isGathering)
            {
                heldResource++;
            }
        }
    }
}
