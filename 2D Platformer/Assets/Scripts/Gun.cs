using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject projectile;
    public Transform firePoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire3"))
        {
            Fire();
        }

    }

    public void Fire()
    {
        GameObject newProj = Instantiate(projectile, firePoint.position, Quaternion.identity);

        bool dir;

        if (transform.parent.localScale.x >= 0)
            dir = true;
        else
            dir = false;

        newProj.GetComponent<Projectile>().Fire(dir);
    }
}
