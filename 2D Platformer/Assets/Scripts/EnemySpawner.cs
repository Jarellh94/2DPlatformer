using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float spawnTime;

    GameObject spawnedEnemy;

    float currTimer;

	// Use this for initialization
	void Start () {
        SpawnEnemy();
        currTimer = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (currTimer <= 0)
            SpawnEnemy();
        else
            currTimer -= Time.deltaTime;
	}

    void SpawnEnemy()
    {
        currTimer = spawnTime;
        spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
