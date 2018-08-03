using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropperType { TIMED, VIEW};

public class Dropper : MonoBehaviour {

    public DropperType dropType;
    [Header("Timed Settings")]
    public float dropRate;
    [Header("View Settings")]
    public float range;
    public float angle;
    public GameObject dropObject;


    bool hasDropped = false;
    int layerMask = 1 << 10;
    float dropCounter = 0;
    
    EnemyShooter myShooter;

    // Use this for initialization
    void Start () {

        myShooter = GetComponent<EnemyShooter>();
    }
	
	// Update is called once per frame
	void Update () {

        if (dropType == DropperType.VIEW)
        {
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, new Vector2(-angle, -1), range, layerMask);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, new Vector2(angle, -1), range, layerMask);

            if ((leftHit.collider != null || rightHit.collider != null) && !hasDropped)
            {
                Debug.Log("Hit Something");
                DropIt();
                hasDropped = true;
            }
        }
        else
        {
            if (dropCounter > dropRate)
            {
                DropIt();
                dropCounter = 0;
            }
            else
                dropCounter += Time.deltaTime;
        }
	}

    void OnDrawGizmosSelected()
    {
        Vector3 downLeft = (new Vector3(-angle, -1) * range) + transform.position;
        Vector3 downRight = (new Vector3(angle, -1) * range) + transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, downLeft);
        Gizmos.DrawLine(transform.position, downRight);
    }

    void DropIt()
    {
        myShooter.Fire();
    }
}
