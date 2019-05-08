using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public ObjectInfo primary;
    public Slider healthBar;
    public Text healthDisplay;
    public Text nameDisplay;
    public Text atkDisplay;
    public Text pArmkDisplay;
    public Text hArmDisplay;
    public GameObject selectionIndicator;
    public GameObject iconCam;
    public CanvasGroup InfoPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        selectionIndicator.SetActive(primary.isSelected);


        healthBar.maxValue = primary.maxHealth;
        healthBar.value = primary.health;

        iconCam.SetActive(primary.isSelected);

        nameDisplay.text = primary.objectname;

        healthDisplay.text = "" + primary.health;
        atkDisplay.text = "" + primary.atk;
        hArmDisplay.text = "" + primary.hArm;
        pArmkDisplay.text = "" + primary.pArm;

        if (primary.isSelected)
        {
            InfoPanel.alpha = 1;
            InfoPanel.blocksRaycasts = true;
            InfoPanel.interactable = true;
        }
        else
        {
            InfoPanel.alpha = 0;
            InfoPanel.blocksRaycasts = false;
            InfoPanel.interactable = false;
        }
    }
}
