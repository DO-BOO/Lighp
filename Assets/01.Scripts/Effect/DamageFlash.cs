using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
/// <summary>
/// 데미지 이펙트 중 Flash
/// </summary>
public class DamageFlash : MonoBehaviour
{
    private readonly int hashEmmision = Shader.PropertyToID("_EmissionColor");

    private List<Color> colors = new List<Color>();
    private Renderer[] renderers;

    private Color32 flashColor = Color.red;

    private bool isDamage = false;
    private float flashDuration = 0.2f;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        SetBeforeColor();
    }

    // 바꿀 색 받기 default : red
    public void SetColor(Color color)
    {
        float factor = Mathf.Pow(2, 2f);

        color.r *= factor;
        color.g *= factor;
        color.b *= factor;
        color.a *= factor;

        flashColor = color;
    }

    // 맨 처음 오브젝트의 색 저장
    private void SetBeforeColor()
    {
        foreach (Renderer renderer in renderers)
        {
            try
            {
                colors.Add(renderer.material.GetColor(hashEmmision));
            }
            catch
            {
                Debug.Log("ㅠㅠ");
            }
        }
    }

    // 색바꾸기
    public void DamageEffect()
    {
        if (isDamage)
        {
            StopCoroutine(TwinkleDamageEffect());
            ChangeBeforeColor();
        }
        isDamage = true;
        StartCoroutine(TwinkleDamageEffect());
    }

    private IEnumerator TwinkleDamageEffect()
    {
        ChangeColor();
        yield return new WaitForSeconds(flashDuration);
        ChangeBeforeColor();
        isDamage = false;
    }

    private void ChangeColor()
    {
        foreach (Renderer renderer in renderers)
        {
            try
            {
                renderer.material.SetColor(hashEmmision, flashColor);
            }
            catch
            {
                Debug.Log("머티리얼없음");
            }
        }
    }

    private void ChangeBeforeColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            try
            {
                renderers[i].material.SetColor(hashEmmision, colors[i]);
            }
            catch
            {
                Debug.Log("머티리얼없음");
            }

        }
    }

}
