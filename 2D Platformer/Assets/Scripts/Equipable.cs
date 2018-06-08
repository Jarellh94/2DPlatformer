using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to be added to every equippable item.

public class Equipable : MonoBehaviour {

    public EquipableType myType = EquipableType.GUN;
    public GameObject myItem; //The item that this eqippable will give when picked up.
    public Transform firePoint; 

    //public GameObject pickupParticles;

    SpriteRenderer sprite;

    // Use this for initialization
    void Start ()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Sprite Collected()
    {
        sprite.enabled = false;
        //Instantiate(pickupParticles, transform);
        
        Destroy(gameObject, 1);
        return sprite.sprite;
    }
}
