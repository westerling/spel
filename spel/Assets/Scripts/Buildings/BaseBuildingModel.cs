using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildingModel : MonoBehaviour {
    public ObjectInfo objectInfo;
    public UpgradeHandler upgradeHandler;
    public List<Upgrade> possibleUpgrades = new List<Upgrade>();
    public List<GameObject> possibleUnits = new List<GameObject>();
    public GameObject spawnPosition;
    public GUIManager GUIManager;

    private Vector3 movePosition;

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        movePosition = spawnPosition.transform.position;
    }
    void Update()
    {
        foreach (var upgrade in possibleUpgrades)
        {
            foreach (var mustHaveUpgrade in upgrade.mustHaveUpgrades)
            {
                if (mustHaveUpgrade.researchStatus == ResearchStatus.Unavailable)
                {
                    upgrade.researchStatus = ResearchStatus.Unavailable;
                    return;
                }
            }
            if (upgrade.researchStatus != ResearchStatus.Researched ||
                upgrade.researchStatus != ResearchStatus.Available)
            {
                upgrade.researchStatus = ResearchStatus.Available;
            }
        }

        if (Input.GetMouseButton(1) && GetComponent<ObjectInfo>().isSelected)
        {
            SetSpawnPoint();
        }
    }

    private void SpawnBuilding(UpgradeHandler upgrades)
    {

    }

    private void SetSpawnPoint()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            switch (hit.collider.tag)
            {
                case "Ground":
                    movePosition = hit.transform.position;
                    break;
            }
        }
    }
}