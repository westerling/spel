using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Create Upgrade")]
public class Upgrade : ScriptableObject
{
    public string description;

    public Sprite thumbnail;

    public Cost cost;

    public float researchTime;

    public ResearchStatus researchStatus;

    public ArithmeticType arithmeticType;

    public List<UnitType> affectedType = new List<UnitType>();
    public List<UnitAttributes> affectedAttributes = new List<UnitAttributes>();
    public List<Upgrade> mustHaveUpgrades = new List<Upgrade>();
}
