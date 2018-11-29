using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float panSpeed;

    private float m_panDetect = 15f;
    private float moveX;
    private float moveZ;
    private float xPos;
    private float yPos;
    private Vector3 m_newPos;

	void Start () {

        m_newPos = new Vector3(0, 0, 0);
    }
	
	void Update () {

        MoveCamera();
	}

    void MoveCamera()
    {
        moveX = Camera.main.transform.position.x;
        moveZ = Camera.main.transform.position.z;

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

        m_newPos = new Vector3(moveX, 0, moveZ);

        Camera.main.transform.position = m_newPos;
    }
}
