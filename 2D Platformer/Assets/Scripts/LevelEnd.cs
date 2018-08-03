using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to go on the end goal of a level, ends the level and returns to the map.
public class LevelEnd : MonoBehaviour
{
    public GameObject winScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        winScreen.SetActive(true);
        winScreen.GetComponent<LevelEndResults>().GameEnded();
    }
}
