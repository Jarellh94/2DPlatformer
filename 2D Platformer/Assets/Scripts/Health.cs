using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;

    float health;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(float value, Vector2 direction, float force)
    {
        health -= value;
        

        if (health <= 0)
            Die();
        else
            gameObject.GetComponent<Enemy>().Knockback(direction, force);
    }

    public void Die()
    {
        gameObject.GetComponent<Enemy>().Respawn();

        health = maxHealth;
    }
}
