using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// EnemySpawner is used to spawn enemies on platforms.
/// </summary>
public class EnemySpawner : MonoBehaviour {

    public List<GameObject> ValidEnemies = new List<GameObject>();
	// Use this for initialization
	void Awake () 
    {
        

        
	}

    public List<Character> SpawnEnemies()
    {
        List<Character> newEnemies = new List<Character>();

        if (ValidEnemies.Count > 0)
        {
            Character ch = ValidEnemies[Random.Range(0, ValidEnemies.Count - 1)].GetComponent<Character>();
            ch.RegisterPrefab();

            float topPosition = collider.bounds.size.y / 2;
            float spawnHeight = topPosition + ch.collider.bounds.size.x;
            CapsuleCollider capsule = ch.collider as CapsuleCollider;
            if (capsule != null)
            {
                spawnHeight += capsule.height / 2;
            }
            Vector3 spawnPos = transform.position + new Vector3(0, spawnHeight, 0);

            Character enemy = ch.Spawn(spawnPos, Quaternion.identity);
            newEnemies.Add(enemy);

            //enemy.transform.parent = transform.parent; //UNITY BUG Hangs editor. Do parenting in PlatformSpawner.

        }

        return newEnemies;
    }
    public void OnReachedRemover()
    {
        transform.parent.GetComponent<Platform>().Release();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
