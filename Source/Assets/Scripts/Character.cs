using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum AIState
{
    Idle,
    Firing
};

/// <summary>
/// A character class holds all gameplay elements related to character visible in the level.
/// It controls Aiming, Thruster and other related features
/// </summary>
public class Character : MonoBehaviour {

    /// <summary>
    /// Weapon, the player is currently holding
    /// </summary>
    private Weapon CurrentWeapon;

    /// <summary>
    /// A state that AI is in.
    /// </summary>
    private AIState _State;

    private Character _SightedEnemy = null;

    /// <summary>
    /// Prefab of weapon that will be given to player after game start
    /// </summary>
    public List<Weapon> AvailableWeapons = new List<Weapon>();


    
	// Use this for initialization
    protected virtual void Start()
    {
	    
	}

    protected virtual void Awake()
    {
        
        if (AvailableWeapons.Count > 0)
        {
            foreach (var t in gameObject.GetComponentsInChildren<Transform>())
            {
                if (t.tag == "MountPoint")
                {
                    GameObject weaponObject = Instantiate(AvailableWeapons[Random.Range(0, AvailableWeapons.Count-1)].gameObject, t.position, Quaternion.identity) as GameObject;
                    weaponObject.transform.parent = transform;
                    CurrentWeapon = weaponObject.GetComponent<Weapon>();
                }
            }
            
        }
        else
        {
            Debug.LogWarning("Character has no available weapons.");
        }
    }
	
	// Update is called once per frame
    protected virtual void Update() 
    {
	
	}
    protected virtual void FixedUpdate()
    {
        
    }

    private IEnumerator Think()
    {
        while (true)
        {
            switch (_State)
            {
                case AIState.Idle:
                    ExecuteIdle();
                    break;
                case AIState.Firing:
                    ExecuteFiring();
                    break;
                default:
                    break;
            }
            yield return new  WaitForSeconds(0.2f);
        }
    }

    private void ExecuteIdle()
    {
    }

    private void ExecuteFiring()
    {

    }
}
