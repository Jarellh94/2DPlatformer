using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public GameObject hitCollider;

    public float attackTime;
    float attackTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;

            if(attackTimer <= 0)
            {
                attackTimer = 0;
                //hitCollider.enabled = false;
                hitCollider.SetActive(false);
            }
        }
        
	}

    public void Attack()
    {
        if (attackTimer == 0)
        {
            //hitCollider.enabled = true;
            hitCollider.SetActive(true);
            attackTimer = attackTime;
        }
    }
}
