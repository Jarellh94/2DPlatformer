using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {
    
    public GameObject projectile;
    public Transform firePoint;
    public float shootDelay;

    private SpriteRenderer mySprite;
    private float shootCounter;

    // Use this for initialization
    void Start () {
        shootCounter = shootDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(shootCounter > 0)
            shootCounter -= Time.deltaTime;
        else
        {
            shootCounter = shootDelay;
            Fire();
        }
	}

    void Fire()
    {
        GameObject newProj = Instantiate(projectile, firePoint.position, Quaternion.identity);
        Projectile proj = newProj.GetComponent<Projectile>();

        bool dir;

        if (transform.localScale.x >= 0)
            dir = true;
        else
            dir = false;

        proj.Fire(dir, false);
    }
}
