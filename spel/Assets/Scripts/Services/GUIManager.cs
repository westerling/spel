using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public ObjectInfo primary;
    public ArmyScript primaryTest;
    public Slider healthBar;
    public Text healthDisplay;
    public Text nameDisplay;
    public Text primaryDisplay;
    public Text secondaryDisplay;
    public Text thirdDisplay;
    public Image entityIcon;
    public Button[] buttons;
    public GameObject selectionIndicator;
    public CanvasGroup InfoPanel;

    void Start()
    {
        InfoPanel.alpha = 1;
        InfoPanel.blocksRaycasts = true;
        InfoPanel.interactable = true;
    }


    void Update () {

        if (primary.tag.Equals("Selectable"))
        {
            UnitUpdate();
        }
        else if (primary.tag.Equals("Building"))
        {
            BuildingUpdate();
        }


        if (!primary.isSelected)
        {

        }
    }

    void BuildingUpdate()
    {
        selectionIndicator.SetActive(primary.isSelected);

        entityIcon = primary.ObjectImage;

        healthBar.maxValue = primary.maxHealth;
        healthBar.value = primary.health;

        nameDisplay.text = primary.objectname;

        healthDisplay.text = "" + primary.health;

        primaryDisplay.text = "" + primary.Attributes[1].amount;
        secondaryDisplay.text = "" + primary.Attributes[0].amount;
        thirdDisplay.text = "" + primary.Attributes[3].amount;
    }

    void UnitUpdate()
    {
        selectionIndicator.SetActive(primary.isSelected);

        entityIcon = primary.ObjectImage;

        healthBar.maxValue = primary.maxHealth;
        healthBar.value = primary.health;

        nameDisplay.text = primary.objectname;

        healthDisplay.text = "" + primary.health;

        primaryDisplay.text = "" + primary.Attributes[1].amount;
        secondaryDisplay.text = "" + primary.Attributes[0].amount;
        thirdDisplay.text = "" + primary.Attributes[3].amount;
    }


    void OnClick()
    {

    }
}
