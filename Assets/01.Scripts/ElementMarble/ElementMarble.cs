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

    public int Count { get; set; }

    private float buffValue;
    public float BuffValue
    {
        get
        {
            switch (Count)
            {
                case 0:
                    buffValue = 0;
                    break;
                case 1:
                    buffValue = defaultBuffValue;
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

    [field: SerializeField]
    public MarbleType MarbleType { get; protected set; } = MarbleType.End;

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
    /// ���� ��ũ��Ʈ���� �����ϱ� �� �����ؾ��ϴ� �Լ��̴�.
    /// ���� ������ �������� �ó����� �����Ѵ�.
    /// </summary>
    /// <param name="count">�ش� ���ұ����� ����</param>
    public void ExecuteMarble(int count, StateMachine mosterStateMachine)
    {
        switch (count)
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
