using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour
{

    public static RTSManager Current = null;

    public List<PlayerSetupDefenition> players = new List<PlayerSetupDefenition>();

    public TerrainCollider MapCollider;

    public Vector3? ScreenPointToMapPos(Vector2 point)
    {
        var ray = Camera.main.ScreenPointToRay(point);

        RaycastHit hit;
        if (!MapCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            return null;
        }
        return hit.point;
    }

    // Start is called before the first frame update
    void Start()
    {
        Current = this;

        foreach(var p in players)
        {
            foreach (var u in p.StartingUnits)
            {
                var go = (GameObject)GameObject.Instantiate(u, p.Location.position, p.Location.rotation);
                var player = go.AddComponent<Player>();
                player.info = p;
                if(!p.IsAi)
                {
                    if(Player.defaultPlayer == null)
                    {
                        Player.defaultPlayer = p;
                    }
                    go.AddComponent<RightClickNavigation>();
                    go.AddComponent<ActionSelect>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
