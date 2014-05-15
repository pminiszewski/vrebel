using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

    public float ShootFrequency = 1;
    public float Damage = 1;

	// Use this for initialization
	protected virtual void Start () {
	
	}

    protected virtual void Awake()
    {

    }
	// Update is called once per frame
	protected virtual void Update () {
	
	}


    public virtual void Fire()
    {

    }
}
