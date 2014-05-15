using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerController holds player related functionality like input handling and GUI.
/// </summary>
public class PlayerController : MonoBehaviour {

    private PlayerCharacter _Character;
	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {
        _Character = GameplayStatics.Character;
    }
	// Update is called once per frame
	void Update () 
    {
        //If player keeps "Fire button"(typically left mouse button), enable characters jetpack.
        float thrust = Input.GetAxis("Fire1");
        if (thrust >= 0)
        {
            _Character.Thrust(thrust);
        }
            
        

	}


}
