using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� (R, G, B)�� ��� �θ� Ŭ����
/// </summary>
[System.Serializable]
public class ElementMarble
{
    /// <summary>
    /// ���� ������ ���� ��ġ
    /// </summary>
    public float defaultBuffValue;
    public float doubleValue;   // ���� �ó����� �� ���� ����ġ
    public float tripleValue;   // Ʈ���� �ó����� �� ���� ����ġ

    public float BuffValue { get; private set; }

    [field: SerializeField]
    public MarbleType MarbleType { get; protected set; } = MarbleType.End;

    public ElementMarble() { }
    public ElementMarble(ElementMarble elementMarble)
    {
        defaultBuffValue = elementMarble.defaultBuffValue;
        doubleValue = elementMarble.doubleValue;
        tripleValue = elementMarble.tripleValue;
    }

    protected virtual void ExecuteDoubleSynergy() { }

    protected virtual void ExecuteTripleSynergy() { }

    /// <summary>
    /// ���� ��ũ��Ʈ���� �����ϱ� �� �����ؾ��ϴ� �Լ��̴�.
    /// ���� ������ �������� �ó����� �����Ѵ�.
    /// </summary>
    /// <param name="count">�ش� ���ұ����� ����</param>
    public void ExecuteMarble(int count)
    {
        switch (count)
        {
            case 1:
                BuffValue = defaultBuffValue;
                break;

            case 2:
                BuffValue = doubleValue;
                ExecuteDoubleSynergy();
                break;

            case 3:
                BuffValue = tripleValue;
                ExecuteTripleSynergy();
                break;
        }

        if (BuffValue > 0)
        {
            // 80% => 1.8
            BuffValue = 1f + BuffValue / 100f;
        }
        else
        {
            // 80% => 0.2
            BuffValue /= 100f;
        }
    }
}
