using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to be added to every collectable item.

public class Collectable : MonoBehaviour {

    public int numValue;
    public CollectableType type;

    public GameObject collectedParticles;

    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Collected()
    {
        sprite.enabled = false;
        Instantiate(collectedParticles, transform);

        Destroy(gameObject, 1);

        return numValue;
    }
}
