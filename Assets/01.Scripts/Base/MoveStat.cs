using UnityEngine;
[System.Serializable]
public struct MoveStat
{
    public float speed;
    public float jumpForce;
    [Range(0f,1f)]
    public float rotationSpeed;
}