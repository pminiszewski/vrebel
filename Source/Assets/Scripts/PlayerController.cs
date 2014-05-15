using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Character _Character;
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
        float thrust = Input.GetAxis("Fire1");
        if (thrust >= 0)
        {
            _Character.Thrust(thrust);
        }
            
        

	}


}
