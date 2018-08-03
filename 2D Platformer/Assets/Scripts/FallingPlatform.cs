using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    public float standTime;//Time the player can stand on it before it collapses.

    private Rigidbody2D rig;
    private BoxCollider2D coll;

    private bool isFalling = false;
    private float fallCounter = 0;

    private PlatformSpawner mySpawn;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
	}
	

    void Update()
    {
        if(isFalling)
        {
            if (fallCounter < standTime)
                fallCounter += Time.deltaTime;
            else
            {
                rig.gravityScale = 1;
                coll.isTrigger = true;
                mySpawn.PlatformLeaving();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!isFalling)
                isFalling = true;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void SetSpawner(PlatformSpawner spawn)
    {
        mySpawn = spawn;
    }
}
