using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상수 매니저
/// </summary>
public class Define
{
    public const string HORIZONTAL  = "Horizontal";
    public const string VERTICAL    = "Vertical";
    public const string JUMP        = "Jump";

    #region SHEET_URL
    public const string KEY_URL         = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=A2:B&gid=623781096";
    #endregion

    #region LAYER_MASK
    public const int BOTTOM_LAYER   = 1 << 6;
    public const int PLAYER_LAYER   = 1 << 7;
    #endregion

    #region DASH
    public const float DASH_DISTANCE    = 12f;
    public const float DASH_DURATION    = 0.2f;
    public const float DASH_COOLTIME    = 2f;
    public const float DASH_DOUBLE_TIME = 0.35f;
    #endregion

    #region EVENT
    public const short ON_END_READ_DATA = 10;
    #endregion
}