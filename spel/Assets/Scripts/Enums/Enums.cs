using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTypeList
{
    Unit,
    Building,
    Resource,
    Nature
}

public enum UnitType
{
    All,
    Villager,
    Archer,
    Swordsman,
    Pikeman,
    Mage,
    Cavalry,
    Siege
}

public enum BuildingType
{
    All,
    House,
    Tower,
    Dropoff,
    Training
}

public enum ResearchStatus
{
    Available,
    Researched,
    Unavailable
}

public enum ArithmeticType
{
    Plus,
    Minus,
    Times,
    DividedBy
}