using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private TextMeshProUGUI _heightText;

    void Awake() => Instance = this;

    public void UpdateMaxHeightText(string text) => _heightText.text = text;
}
