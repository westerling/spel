using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildingModel : MonoBehaviour {
    public ObjectInfo objectInfo;
    public UpgradeHandler upgradeHandler;
    public List<Upgrade> possibleUpgrades = new List<Upgrade>();


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
    }
}