using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBlip : MonoBehaviour
{

    private GameObject blip;

    public GameObject Blip { get { return blip; } }
    // Start is called before the first frame update
    void Start()
    {
        blip = GameObject.Instantiate(Map.Current.blipPrefab);
        blip.transform.SetParent(Map.Current.transform);
        var colour = GetComponent<Player>().info.AccentColor;
        blip.GetComponent<Image>().color = colour;
    }

    // Update is called once per frame
    void Update()
    {
        blip.transform.position = Map.Current.WorldPositionToMap(transform.position);
    }

    void OnDestroy()
    {
        GameObject.Destroy(blip);
    }
}
