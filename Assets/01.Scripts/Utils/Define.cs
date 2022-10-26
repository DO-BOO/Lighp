using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �Ŵ���
/// </summary>
public class Define
{
    public const string HORIZONTAL  = "Horizontal";
    public const string VERTICAL    = "Vertical";
    public const string JUMP        = "Jump";
    public const int PLAYER_LAYER = 1 << 7;

    #region SHEET_URL
    public const string KEY_URL         = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=A2:B&gid=623781096";
    #endregion

    #region LAYER_MASK
    public const int BOTTOM_LAYER       = 1 << 6;
    #endregion

    public const float DASH_DISTANCE    = 12f;
    public const float DASH_DURATION    = 0.2f;
    public const float DASH_COOLTIME    = 2f;

    /// <summary>
    /// DASH_DOUBLE_TIME�ʸ��� Dash�� �ٽ� ���� ��Ÿ���� �ο��ȴ�.
    /// </summary>
    public const float DASH_DOUBLE_TIME = 0.35f;
}