using UnityEngine;
using System.Collections;

/// <summary>
/// Base weapon class.
/// Defines Weapon projectile type, fire rate, etc.
/// </summary>
public abstract class Weapon : MonoBehaviour {

    private bool _IsFiring = false;

    private Transform WeaponTip;

#region Inspector variables
    public float ShootFrequency = 1;
    public GameObject ProjectilePrefab;
    
#endregion

    public Vector3 WeaponTipPosition
    {
        get
        {
            return WeaponTip.position;
        }
    }
    public bool IsFiring
    {
        get { return _IsFiring; }
    }
    

	// Use this for initialization
	protected virtual void Start () {
	
	}

    protected virtual void Awake()
    {
        ProjectilePrefab.GetComponent<Projectile>().RegisterPrefab();
        //ProjectilePrefab.GetComponent<ProjectileRocket>().RegisterPrefab();
        foreach (var t in GetComponentsInChildren<Transform>())
        {
            if (t.tag == "WeaponBarrelTip")
            {
                WeaponTip = t;
            }
        }
        if (WeaponTip == null)
        {
            Debug.LogWarning("Weapon" + this + "has no weapon tip");
        }
    }
	// Update is called once per frame
	protected virtual void Update () {
	
	}


    public virtual void StartFire()
    {
        if (ShootFrequency > 0)
        {
            StartCoroutine("Fire");
            _IsFiring = true;
        }
    }
    public virtual void EndFire()
    {
        if (_IsFiring)
        {
            _IsFiring = false;
            StopCoroutine("Fire");
        }
    }

    protected IEnumerator Fire()
    {
        while (true)
        {
            if(ProjectilePrefab != null)
            {
                //Projectile pr = (Instantiate(ProjectilePrefab, WeaponTipPosition, Quaternion.identity) as GameObject).GetComponent<Projectile>(); //Construct new projectile
                Projectile pr = ProjectilePrefab.GetComponent<Projectile>().Spawn(WeaponTipPosition, Quaternion.identity); //Get projectile from pool
                if (pr == null)
                {
                    Debug.LogError("Invalid projectile prefab");
                    StopCoroutine("Fire");
                    break;
                }
                pr.rigidbody.velocity = transform.forward*pr.Speed;
                pr.transform.forward = transform.forward;
            }
            float frequency = (1f / ShootFrequency );
           
            yield return new WaitForSeconds(frequency);
        }
    }
}
