using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public float gold;
    public float stone;
    public float wood;
    public float food;
    public float population;

    public float maxPopulation;
    public float maxResources;

    public Text goldDisplay;
    public Text foodDisplay;
    public Text stoneDisplay;
    public Text woodDisplay;
    public Text popDisplay;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ResourceEqualiser();
        updateUIElement();
    }

    void updateUIElement()
    {
        foodDisplay.text = "" + food;
        goldDisplay.text = "" + gold;
        stoneDisplay.text = "" + stone;
        woodDisplay.text = "" + wood;
        popDisplay.text = "" + population;
    }

    void ResourceEqualiser()
    {
        if (gold >= maxResources)
        {
            gold = maxResources;
        }
        if (stone >= maxResources)
        {
            stone = maxResources;
        }
        if (wood >= maxResources)
        {
            wood = maxResources;
        }
        if (food >= maxResources)
        {
            food = maxResources;
        }
    }
}
