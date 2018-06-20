using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouch : MonoBehaviour {

    public int damage;
    public float knockbackForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D (Collision2D collide)
    {
        GameObject other = collide.gameObject;

        if (other.CompareTag("Player"))
        {
            Health othHealth = other.GetComponent<Health>();

            if (othHealth != null)
            {
                Vector2 direction = othHealth.transform.position - transform.position;

                direction = direction.normalized;

                othHealth.Damage(damage, direction, knockbackForce);
            }
        }

        if(other.GetComponent<Enemy>() != null)
        {
            if(gameObject.GetComponent<Enemy>() != null)
                gameObject.GetComponent<Enemy>().Turn();
        }
    }

    void OnCollisionStay2D(Collision2D collide)
    {
        GameObject other = collide.gameObject;

        if (other.CompareTag("Player"))
        {
            Health othHealth = other.GetComponent<Health>();

            if (othHealth != null)
            {
                Vector2 direction = othHealth.transform.position - transform.position;

                direction = direction.normalized;

                othHealth.Damage(damage, direction, knockbackForce);
            }
        }

        

        if (other.GetComponent<Enemy>() != null)
        {
            if (gameObject.GetComponent<Enemy>() != null)
                gameObject.GetComponent<Enemy>().Turn();
        }
    }
}
