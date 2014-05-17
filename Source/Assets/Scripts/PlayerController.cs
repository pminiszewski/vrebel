using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerController holds player related functionality like input handling and GUI.
/// </summary>
public class PlayerController : MonoBehaviour {

    private PlayerCharacter _Character;
    private bool DeadPlayerKeyDown = false;

	// Use this for initialization
	private void Start () {
	
	}
    private void Awake()
    {
        _Character = GameplayStatics.Character ;
    }
    
	// Update is called once per frame
	private void Update () 
    {
        //If player keeps "Fire button"(typically left mouse button), enable characters jetpack.
        float thrust = Input.GetAxis("Fire1");
       
            

            switch (GameController.GetGameState())
            {
                case GameState.PrePlay:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameController.SetGameState(GameState.Playing);
                    }
                    break;
                case GameState.Playing:
                    if (Input.GetKey(KeyCode.Space))
                    {
                        _Character.Thrust(1);
                    }
                    else
                    {
                        _Character.Thrust(0);
                    }
                    break;
                case GameState.Dead:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        DeadPlayerKeyDown = true;
                    }
                    if (Input.GetKeyUp(KeyCode.Space) && DeadPlayerKeyDown)
                    {
                        DeadPlayerKeyDown = false;
                        GameController.SetGameState(GameState.Reloading);
                        
                    }
                    break;
                default:
                    break;
            
        }
        
            
        

	}

    private void DrawDeadGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical(GUILayout.ExpandHeight(true));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("You are dead. Press space to retry.");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    private void DrawPrePlayGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical(GUILayout.ExpandHeight(true));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Press space to fly.");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void OnGUI()
    {
        switch (GameController.GetGameState())
        {
            case GameState.PrePlay:
                DrawPrePlayGUI();
                break;
            case GameState.Playing:
                break;
            case GameState.Dead:
                DrawDeadGUI();
                break;
            default:
                break;
        }

    }
}
