using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ObjectPool : MonoBehaviour
{
    private Dictionary<MonoBehaviour, List<MonoBehaviour>> _AvailablePoolObjects = new Dictionary<MonoBehaviour, List<MonoBehaviour>>();
    private Dictionary<MonoBehaviour, MonoBehaviour> _RegisteredPrefabInstances = new Dictionary<MonoBehaviour, MonoBehaviour>();

    /// <summary>
    /// Before prefab can be spawned. It's type has to be registered with the pool.
    /// </summary>
    /// <param name="template">prefab to register</param>
    public void RegisterPrefab(MonoBehaviour template)
    {
        if (!_AvailablePoolObjects.ContainsKey(template))
        {
            _AvailablePoolObjects.Add(template, new List<MonoBehaviour>());
        }
        //else
        //{
        //    Debug.LogWarning("Trying to re-register existing template.");
        //}
    }

    /// <summary>
    /// Spawn grabs available object from the pool, sets its transform and return to the caller. If pool is empty, new object is instantiated.
    /// </summary>
    /// <typeparam name="T">Type of object to return</typeparam>
    /// <param name="template">Prefab template to spawn from</param>
    /// <param name="position">Spawn object with this position</param>
    /// <param name="rotation">Spawn object with this rotation</param>
    /// <returns></returns>
    public T Spawn<T>(T template, Vector3 position, Quaternion rotation) where T : MonoBehaviour
    {
        T obj = null;

        if (_AvailablePoolObjects.ContainsKey(template))
        {
            List<MonoBehaviour> prefabList = _AvailablePoolObjects[template];
            prefabList.RemoveAll((prefab) => { return prefab == null; });
 
            if (prefabList.Count > 0)
            {
                obj = prefabList[0] as T;
                prefabList.RemoveAt(0);
            }
            if (obj == null)
            {
                obj = Instantiate(template) as T;
            }
            obj.transform.parent = null;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.gameObject.SetActive(true);
            
            _RegisteredPrefabInstances.Add(obj, template);
        }
        else
        {
            Debug.LogError("Trying to spawn unregistered prefab type");
        }
        return obj;
    }

    /// <summary>
    /// When object is no longer used, return it back to the pool.
    /// </summary>
    /// <typeparam name="T">Type of returned object.</typeparam>
    /// <param name="obj">object to return</param>
    public void Release<T>(T obj) where T : MonoBehaviour
    {
        if (obj != null)
        {
            if (_RegisteredPrefabInstances.ContainsKey(obj))
            {
                _AvailablePoolObjects[_RegisteredPrefabInstances[obj]].Add(obj);
                _RegisteredPrefabInstances.Remove(obj);
                obj.transform.parent = transform;
                obj.gameObject.SetActive(false);
                obj.StopAllCoroutines();
            }
            else
            {
                Destroy(obj.gameObject);
            }
        }
    }
}

public static class ObjectPoolExtensions
{
    public static T Spawn<T>(this T type, Vector3 position, Quaternion rotation) where T : MonoBehaviour
    {
        var r = GameplayStatics.ObjectPool.Value.Spawn<T>(type, position, rotation);
        return r;
    }
    public static void Release<T>(this T obj) where T : MonoBehaviour
    {
        GameplayStatics.ObjectPool.Value.Release<T>(obj);
    }
    public static void RegisterPrefab<T>(this T type) where T : MonoBehaviour
    {
        GameplayStatics.ObjectPool.Value.RegisterPrefab(type);
    }
}
