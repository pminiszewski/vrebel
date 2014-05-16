using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    IEnumerator SpawnPlatforms()
    {
        while (true)
        {
            float waitFor = (20/(PlatformSpeed * PlatformAmount))+Random.Range(-1f,1f) ;
            if(AvailablePlatformPresets.Count > 0)
            {

                GameObject randomPlatform = AvailablePlatformPresets[Random.Range(0, AvailablePlatformPresets.Count - 1)];
                Platform p = randomPlatform.GetComponent<Platform>().Spawn(transform.position, Quaternion.identity);
                p.transform.localScale = new Vector3(Random.Range(PlatformWidthMin, PlatformWidthMax), 1, 1);
                p.MovementSpeed = PlatformSpeed;
            }
                else
                {
                    break;
                }
            yield return new WaitForSeconds(waitFor );
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
