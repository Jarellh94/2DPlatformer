using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour {

    public int maxHealth;
    public float invincibleTime; //Invincibility timer

    protected int health;

    protected float invincibleCounter = 0;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter < 0)
                invincibleCounter = 0;
        }
	}

    public abstract void Damage(int value, Vector2 direction, float force);
    public abstract void Die();
}
