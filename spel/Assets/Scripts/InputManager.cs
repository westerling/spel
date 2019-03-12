using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;
    
    private Quaternion rotation;

    public Ray ray;
    public RaycastHit hit;

    private float minHeight = 10f;
    private float maxHeight = 100f;
    private float m_panDetect = 15f;

    private float moveX;
    private float moveZ;
    private float moveY;
    private float xPos;
    private float yPos;

    private Vector2 boxStart;
    private Vector2 boxEnd;
    private Vector3 m_newPos;

    public GameObject selectedObject;
    private GameObject[] units;

    private Rect selectBox;

    public Texture boxTexture;

    private ObjectInfo selectedInfo;
    

	void Start () {

        m_newPos = new Vector3(0, 0, 0);

        rotation = Camera.main.transform.rotation;
    }
	
	void Update () {

        MoveCamera();
        //RotateCamera();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if (Input.GetMouseButton(0) && boxStart == Vector2.zero)
        {
            boxStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && boxStart!= Vector2.zero)
        {
            boxEnd = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            units = GameObject.FindGameObjectsWithTag("Selectable");
            MultiSelect();
           
        }

        selectBox = new Rect(boxStart.x, Screen.height - boxStart.y, boxEnd.x - boxStart.x, -1 * ((Screen.height - boxStart.y) - (Screen.height - boxEnd.y)));
	}

    public void MultiSelect()
    {
        foreach(GameObject unit in units)
        {
            if (unit.GetComponent<ObjectInfo>().isUnit)
            {
                Vector2 unitPos = Camera.main.WorldToScreenPoint(unit.transform.position);

                if(selectBox.Contains(unitPos, true))
                {
                    unit.GetComponent<ObjectInfo>().isSelected = true;
                    selectedInfo = unit.GetComponent<ObjectInfo>();
                    Debug.Log(unit.GetComponent<ObjectInfo>().objectname + " is selected");
                }
            }
        }
        boxStart = Vector2.zero;
        boxEnd = Vector2.zero;
    }

    public void LeftClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            switch (hit.collider.tag)
            {
                case "Ground":
                    selectedObject = null;
                    if (selectedInfo == null)
                    {
                        return;
                    }

                    selectedInfo.isSelected = false;
                    UnselectAll();
                        break;
                case "Selectable":
                    UnselectAll();
                    selectedObject = hit.collider.gameObject;
                    selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                    selectedInfo.isSelected = true;
                    break;
                default:
                    selectedObject = null;
                    break;
            }
        }
    }

    void MoveCamera()
    {
        moveX = Camera.main.transform.position.x;
        moveZ = Camera.main.transform.position.z;
        moveY = Camera.main.transform.position.y;

        xPos = Input.mousePosition.x;
        yPos = Input.mousePosition.y;

        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            return;
        }

        if (Input.GetKey(KeyCode.A) || xPos > 0 && xPos < m_panDetect)
        {
            moveX -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || xPos < Screen.width && xPos > Screen.width - m_panDetect)
        {
            moveX += panSpeed;
        }

        if (Input.GetKey(KeyCode.W) || yPos < Screen.height && yPos > Screen.height - m_panDetect )
        {
            moveZ += panSpeed;
        }
        else if (Input.GetKey(KeyCode.S) || yPos > 0 && yPos < m_panDetect)
        {
            moveZ -= panSpeed;
        }

        moveY -= Input.GetAxis("Mouse ScrollWheel") * (panSpeed *20);
        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);

        m_newPos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = m_newPos;
    }

    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if(Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;
        }
        if(destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
        }
    }

    private void OnGUI()
    {
        if (boxStart != Vector2.zero && boxEnd != Vector2.zero)
        {
            GUI.DrawTexture(selectBox, boxTexture);
        }
    }

    private void UnselectAll()
    {
        if (units == null || units.Length < 1)
        {
            return;
        }
        foreach (GameObject unit in units)
        {
            unit.GetComponent<ObjectInfo>().isSelected = false;
        }
    }
}
