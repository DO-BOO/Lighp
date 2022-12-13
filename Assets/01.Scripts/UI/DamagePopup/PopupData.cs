using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PopupData
{
    public PopupData(float upValue, float sideValue, float moveTime, float sizeTime, float afterLifetime, Color defaultColor, Color specialColor)
    {
        this.upValue = upValue;
        this.sideValue = sideValue;
        this.moveTime = moveTime;
        this.sizeTime = sizeTime;
        this.afterLifetime = afterLifetime;
        this.defaultColor = defaultColor;
        this.specialColor = specialColor;
    }

    public float upValue;
    public float sideValue;
    public float moveTime;
    public float sizeTime;
    public float afterLifetime;
    public Color defaultColor;
    public Color specialColor;

    private static PopupData original = new PopupData(150, 100, 0.5f, 0.5f, 0.1f, Color.white, Color.yellow);
    public static PopupData Original => original;
}
