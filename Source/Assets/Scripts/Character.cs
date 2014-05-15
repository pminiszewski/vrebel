using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private ParticleSystem _Thruster;
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

    /// <summary>
    /// Called from Player character when user enables thrust
    /// </summary>
    /// <param name="factor"></param>
    public void Thrust(float factor)
    {
        rigidbody.AddForce(0, factor * MaxThrust, 0);
        if (factor > 0 && !_Thruster.isPlaying )
        {
            _Thruster.Play();
        }
        else if (factor == 0 && _Thruster.isPlaying)
        {
            _Thruster.Stop();
        }
    }
}
