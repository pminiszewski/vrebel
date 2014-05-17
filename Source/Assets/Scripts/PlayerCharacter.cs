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
