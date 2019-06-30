﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisibilityManager : MonoBehaviour
{
    public float timeBetweenChecks = 1;
    public float visibleRange = 100;

    private float waited = 10000;


    void Update()
    {
        waited += Time.deltaTime; 
        if(waited <= timeBetweenChecks)
        {
            return;
        }

        waited = 0;

        List<MapBlip> pBlips = new List<MapBlip>();
        List<MapBlip> oBlips = new List<MapBlip>();

        foreach (var p in RTSManager.Current.players)
        {
            Debug.Log("There are active players");
            foreach (var u in p.ActiveUnits)
            {
                var blip = u.GetComponent<MapBlip>();
                if(p == Player.defaultPlayer)
                {
                    pBlips.Add(blip);
                }
                else
                {
                    oBlips.Add(blip);
                }
            }
        }
        foreach (var o in oBlips)
        {
            bool active = false;
            foreach (var p in pBlips)
            {
                var distance = Vector3.Distance(o.transform.position, p.transform.position);

                if(distance <= visibleRange)
                {
                    active = true;
                    break;
                }
            }
            o.Blip.SetActive(active);
            foreach (var r in o.GetComponentsInChildren<Renderer>())
            {
                r.enabled = active;
            }
        }
    }
}
