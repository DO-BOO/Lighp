using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �Ŵ���
/// </summary>
public class Define
{
    public const string STAY        = "Stay";

    public const string ANIM_TAG_ATTACK = "Attack";
    #region SHEET_URL
    public const string SKILL_URL           = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=B3:H&gid=1746391345";
    public const string KEY_URL             = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=A2:B&gid=623781096";
    public const string ELEMENT_MARBLE_URL  = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=C3:F&gid=846138262";
    public const string WEAPON_URL  = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=B3:Q&gid=0";
    public const string ENEMY_URL  = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=B3:O&gid=570481040";
    public const string WEPON_SKILL_URL  = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=B3:G&gid=2055682788";
    #endregion

    #region LAYER_MASK
    public const int BOTTOM_LAYER       = 1 << 6;
    public const int PLAYER_LAYER       = 1 << 7;
    public const int MONSTER_LAYER      = 1 << 9;

    #endregion

    #region DASH
    public const float DASH_DISTANCE    = 12f;
    public const float DASH_DURATION    = 0.25f;
    public const float DASH_COOLTIME    = 2f;
    public const float DASH_DOUBLE_TIME = 0.35f;
    #endregion

    #region AVOID

    public const float AVOID_DISTANCE = 20f;
    public const float AVOID_DURATION = 0.3f;
    public const float AVOID_COOLTIME = 5f;

    #endregion

    #region WARNINGLINE

    public const float WARNING_DISTANCE = 0.35f;
    public const float WARNING_DURATION = 0.2f;

    #endregion

    #region EVENT
    public const short ON_END_READ_DATA = 1000;
    public const short ON_ADD_MARBLE = 3000;
    public const short ON_SET_WEAPON = 4000;

    public const short ON_START_DARK= 5000;
    public const short ON_END_DARK= 5001;
    #endregion

    #region MARBLE
    public const int ELEMENT_MARBLE_COUNT = 3;
    #endregion

    public const float FIXED_FPS = 50;
}