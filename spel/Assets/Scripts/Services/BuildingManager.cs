using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildings;
    public BuildingPlacementScript buildingPlacement;
    
    // Start is called before the first frame update
    void Start()
    {
        buildingPlacement = GetComponent<BuildingPlacementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
