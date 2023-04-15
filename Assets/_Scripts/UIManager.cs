using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private TextMeshProUGUI _heightText;

    [SerializeField]
    private GameObject _deathScreen;

    void Awake() => Instance = this;

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void GameOver()    => _deathScreen.SetActive(true);

    public void UpdateMaxHeightText(string text) => _heightText.text = text;
}
