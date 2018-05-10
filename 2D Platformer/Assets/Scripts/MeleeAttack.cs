using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    
    public float damage;
    public float knockbackForce;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Health othHealth = other.GetComponent<Health>();

        if (othHealth != null)
        {
            Vector2 direction = othHealth.transform.position - transform.position;

            direction = direction.normalized;

            othHealth.Damage(damage, direction, knockbackForce);
        }
    }
}
