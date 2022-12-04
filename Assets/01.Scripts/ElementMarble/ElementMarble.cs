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
    public int defaultBuffValue;
    public int doubleValue;       // ���� �ó����� �� ����ġ
    public int tripleValue;       // Ʈ���� �ó����� �� ����ġ
    public int rgbSynergyValue;   // RGB �ó����� �� ����ġ

    public bool rgbSynergy = false; // ���� RGB �ó������� �Ǵ�
    public int Count { get; private set; }

    protected int buffValue;
    public int BuffValue
    {
        get
        {
            return CalculateBuff();
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

    private int CalculateBuff()
    {
        if (rgbSynergy)
            return rgbSynergyValue;

        switch (Count)
        {
            case 0:
                buffValue = 0;
                break;
            case 1:
                {
                    if (rgbSynergy)
                        buffValue = rgbSynergyValue;
                    else
                        buffValue = defaultBuffValue;
                }
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

    /// <summary>
    /// ���� ������ �ϳ��� ������ �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void AddCount()
    {
        ++Count;
        ChildAddCount();
    }

    protected virtual void ChildAddCount() { }
}
