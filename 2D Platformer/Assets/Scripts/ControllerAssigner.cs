using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for handling assigning controllers to players as they join the game.
public class ControllerAssigner : MonoBehaviour {
    
    public List<int> unnassignedControllers = new List<int>();
    public List<PlayerMovement> players = new List<PlayerMovement>();

    int lastAssignedPlayer = 1;

    // Use this for initialization
    void Start () {
        unnassignedControllers.Add(2);
        unnassignedControllers.Add(3);
        unnassignedControllers.Add(4);
	}
	
	// Update is called once per frame
	void Update () {

        //Checks for A button press on every controller that doens't have a player assigned
		for (int i = 0; i < unnassignedControllers.Count; i++)
        {
            if (Input.GetButtonDown("J" + unnassignedControllers[i] + "A"))
            {
                players[lastAssignedPlayer].SetControllerNum(unnassignedControllers[i]);
                unnassignedControllers.Remove(unnassignedControllers[i]);
                lastAssignedPlayer++;
            }
        }
	}
}
