using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour {


    public enum HeldResources { Stone}
    public NodeManager.ResourceTypes heldRecoursesType;
    public bool isSelected = false;
    public bool isGathering = false;
    public int heldResource;
    public int maxHeldResource;
    public string objectname;

    private NavMeshAgent agent;

    private Ray ray;
    private RaycastHit hit;

    // Use this for initialization
    void Start () {
        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (heldResource >= maxHeldResource)
        {
            //Todo drop off point
        }

		if (Input.GetMouseButton(1) && isSelected)
        {
            RightClick();
        }
	}

    private void RightClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
            }
            else if(hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;
        switch (hitObject.tag)
        {
            case "Resource":
                isGathering = true;
                hitObject.GetComponent<NodeManager>().gatherers++;
                heldRecoursesType = hitObject.GetComponent<NodeManager>().resourceType;
                break;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;

        switch (hitObject.tag)
        {
            case "Resource":
                hitObject.GetComponent<NodeManager>().gatherers--;
                break;
        }
    }

    IEnumerator GatherTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(isGathering)
            {
                heldResource++;
            }
        }
    }
}
