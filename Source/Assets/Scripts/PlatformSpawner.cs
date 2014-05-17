using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Helper object to spawn platforms in the world. At given rate it will spawn platforms, which will have enemies on it.
/// </summary>
public class PlatformSpawner : MonoBehaviour {

    /// <summary>
    /// This defines platforms movement speed. It overrides speed value defined in paltform.
    /// </summary>
    public float PlatformSpeed;

    public float PlatformAmount = 3;

    public float PlatformWidthMin = 2;
    public float PlatformWidthMax = 9;

    public List<GameObject> AvailablePlatformPresets = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        
        if (AvailablePlatformPresets.Count == 0)
        {
            Debug.LogWarning("No platform presets");
        }
        else
        {
            AvailablePlatformPresets[0].GetComponent<Platform>().RegisterPrefab();
        }
        GameController.GameStateChanged += GameController_GameStateChanged;
    }

    void GameController_GameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.PrePlay:
                break;
            case GameState.Playing:
                
                StartCoroutine("SpawnPlatforms");
                break;
            case GameState.Dead:
                StopAllCoroutines();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Cooroutine called on when game state is changed to Playing.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            float waitFor = (20 / (PlatformSpeed * PlatformAmount)) + Random.Range(-3f, 3f);
            if (AvailablePlatformPresets.Count > 0)
            {

                GameObject randomPlatform = AvailablePlatformPresets[Random.Range(0, AvailablePlatformPresets.Count - 1)];
                Platform p = randomPlatform.GetComponent<Platform>().Spawn(transform.position, Quaternion.identity);
                p.MovementSpeed = PlatformSpeed;

                EnemySpawner enemySpawner = p.gameObject.GetComponentInChildren<EnemySpawner>();
                enemySpawner.transform.localScale = new Vector3(Random.Range(PlatformWidthMin, PlatformWidthMax), 1, 1);
                List<Character> enemies = enemySpawner.SpawnEnemies();

                foreach (var enemy in enemies)
                {
                    enemy.transform.parent = p.transform;
                }
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(waitFor);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
