using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour {

    public enum HeldResources { Stone}
    public TaskList task;
    public ResourceManager RM;
    public NodeManager.ResourceTypes heldRecoursesType;
    public bool isSelected = false;
    public bool isGathering = false;
    public int heldResource;
    public int maxHeldResource;
    public string objectname;

    GameObject targetNode;
    GameObject[] drops;

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

        if(targetNode == null)
        {
            if(heldResource != 0)
            {
                drops = GameObject.FindGameObjectsWithTag("Drops");
                agent.destination = GetClosestDropOff(drops).transform.position;
                drops = null;
                task = TaskList.Delivvering;
            }
            else
            {
                task = TaskList.Idle;
            }
        }

        if (heldResource >= maxHeldResource)
        {
            drops = GameObject.FindGameObjectsWithTag("Drops");
            agent.destination = GetClosestDropOff(drops).transform.position;
            drops = null;
            task = TaskList.Delivvering;
        }

		if (Input.GetMouseButton(1) && isSelected)
        {
            RightClick();
        }
	}

    GameObject GetClosestDropOff(GameObject[] dropOffs)
    {
        GameObject closestDrop = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject targetDrop in dropOffs)
        {
            Vector3 direction = targetDrop.transform.position - position;
            float distance = direction.sqrMagnitude;
            if(distance < closestDistance)
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
            if(hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
                task = TaskList.Moving;
            }
            else if(hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                task = TaskList.Gathering;
                targetNode = hit.collider.gameObject;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.tag == "Resource" && task == TaskList.Gathering)
        {
            isGathering = true;
            hitObject.GetComponent<NodeManager>().gatherers++;
            heldRecoursesType = hitObject.GetComponent<NodeManager>().resourceType;
        }
        else if (hitObject.tag == "Drops" && task == TaskList.Delivvering)
        {
            if(RM.stone >= RM.maxResources)
            {
                task = TaskList.Idle;
            }
            else
            {
                RM.stone += heldResource;
                heldResource = 0;
                task = TaskList.Gathering;
                agent.destination = targetNode.transform.position;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;

        switch (hitObject.tag)
        {
            case "Resource":
                hitObject.GetComponent<NodeManager>().gatherers--;
                isGathering = false;
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
