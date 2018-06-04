using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float bulletSpeed;
    public float range;
    public int damage;
    public float knockbackForce;

    int direction;
    bool fired;
    float initialLoc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(fired)
        {
            transform.Translate(new Vector3(bulletSpeed * direction * Time.deltaTime, 0, 0));

            if (Mathf.Abs(transform.position.x - initialLoc) > range)
                Destroy(gameObject);
        }
	}

    public void Fire(bool right)
    {
        if (right)
            direction = 1;
        else
            direction = -1;

        initialLoc = transform.position.x;

        fired = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health othHealth = other.GetComponent <Health>();

        if(othHealth != null)
        {
            Vector2 direction = othHealth.transform.position - transform.position;

            direction = direction.normalized;

            othHealth.Damage(damage, direction, knockbackForce);

            Destroy(gameObject);
        }
    }
}
