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
            foreach (var u in p.startingUnits)
            {
                var go = (GameObject)GameObject.Instantiate(u, p.location.position, p.location.rotation);
                var player = go.AddComponent<Player>();
                player.info = p;
                if(!p.isAI)
                {
                    if(Player.defaultPlayer == null)
                    {
                        Player.defaultPlayer = p;
                    }
                    go.AddComponent<RightClickNavigation>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
