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

    protected virtual void ExecuteDoubleSynergy(CharacterHp characterHp) { }

    protected virtual void ExecuteTripleSynergy(CharacterHp characterHp) { }

    /// <summary>
    /// ���� ��ũ��Ʈ���� **�����ϱ� ��** �����ؾ��ϴ� �Լ��̴�.
    /// ���� ������ �������� �ó����� �����Ѵ�.
    /// </summary>
    /// <param name="count">�ش� ���ұ����� ����</param>
    public void ExecuteMarble(int count, CharacterHp characterHp)
    {
        switch (count)
        {
            case 1:
                BuffValue = defaultBuffValue;
                break;

            case 2:
                BuffValue = doubleValue;
                ExecuteDoubleSynergy(characterHp);
                break;

            case 3:
                BuffValue = tripleValue;
                ExecuteTripleSynergy(characterHp);
                break;
        }
    }
}
