using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSetupDefenition info;

    public static PlayerSetupDefenition defaultPlayer;

    void Start()
    {
        info.ActiveUnits.Add(gameObject);
    }
    void OnDestroy()
    {
        info.ActiveUnits.Remove(gameObject);
    }
}
