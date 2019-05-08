using UnityEngine;

public class ObjectInfo : MonoBehaviour {
    
    public bool isSelected = false;
    public ObjectTypeList objectType;

    public string objectname;

    public float maxHealth;
    public float health;
    public float atk;
    public float hArm;
    public float pArm;

    public float buildTime;

    GameObject targetNode;
    GameObject[] drops;

    StateModifier[] upgrades;

    public Team unitTeam;

    private ActionList AL;

	
	// Update is called once per frame
	void Update ()  {

        if(health <= 0)
        {
            Destroy(gameObject);
        }
	}
}
