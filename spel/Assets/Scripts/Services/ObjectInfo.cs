using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectInfo : MonoBehaviour {

    [Header("Unit stats")]
    public string objectname;

    public bool isSelected = false;

    public ObjectTypeList objectType;

    public int maxHealth;
    public int health;
    public int populationCost;

    public Cost cost;

    GameObject targetNode;
    GameObject[] drops;

    public Image ObjectImage;

    public Team unitTeam;

    private ActionList AL;

    [Header("Unit Attributes")]
    public List<UnitAttributes> Attributes = new List<UnitAttributes>();

    [Header("Player Attributes")]
    public List<UnitAttributes> Upgrades = new List<UnitAttributes>();

    void Update ()  {

        if(health <= 0)
        {
            Destroy(gameObject);
        }
	}
}
