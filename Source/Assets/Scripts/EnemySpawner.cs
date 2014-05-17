using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    void OnDrawGizmos()
    {
        
        //Gizmos.DrawCube(new Vector3(transform.position.x, topPosition, transform.position.z), new Vector3(collider.bounds.size.x,2,collider.bounds.size.z));
    }
	// Update is called once per frame
	void Update () {
	
	}
}
