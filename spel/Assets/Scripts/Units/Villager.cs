using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : BaseUnitModel
{    
    public ResourceManager resourceManager;
    public ResourceScript.ResourceTypes heldRecoursesType;

    public int heldResource;
    public int maxHeldResource;

    public bool isGathering = false;
    public bool isGatherer = false;
    public bool isBuilder = false;
    public bool isBuilding = false;


    GameObject targetNode;
    GameObject[] drops;
    Vector3 offset;

    private Ray ray;
    private RaycastHit hit;

    void Start ()
    {
        StartCoroutine(GatherTick());
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

        if (task == TaskList.Delivering)
        {
            offset = targetNode.transform.position - transform.position;
            distanceToTarget = offset.sqrMagnitude;

            if (distanceToTarget <= 3.5f * 3.5f)
            {
                Debug.Log("dropping off");
                switch (heldRecoursesType)
                {
                    case ResourceScript.ResourceTypes.Food:
                        resourceManager.food += heldResource;
                        DroppingOff();
                        break;
                    case ResourceScript.ResourceTypes.Gold:
                        resourceManager.gold += heldResource;
                        DroppingOff();
                        break;
                    case ResourceScript.ResourceTypes.Stone:
                        resourceManager.stone += heldResource;
                        DroppingOff();
                        break;
                    case ResourceScript.ResourceTypes.Wood:
                        resourceManager.wood += heldResource;
                        DroppingOff();
                        break;
                }
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
            switch(hit.collider.tag)
            {
                case "Ground":
                    if (targetNode != null)
                    {
                        targetNode.GetComponent<ResourceScript>().gatherers--;
                    }
                    isGathering = false;
                    actionList.Move(agent, hit);
                    task = TaskList.Moving;
                    break;
                case "Resource":
                    actionList.Move(agent, hit);
                        task = TaskList.Gathering;
                        targetNode = hit.collider.gameObject;
                    break;
                case "Drops":
                    DeliverResource();
                    break;
                case "Selectable":
                    var colliderObject = hit.collider.GetComponent<ObjectInfo>();
                    if (colliderObject.unitTeam != GetComponent<ObjectInfo>().unitTeam)
                    {
                        //AL.attack();
                    }
                    break;
            }            
        }
    }

    public void Gather()
    {
        isGathering = true;
        
        if (!isGatherer)
        {
            targetNode.GetComponent<ResourceScript>().gatherers++;
            isGatherer = true;
        }
        heldRecoursesType = targetNode.GetComponent<ResourceScript>().resourceType;
    }

    private void DeliverResource()
    {
        targetNode.GetComponent<ResourceScript>().gatherers--;
        isGathering = false;
        drops = GameObject.FindGameObjectsWithTag("Drops");
        agent.destination = GetClosestDropOff(drops).transform.position;
        offset = GetClosestDropOff(drops).transform.position - transform.position;
        distanceToTarget = offset.sqrMagnitude;
        drops = null;
        task = TaskList.Delivering;
    }

    private void DroppingOff()
    {
        heldResource = 0;
        task = TaskList.Gathering;
        agent.destination = targetNode.transform.position;
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
