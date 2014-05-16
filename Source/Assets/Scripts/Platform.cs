using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public float MovementSpeed = 0.25f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        switch (GameController.GetGameState())
        {
            case GameState.PrePlay:
                break;
            case GameState.Playing:
                transform.position -= Vector3.up * MovementSpeed * Time.deltaTime;
                break;
            case GameState.Dead:
                break;
            default:
                break;
        }
	}
}
