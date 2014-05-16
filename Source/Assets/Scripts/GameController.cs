using UnityEngine;
using System.Collections;

public enum GameState
{
    InGUI,
    PrePlay,
    Playing,
    Dead
}; 

public class GameController : MonoBehaviour {
    public delegate void GameStateChangedDelegate(GameState newState);
    private static GameState _GameState = GameState.PrePlay;

    public static event GameStateChangedDelegate GameStateChanged;

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
	}

    void GameController_GameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.InGUI:
                break;
            case GameState.PrePlay:
                break;
            case GameState.Playing:
                break;
            case GameState.Dead:
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
