using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PopupData
{
    public PopupData(float upValue, float sideValue, float moveTime, float sizeTime, float afterLifetime)
    {
        this.upValue = upValue;
        this.sideValue = sideValue;
        this.moveTime = moveTime;
        this.sizeTime = sizeTime;
        this.afterLifetime = afterLifetime;
    }

    public float upValue;
    public float sideValue;
    public float moveTime;
    public float sizeTime;
    public float afterLifetime;

    private static PopupData defualt = new PopupData(150, 100, 0.5f, 0.5f, 0.1f);
    public static PopupData Default => defualt;
}
