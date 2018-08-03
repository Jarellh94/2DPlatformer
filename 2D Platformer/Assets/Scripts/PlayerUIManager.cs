using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {
    
    public GameObject myPanel;

    public Text pollenText;
    public Text honeyText;
    public Text livesText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePollenCount(int num)
    {
        pollenText.text = "Pollen: " + num.ToString();
    }

    public void UpdateHoneyCount(int num)
    {
        honeyText.text = "Honey: " + num.ToString();
    }

    public void UpdateLives(int num)
    {
        livesText.text = "Lives: " + num.ToString();
    }

    public void ActivatePanel()
    {
        myPanel.SetActive(true);
    }
}
