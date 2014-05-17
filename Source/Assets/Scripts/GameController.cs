using UnityEngine;
using System.Collections;

/// <summary>
/// Available game states
/// </summary>
public enum GameState
{
    InGUI,
    Reloading,
    PrePlay,
    Playing,
    Dead
}; 

public class GameController : MonoBehaviour {
    
    private static GameState _GameState = GameState.Reloading;

    public static event GameStateChangedDelegate GameStateChanged;
    public delegate void GameStateChangedDelegate(GameState newState);

    public static GameState GetGameState()
    {
        return _GameState;
    }

    public static void SetGameState(GameState state)
    {
        if (_GameState != state)
        {
            
            _GameState = state;
            if (GameStateChanged != null)
            {
                GameStateChanged(state);
            }
        }
    }

	// Use this for initialization
	void Start () {
        GameStateChanged += GameController_GameStateChanged;
        SetGameState(GameState.PrePlay);
	}

    void GameController_GameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.InGUI:
                break;
            case GameState.PrePlay:
                break;
            case GameState.Reloading:
                GameplayStatics.Reset(); //Reset all static variables
                Application.LoadLevel("Entry");
                foreach (var d in GameStateChanged.GetInvocationList()) //Unbind all events
                {
                    GameStateChanged -= (GameStateChangedDelegate)d;

                }
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
