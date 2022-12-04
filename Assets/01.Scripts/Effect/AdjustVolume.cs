using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class AdjustVolume : MonoBehaviour
{
    private Volume volume;
    private VolumeProfile profile;

    #region ORIGINAL
    private float contrast;
    #endregion

    private void Awake()
    {
        EventManager.StartListening(Define.ON_START_DARK, StartDarkEffect);
        EventManager.StartListening(Define.ON_END_DARK, EndDarkEffect);
    }

    void Start()
    {
        volume = GetComponent<Volume>();
        profile = volume.profile;

        profile.TryGet(out ColorAdjustments color);

        contrast = color.contrast.value;
    }

    private void SetContrast(float value, float duration)
    {
        profile.TryGet(out ColorAdjustments color);
        DOTween.To(() => color.contrast.value, x => color.contrast.value = x, value, duration);
    }

    private void StartDarkEffect() => SetContrast(100f, 1f);
    private void EndDarkEffect() => SetContrast(contrast, 1f);

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_DARK, StartDarkEffect);
        EventManager.StopListening(Define.ON_END_DARK, EndDarkEffect);
    }
}
