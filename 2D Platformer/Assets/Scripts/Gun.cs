using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject defaultProj;
    public Transform firePoint;

    protected GameObject projectile;

    private SpriteRenderer mySprite;
    private Sprite defaultSprite;
    private Vector3 defaultFirePoint;

	// Use this for initialization
	void Awake () {
        mySprite = GetComponent<SpriteRenderer>();
        defaultSprite = mySprite.sprite;
        defaultFirePoint = firePoint.localPosition;
        Respawn();
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

        newProj.GetComponent<Projectile>().Fire(dir, true);

        mySprite.enabled = true;
        CancelInvoke("DisableSprite");
        Invoke("DisableSprite", 0.2f);
    }

    public void SetProjectile(GameObject newProj)
    {
        projectile = newProj;
    }

    public void SetFirePoint(Vector3 pos)
    {
        firePoint.localPosition = pos;
    }

    public void SetSprite(Sprite newSprite)
    {
        mySprite.sprite = newSprite;
    }

    void DisableSprite()
    {
        mySprite.enabled = false;
    }

    public void Respawn()
    {
        mySprite.sprite = defaultSprite;
        projectile = defaultProj;
        firePoint.localPosition = defaultFirePoint;
    }
}
