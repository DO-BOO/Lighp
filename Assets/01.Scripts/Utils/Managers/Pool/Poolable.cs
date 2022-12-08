using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀링이 필요한 모든 오브젝트가 반드시 상속을 받아야하는 클래스
/// </summary>
public abstract class Poolable : MonoBehaviour
{
    /// <summary>
    /// 씬에서 사용 중일 때 true
    /// </summary>
    public bool IsUsing = false;

    /// <summary>
    /// Pool(stack)에 Push 하기 전에 데이터를 리셋하는 곳
    /// </summary>
    public abstract void ResetData();

    /// <summary>
    /// Pool에서 꺼내쓰기 전에 데이터를 설정하는 곳
    /// </summary>
    public virtual void SetData() { }
}