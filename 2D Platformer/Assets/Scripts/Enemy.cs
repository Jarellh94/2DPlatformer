using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { PATROL, CHASE}

public class Enemy : MonoBehaviour {

    public float WalkRange;
    public float walkSpeed;

    public float knockbackTime;
    float knockbackCounter = 0;

    Vector3 spawnPoint;

    EnemyState myState = EnemyState.PATROL;
    bool stopped;
    int walkDir = 1;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (myState == EnemyState.PATROL)
        {
           if (knockbackCounter == 0)
            {
                transform.Translate(new Vector3(walkDir * walkSpeed * Time.deltaTime, 0));
                //rb.velocity = new Vector2(walkDir * walkSpeed, rb.velocity.y);

                if (transform.position.x - spawnPoint.x > WalkRange && walkDir > 0)
                {
                    Turn();
                }
                else if(transform.position.x - spawnPoint.x < WalkRange * -1 && walkDir < 0)
                {
                    Turn();
                }

            }
            else
            {
                knockbackCounter -= Time.deltaTime;

                if (knockbackCounter < 0)
                {
                    knockbackCounter = 0;
                    rb.velocity = Vector2.zero;
                }
            }
        }
	}

    void Turn()
    {
        walkDir *= -1;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
        walkDir = 1;
    }

    public void Knockback(Vector2 direction, float knockbackForce)
    {
        knockbackCounter = knockbackTime;

        rb.velocity = Vector2.zero;

        int dir = 1;

        if (direction.x < 0)
            dir = -1;

        //Apply knockback force to entity by changing velocity and moving the transform. This seems to feel the best for me.
        rb.velocity = new Vector2(dir * knockbackForce, 0);

        transform.Translate(new Vector2(dir * knockbackForce, 0));
    }
}
