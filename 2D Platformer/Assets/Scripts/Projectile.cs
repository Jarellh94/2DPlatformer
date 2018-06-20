using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect { DAMAGE, SLOW, DROP};

public class Projectile : MonoBehaviour {

    public float bulletSpeed;
    public float range;
    public int damage;
    public float knockbackForce;
    public float freezeTime; //If the projectile has a freezing attribute
    public Effect myEffect = Effect.DAMAGE;

    int direction;
    bool fired;
    float initialLoc;
    bool playerBullet = true;
    Rigidbody2D rig;

	// Use this for initialization
	void Awake () {
        rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(fired)
        {
            //transform.Translate(new Vector3(bulletSpeed * direction * Time.deltaTime, 0, 0));

            if (Mathf.Abs(transform.position.x - initialLoc) > range)
                Destroy(gameObject);
        }
	}

    public void Fire(bool right, bool isPlayer)
    {
        playerBullet = isPlayer;

        if (right)
            direction = 1;
        else
            direction = -1;

        initialLoc = transform.position.x;

        //rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(bulletSpeed * direction, 0);

        fired = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !playerBullet)
        {
            if (myEffect == Effect.SLOW)
            {
                PlayerMovement othEnemy = other.GetComponent<PlayerMovement>();

                othEnemy.Freeze(freezeTime);
            }
            else
            {
                Health othHealth = other.GetComponent<Health>();

                if (othHealth != null)
                {
                    Vector2 direction = othHealth.transform.position - transform.position;

                    direction = direction.normalized;

                    othHealth.Damage(damage, direction, knockbackForce);
                }
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy") && playerBullet)
        {
            if (myEffect == Effect.SLOW)
            {
                Enemy othEnemy = other.GetComponent<Enemy>();

                othEnemy.Freeze(freezeTime);
            }
            else
            {
                Health othHealth = other.GetComponent<Health>();

                if (othHealth != null)
                {
                    Vector2 direction = othHealth.transform.position - transform.position;

                    direction = direction.normalized;

                    othHealth.Damage(damage, direction, knockbackForce);
                }
            }

            Destroy(gameObject);
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
