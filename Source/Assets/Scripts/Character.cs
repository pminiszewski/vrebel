using UnityEngine;
using System.Collections;

/// <summary>
/// A character class holds all gameplay elements related to character visible in the level.
/// It controls Aiming, Thruster and other related features
/// </summary>
public class Character : MonoBehaviour {

    private ParticleSystem _Thruster;
    private bool _ThrusterEnabled;

    /// <summary>
    /// The amount of thrust that can be applies to the character
    /// </summary>
    public float MaxThrust = 40;
    
	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        _Thruster = gameObject.GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void FixedUpdate()
    {
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
        
        if (factor > 0 && !_Thruster.isPlaying )
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
