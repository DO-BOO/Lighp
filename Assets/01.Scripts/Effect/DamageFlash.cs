using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
/// <summary>
/// ������ ����Ʈ �� Flash
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

    // �ٲ� �� �ޱ� default : red
    public void SetColor(Color color)
    {
        float factor = Mathf.Pow(2, 2f);

        color.r *= factor;
        color.g *= factor;
        color.b *= factor;
        color.a *= factor;

        flashColor = color;
    }

    // �� ó�� ������Ʈ�� �� ����
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
                Debug.Log("�Ф�");
            }
        }
    }

    // ���ٲٱ�
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
                Debug.Log("��Ƽ�������");
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
                Debug.Log("��Ƽ�������");
            }

        }
    }

}
