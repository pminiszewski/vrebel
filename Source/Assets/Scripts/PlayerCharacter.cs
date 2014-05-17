using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {

    private ParticleSystem _Thruster;
    private bool _ThrusterEnabled;

    /// <summary>
    /// The amount of thrust that can be applies to the character
    /// </summary>
    public float MaxThrust = 40;

    /// <summary>
    /// Thrust will be enabled all the time
    /// </summary>
    public bool AutoThrust = false;


    protected override void OnStart()
    {
        _ThrusterEnabled = false;
        _Thruster = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    protected override void FixedUpdate()
    {
        
        base.FixedUpdate();
        if (_ThrusterEnabled)
        {
            rigidbody.AddForce(0, MaxThrust, 0);
        }
    }

    protected override void OnGUI()
    {
        switch (GameController.GetGameState())
        {
            case GameState.Playing:
            case GameState.Dead:
                GUILayout.BeginArea(new Rect(30, 30, 200, 100));
                GUILayout.Label(string.Format("Health: {0}%", Health / MaxHealth * 100));
                GUILayout.EndArea();
                break;
        }
        
    }

    /// <summary>
    /// Add aiming prediction for moving targets
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    protected override Vector3 GetWeaponAimTarget(Weapon weapon, Character target)
    {
        float projectileSpeed = weapon.ProjectilePrefab.GetComponent<Projectile>().Speed;
        Platform p = target.transform.parent.GetComponent<Platform>();
        if(p != null) //If enemy is based on platform, adjust projectile trajectory to platform movement speed
        {
            float platformSpeed = p.MovementSpeed;
            float targetDistance = Vector3.Distance(weapon.WeaponTipPosition, target.transform.position); //Distance from weapon to target
            float projTravelTime = targetDistance / projectileSpeed; //Time it takes projectile to hit target;
            return target.transform.position + new Vector3(0, -projTravelTime * platformSpeed, 0);
        }
        else
        {
            return base.GetWeaponAimTarget(weapon, target);
        }


    }

    /// <summary>
    /// Called from Player character when user enables thrust
    /// </summary>
    /// <param name="factor"></param>
    public void Thrust(float factor)
    {
        if (_Thruster != null)
        {
            if (factor > 0 && !_Thruster.isPlaying || AutoThrust)
            {
                _ThrusterEnabled = true;
                _Thruster.Play();
            }
            else if (factor == 0 && _Thruster.isPlaying)
            {
                _ThrusterEnabled = false;
                _Thruster.Stop();
            }
        }
    }

    public override void Killed()
    {
        GameController.SetGameState(GameState.Dead);
    }

   
}
