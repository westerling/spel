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

    private float panDetect = 15f;
    private float minHeight = 10f;
    private float maxHeight = 100f;
    private float m_panDetect = 15f;

    private float moveX;
    private float moveZ;
    private float moveY;
    private float xPos;
    private float yPos;

    private Vector3 m_newPos;

    public GameObject selectedObject;
    private ObjectInfo selectedInfo;
    

	void Start () {

        m_newPos = new Vector3(0, 0, 0);

        rotation = Camera.main.transform.rotation;
    }
	
	void Update () {

        MoveCamera();
        RotateCamera();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }
	}

    public void LeftClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "Ground")
            {
                selectedObject = null;
                Debug.Log(selectedObject);

                selectedInfo.isSelected = false;
            }
            else if(hit.collider.tag == "Selectable")
            {
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;
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
}
