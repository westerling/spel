using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public float gold;
    public float stone;
    public float wood;
    public float food;

    public float maxResources;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gold >= maxResources)
        {
            gold = maxResources;
        }
        if (stone >= maxResources)
        {
            stone = maxResources;
        }
        if (wood >= maxResources)
        {
            wood = maxResources;
        }
        if (food >= maxResources)
        {
            food = maxResources;
        }
    }
}
