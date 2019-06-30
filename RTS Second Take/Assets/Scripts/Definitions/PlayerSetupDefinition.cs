using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSetupDefenition 
{
    public string name;

    public Transform location;

    public Color AccentColor;

    public List<GameObject> startingUnits = new List<GameObject>();

    private List<GameObject> activeUnits = new List<GameObject>();

    public List<GameObject> ActiveUnits { get { return activeUnits; } }

    public bool isAI;
    public float credits;



    
    
}
