using UnityEngine;
using System.Collections;

/// <summary>
/// PlayerController holds player related functionality like input handling and GUI.
/// </summary>
public class PlayerController : MonoBehaviour {

    private PlayerCharacter _Character;

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
                    if (thrust > 0)
                    {
                        GameController.SetGameState(GameState.Playing);
                    }
                    break;
                case GameState.Playing:
                    if (thrust >= 0)
                    {
                        _Character.Thrust(thrust);
                    }
                    break;
                case GameState.Dead:
                    
                    break;
                default:
                    break;
            
        }
        
            
        

	}

    private void DrawPrePlayGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical(GUILayout.ExpandHeight(true));
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Press LMB to begin.");
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
                break;
            default:
                break;
        }

    }
}
