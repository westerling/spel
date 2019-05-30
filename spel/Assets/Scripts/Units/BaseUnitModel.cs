using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObjectInfo))]
public class BaseUnitModel : MonoBehaviour {

    public float distanceToTarget;
    public TaskList task;
    public ObjectInfo objectInfo;
    public ActionList actionList;
    public NavMeshAgent agent;
    public UpgradeHandler upgradeHandler;
}
