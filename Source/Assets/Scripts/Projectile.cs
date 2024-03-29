﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{

    public float LifeSpan = 4;
    public float Speed = 10;
    public float Damage = 1;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    protected virtual  void Awake()
    {
        StartCoroutine("BulletTimeout");
    }

    void OnCollisionEnter(Collision colision)
    {
        foreach (var contact in colision.contacts)
        {

            Character c = contact.otherCollider.GetComponent<Character>();
            if (c != null)
            {
                c.OnHit(this);
            }
            PlayImpactEffect();
        }
        this.Release();
    }
    protected virtual void PlayImpactEffect()
    {

    }
    /// <summary>
    /// Called when projectile exceeds it's lifespan
    /// </summary>
    public virtual void OnInvalidate()
    {
        this.Release();
    }
    IEnumerator BulletTimeout()
    {
        while (true)
        {

            yield return new WaitForSeconds(LifeSpan);
            OnInvalidate();
        }

    }
    public void OnReachedRemover()
    {
        this.Release();
    }
}
