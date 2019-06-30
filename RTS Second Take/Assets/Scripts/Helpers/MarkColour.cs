using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkColour : MonoBehaviour
{

    public MeshRenderer[] renderers;

    void Start()
    {
        var colour = GetComponent<Player>().info.AccentColor;
        foreach (var r in renderers)
        {
            r.material.color = colour;
        }
    }

}
