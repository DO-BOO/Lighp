using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum ResourceType
{
    Effect,
    Sound,
    Sprite,
    Material,
    Prefab,
    Length
}

public class UseResource
{
    /// <summary>
    /// key => path
    /// value => handle
    /// </summary>
    private Dictionary<string, AsyncOperationHandle> handles = new Dictionary<string, AsyncOperationHandle>();

    #region Instantiate
    public static GameObject Instantiate(string path)
    {
        return Addressables.InstantiateAsync(path).Result;
    }

    public static GameObject Instantiate(ResourceType type, string path)
    {
        return Instantiate($"{type}/{path}");
    }

    public static GameObject Instantiate(string path, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        return Addressables.InstantiateAsync(path, pos, rot, parent).Result;
    }

    public static GameObject Instantiate(ResourceType type, string name, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        return Instantiate($"{type}/{name}", pos, rot, parent);
    }
    #endregion // Instantiate

    #region Resource
    public void GetResource<T>(string path, Action<AsyncOperationHandle<T>> action)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        handle.Completed += action;
        handle.Completed += (x) => handles.Add(path, x);
    }

    public void GetResource<T>(ResourceType type, string path, Action<AsyncOperationHandle<T>> action)
    {
        GetResource<T>($"{type}/{path}", action);
    }
    #endregion // Resource

    #region Release
    public void Release(string path)
    {
        if (handles.ContainsKey(path))
        {
            Addressables.Release(handles[path]);
        }
    }

    public void Release(ResourceType type, string name)
    {
        Release($"{type}/{name}");
    }

    public void ReleaseAll()
    {
        foreach (var handle in handles)
        {
            Addressables.Release(handle);
        }
    }

    ~UseResource()
    {
        ReleaseAll();
    }
    #endregion // Release
}