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

    List<Color> colors = new List<Color>();
    Renderer[] renderers;

    Color32 flashColor = Color.red;

    bool isDamage = false;

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
            colors.Add(renderer.material.GetColor(hashEmmision));
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
        yield return new WaitForSeconds(0.5f);
        ChangeBeforeColor();
        isDamage = false;
    }

    private void ChangeColor()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor(hashEmmision, flashColor);
        }
    }

    private void ChangeBeforeColor()
    {
        for(int i=0;i<renderers.Length;i++)
        {
            renderers[i].material.SetColor(hashEmmision, colors[i]);
        }
    }

}
