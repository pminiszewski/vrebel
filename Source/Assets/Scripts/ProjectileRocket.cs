using UnityEngine;
using System.Collections;

public class ProjectileRocket : Projectile
{
    protected override void Awake()
    {
        rigidbody.useGravity = false;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.velocity = Vector3.zero;
        base.Awake();
    }
    public override void OnInvalidate()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.AddTorque(Random.onUnitSphere * 3);
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }
	
}
