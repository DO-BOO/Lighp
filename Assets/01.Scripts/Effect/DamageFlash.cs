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

    Color32 flashColor = Color.red;

    bool isDamage = false;

    public void SetColor(Color32 color)
    {
        flashColor = color;
    }

    public void DamageEffect()
    {
        if(isDamage)
        {
        StopCoroutine(TwinkleDamageEffect());
        }
        isDamage = true;
        StartCoroutine(TwinkleDamageEffect());
    }

    private IEnumerator TwinkleDamageEffect()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Queue<Color> colors = new Queue<Color>();

        foreach (Renderer renderer in renderers)
        {
            Color color = Color.black;
            float factor = Mathf.Pow(2, 2f);

            color.r *= factor;
            color.g *= factor;
            color.b *= factor;
            color.a *= factor;

            colors.Enqueue(renderer.material.GetColor(hashEmmision));
            renderer.material.SetColor(hashEmmision, flashColor);
        }

        yield return new WaitForSeconds(0.5f);

        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor(hashEmmision, colors.Dequeue());
        }
        isDamage = false;
    }


}
