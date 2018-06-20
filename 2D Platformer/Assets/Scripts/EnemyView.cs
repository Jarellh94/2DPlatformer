using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour {

    FlyingEnemy mySelf;
    Enemy myEnemySelf;

	// Use this for initialization
	void Start () {
        mySelf = GetComponentInParent<FlyingEnemy>();
        myEnemySelf = GetComponentInParent<Enemy>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (mySelf != null)
            {
                mySelf.SetTarget(coll.transform);
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else if(myEnemySelf != null)
            {
                myEnemySelf.SeePlayer(coll.transform);
            }
        }
    }
}
