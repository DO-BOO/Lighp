using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool")]
public class PoolList : ScriptableObject
{
    public List<PoolGroup> poolGroups;
}

[System.Serializable]
public class PoolGroup
{
    public Poolable poolObject;
    public int instanceCount;
}