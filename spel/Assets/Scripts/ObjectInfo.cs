using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour {
    
    public bool isSelected = false;
    public bool isUnit;
    public string objectname;
    public int maxHealth;
    public int health;
    public int atk;
    public int hArm;
    public int pArm;
    
    public Slider healthBar;
    public Text healthDisplay;
    public Text nameDisplay;
    public Text atkDisplay;
    public Text pArmkDisplay;
    public Text hArmDisplay;
    public GameObject selectionIndicator;
    public GameObject iconCam;
    public CanvasGroup InfoPanel;

    GameObject targetNode;
    GameObject[] drops;

    private ActionList AL;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update ()  {
        selectionIndicator.SetActive(isSelected);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        healthBar.maxValue = maxHealth;
        healthBar.value = health;

        iconCam.SetActive(isSelected);

        nameDisplay.text = objectname;

        healthDisplay.text = "" + health;
        atkDisplay.text = "" + atk;
        hArmDisplay.text = "" + hArm;
        pArmkDisplay.text = "" + pArm;

        if (isSelected)
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
