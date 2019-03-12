using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour {
    
    public bool isSelected = false;
    public bool isUnit;
    public string objectname;
    public int maxHealth;
    public int health;
    public int atk;
    public int hArm;
    public int pArm;
    


    GameObject targetNode;
    GameObject[] drops;

    private ActionList AL;

	
	// Update is called once per frame
	void Update ()  {

        if(health <= 0)
        {
            Destroy(gameObject);
        }
	}





    


}
