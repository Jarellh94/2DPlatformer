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

        else if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) && attackTimer == 0)
        {
            Attack();
        }


	}

    private void Attack()
    {
        //hitCollider.enabled = true;
        hitCollider.SetActive(true);
        attackTimer = attackTime;
    }
}
