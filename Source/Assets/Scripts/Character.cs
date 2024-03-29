﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// States of the AI
/// </summary>
enum AIState
{
    Idle, //Do nothing; look for enemy
    Firing //Check if current target is still available
};

/// <summary>
/// A character class holds all gameplay elements related to character visible in the level.
/// It controls Aiming, Thruster and other related features
/// </summary>
public class Character : MonoBehaviour {

    
    private float _Health;

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
    /// Sets to true when target gets acquired. 
    /// Used to ensure that character won't fire befor adjusting weapon orientation
    /// </summary>
    private bool WantsToFire = false;

    /// <summary>
    /// Prefab of weapon that will be given to player after game start
    /// </summary>
    public List<GameObject> AvailableWeapons = new List<GameObject>();

    /// <summary>
    /// Maximum helath the haracter can have.
    /// </summary>
    public float MaxHealth = 3;

    /// <summary>
    /// Current health meter
    /// </summary>
    public float Health { get { return _Health; } }


    /// <summary>
    /// A tag to be used when looking for enemies
    /// </summary>
    public string EnemyTag;

    /// <summary>
    /// Per character modification of weapons fire rate
    /// </summary>
    public float FireRateMultiplier = 1;

    /// <summary>
    /// Force character not to shoot
    /// </summary>
    public bool CeaseFire = false;

    /// <summary>
    /// Make character invincible.
    /// </summary>
    public bool GodMode = false;

   
    /// <summary>
    /// Main AI loop
    /// </summary>
    /// <returns></returns>
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
            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// When no enemy sighted.
    /// Looks for visible enemies, adjusts weapon aim, starts firing when enemy visible
    /// </summary>
    private void ExecuteIdle()
    {
        float distance = float.MaxValue;
        Character closestEnemy = null;
        _SightedEnemy = null;
        foreach (var e in GameObject.FindGameObjectsWithTag(EnemyTag))
        {
            float d = Vector3.Distance(transform.position, e.transform.position);

            if (e.activeInHierarchy && d < distance)
            {

                if (!Physics.Linecast(CurrentWeapon.WeaponTipPosition, e.transform.position, 1 << 8))
                {
                    closestEnemy = e.GetComponent<Character>();
                }

                distance = d;
            }
        }
        if (closestEnemy != null && !CeaseFire)
        {
            _SightedEnemy = closestEnemy;
            
            WantsToFire = true;
            _State = AIState.Firing;
        }
    }

    /// <summary>
    /// Called when AI is firing at enemy. Checks if enemy is still valid. If not go to idle.
    /// </summary>
    private void ExecuteFiring()
    {
        if (_SightedEnemy == null || !_SightedEnemy.gameObject.activeInHierarchy||Physics.Linecast(CurrentWeapon.WeaponTipPosition, _SightedEnemy.transform.position, 1 << 8))
        {
            CurrentWeapon.EndFire();
            _SightedEnemy = null;
            _State = AIState.Idle;
            return;
        }
        //Debug.DrawLine(CurrentWeapon.WeaponTipPosition, _SightedEnemy.transform.position, Color.red, 1);
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Think");
        OnStart();
    }
    protected virtual void OnStart(){}

    protected virtual void Awake()
    {
        
        _Health = MaxHealth;
        if (CurrentWeapon != null) //pooled characters can already have a weapon. Destroy it.
        {
            Destroy(CurrentWeapon.gameObject);
        }
        if (AvailableWeapons.Count > 0)
        {
            foreach (var t in gameObject.GetComponentsInChildren<Transform>())
            {
                if (t.tag == "MountPoint") //Spawn a random weapon an attach it in proper place
                {
                    GameObject weaponObject = Instantiate(AvailableWeapons[Random.Range(0, AvailableWeapons.Count)], t.position, Quaternion.identity) as GameObject;
                    weaponObject.transform.parent = transform;
                    weaponObject.transform.forward = t.right;
                    CurrentWeapon = weaponObject.GetComponent<Weapon>();
                    CurrentWeapon.ShootFrequency *= FireRateMultiplier;
                    break;
                }
            }
            
        }
        else
        {
            Debug.LogWarning("Character has no available weapons.");
        }
        
    }
    
    /// <summary>
    /// Returns position, the weapon should fire at.
    /// Useful with different types of weapons with differently behaving projectiles
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    protected virtual Vector3 GetWeaponAimTarget(Weapon weapon, Character target)
    {
        return target.transform.position;
    }
    protected virtual void Update()
    {
        if (_SightedEnemy != null && CurrentWeapon != null)
        {

            CurrentWeapon.transform.LookAt(GetWeaponAimTarget(CurrentWeapon, _SightedEnemy));
            if (WantsToFire)
            {
                CurrentWeapon.StartFire();
                WantsToFire = false;
            }
        }
	}
    protected virtual void FixedUpdate()
    {
        
    }
    protected virtual void OnGUI()
    {
    }
    public virtual void Killed()
    {
        
    }
    public void Kill()
    {
        Killed();
        this.Release();
    }

    /// <summary>
    /// Called when object reaches bottom of the scene
    /// </summary>
    public void OnReachedRemover()
    {
        Kill();
    }
    public void OnHit(Projectile proj)
    {
        if (!GodMode)
        {

            _Health -= proj.Damage;
            if (_Health <= 0)
            {
                Kill();

            }
        }
    }
}
