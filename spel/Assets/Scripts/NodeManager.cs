﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

    public enum ResourceTypes {Stone, Wood, Food, Gold, Population};
    public ResourceTypes resourceType;

    public float harvestTime;
    public float availableResource;

    public int gatherers;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(ResourceTick());	
	}
	
	// Update is called once per frame
	void Update () {
		if (availableResource <= 0)
        {
            Destroy(gameObject);
        }
	}


    public void ResourceGather()
    {
        if (gatherers != 0)
        {
            availableResource -= gatherers;
        }
    }

    IEnumerator ResourceTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            ResourceGather();
        }
    }

}
