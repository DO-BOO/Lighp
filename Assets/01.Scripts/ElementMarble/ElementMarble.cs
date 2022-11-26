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
    public float doubleValue;       // ���� �ó����� �� ����ġ
    public float tripleValue;       // Ʈ���� �ó����� �� ����ġ
    public float rgbSynergyValue;   // RGB �ó����� �� ����ġ

    public bool rgbSynergy = false; // ���� RGB �ó������� �Ǵ�
    public int Count { get; set; }

    private float buffValue;
    public float BuffValue
    {
        get
        {
            if (rgbSynergy)
                return rgbSynergyValue;

            switch (Count)
            {
                case 0:
                    buffValue = 0;
                    break;
                case 1:
                    if (rgbSynergy)
                        buffValue = defaultBuffValue;
                    else
                        buffValue = rgbSynergyValue;
                    break;
                case 2:
                    buffValue = doubleValue;
                    break;
                case 3:
                    buffValue = tripleValue;
                    break;
            }

            return buffValue;
        }
        set => buffValue = value;
    }
    public MarbleType MarbleType { get; protected set; } = MarbleType.Length;

    public ElementMarble() { }
    public ElementMarble(ElementMarble elementMarble, MarbleType marbleType)
    {
        defaultBuffValue = elementMarble.defaultBuffValue;
        doubleValue = elementMarble.doubleValue;
        tripleValue = elementMarble.tripleValue;

        MarbleType = marbleType;
    }

    protected virtual void ExecuteDoubleSynergy(StateMachine machine) { }
    protected virtual void ExecuteTripleSynergy(StateMachine machine) { }

    /// <summary>
    /// ���� ��ũ��Ʈ���� **�����ϱ� ��** �����ؾ��ϴ� �Լ��̴�.
    /// ���� ������ �������� �ó����� �����Ѵ�.
    /// </summary>
    /// <param name="count">�ش� ���ұ����� ����</param>

    public void ExecuteMarble(StateMachine mosterStateMachine)
    {
        switch (Count)
        {
            case 2:
                ExecuteDoubleSynergy(mosterStateMachine);
                break;

            case 3:
                ExecuteTripleSynergy(mosterStateMachine);
                break;
        }
    }
}
