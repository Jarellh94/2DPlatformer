using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndResults : MonoBehaviour {

    public List<Collector> playerCollections = new List<Collector>();

    public List<GameObject> playerWinScreenPanels = new List<GameObject>();
    public List<Text> playerNumPollen = new List<Text>();
    public List<Text> playerNumHoney = new List<Text>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameEnded()
    {
        for(int i = 0; playerCollections[i].gameObject.activeSelf; i++)
        {
            playerWinScreenPanels[i].SetActive(true);
            playerNumPollen[i].text = playerCollections[i].GetPollenCount().ToString() + " Pollen";
            playerNumHoney[i].text = playerCollections[i].GetHoneyCount().ToString() + " Honey";
        }
    }
}
