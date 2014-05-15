using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

    private bool IsFiring = false;

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


    public virtual void StartFire()
    {
        if (ShootFrequency > 0)
        {
            StartCoroutine("Fire");
            IsFiring = true;
        }
    }
    public virtual void EndFire()
    {
        if (IsFiring)
        {
            IsFiring = false;
            StopCoroutine("Fire");
        }
    }

    protected IEnumerator Fire()
    {
        Debug.Log("Pew!");
        yield return new WaitForSeconds(1f / ShootFrequency);
    }
}
