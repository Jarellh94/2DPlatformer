using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { PATROL, CHASE}

public class Enemy : MonoBehaviour {

    public float WalkRange;
    public float walkSpeed;

    public float knockbackTime;
    public float knockbackForce;
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
            if (!stopped)
            {
                if (knockbackCounter == 0)
                {
                    transform.Translate(new Vector3(walkDir * walkSpeed * Time.deltaTime, 0));
                    //rb.velocity = new Vector2(walkDir * walkSpeed, rb.velocity.y);

                    if (Mathf.Abs(transform.position.x - spawnPoint.x) > WalkRange)
                    {
                        stopped = true;
                    }
                }
                else
                {
                    knockbackCounter -= Time.deltaTime;

                    if (knockbackCounter < 0)
                        knockbackCounter = 0;
                }
            }
            else
            {
                stopped = false;

                walkDir *= -1;
            }
        }
	}

    public void Respawn()
    {
        transform.position = spawnPoint;
        walkDir = 1;
    }

    public void Knockback(Vector2 direction)
    {
        knockbackCounter = knockbackTime;

        rb.velocity = new Vector2(direction.x * knockbackForce, rb.velocity.y);
    }
}
