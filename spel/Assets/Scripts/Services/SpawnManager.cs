using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private IEnumerator coroutine;

    private float buildTime;

    public void SpawnEntity(GameObject spawnObject)
    {
        var attributes = spawnObject.GetComponent<ObjectInfo>().Attributes;
        for (int i = 0; i < attributes.Capacity; i++)
        {
            if(attributes[i].attribute.name.Equals("BuildTime"))
            {
                buildTime = attributes[i].amount;
            }
        }

        buildTime = spawnObject.GetComponent<ObjectInfo>().Attributes[4].amount;
        coroutine = SpawnTimer(buildTime, spawnObject);
        StartCoroutine(coroutine);
    }

    private IEnumerator SpawnTimer(float buildTime, GameObject spawnObject)
    {
        yield return new WaitForSeconds(buildTime);
        SpawnUnit(spawnObject);
    }

    private void SpawnUnit(GameObject spawnObject)
    {
        Instantiate(spawnObject);
    }
}
