using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUIController : MonoBehaviour
{
    [SerializeField]
    private Text goldText;

    void Update()
    {
        goldText.text = $"x {GameManager.Instance.Gold}";  
    }
}