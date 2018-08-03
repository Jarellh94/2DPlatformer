using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public GameObject platformPrefab;
    public float platformRespawnTime;

    bool platformGone;
    float respawnCounter = 0;

	// Use this for initialization
	void Start () {
        RespawnPlatform();
	}
	
	// Update is called once per frame
	void Update () {

        //Executes when a player touches the platform.
		if(platformGone)
        {
            if (respawnCounter < platformRespawnTime)
                respawnCounter += Time.deltaTime;

            else
                RespawnPlatform();
        }
	}

    void RespawnPlatform()
    {
        platformGone = false;

        GameObject platform = Instantiate(platformPrefab, transform.position,Quaternion.identity, transform);

        platform.GetComponent<FallingPlatform>().SetSpawner(this);
        respawnCounter = 0;
    }

    //Called by the platform itself
    public void PlatformLeaving()
    {
        platformGone = true;
    }
}
