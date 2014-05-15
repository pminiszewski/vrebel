using UnityEngine;
using System.Collections;

/// <summary>
/// Class that holds most important references to game objects easily accessible.
/// </summary>
public static class GameplayStatics
{
    private static T Get<T>(string tag) where T : MonoBehaviour
    {
        GameObject go = GameObject.FindGameObjectWithTag("Character");
        if (go == null)
        {
            Debug.LogError("No object with tag Character in the scene.");
        }
        T comp = go.GetComponent<T>();
        if (comp == null)
        {
            Debug.LogError("Character object has no Character component.");
        }
        return comp;
    }

    public static Lazy<PlayerCharacter> Character = new Lazy<PlayerCharacter>(() => { return Get<PlayerCharacter>("Character"); });
    public static Lazy<GameController> GameController = new Lazy<GameController>(() => { return Get<GameController>("GameController"); });
    public static Lazy<PlayerController> PlayerController = new Lazy<PlayerController>(() => { return Get<PlayerController>("Player"); });


}
