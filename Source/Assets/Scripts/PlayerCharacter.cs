using UnityEngine;
using System.Collections;

public class PlayerCharacter : Character {

    private ParticleSystem _Thruster;
    private bool _ThrusterEnabled;

    /// <summary>
    /// The amount of thrust that can be applies to the character
    /// </summary>
    public float MaxThrust = 40;

    protected override void Start()
    {
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
            if (factor > 0 && !_Thruster.isPlaying)
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
