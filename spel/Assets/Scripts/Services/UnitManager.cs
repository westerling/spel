using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitAttributes
{
    public Attributes attribute;
    public float amount;

    public UnitAttributes(Attributes attribute, float amount)
    {
        this.attribute = attribute;
        this.amount = amount;
    }
}
