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
    private TextMeshProUGUI _floorDistance;

    [SerializeField]
    private GameObject _deathScreen;

    void Awake() => Instance = this;

    void Update() => ShowBlokyFloorDistance();

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void GameOver()    => _deathScreen.SetActive(true);

    public void UpdateMaxHeightText(string text) => _heightText.text = text;

    private void ShowBlokyFloorDistance()
    {
        if (float.Parse(FloorRiser.Instance.GetFloorToBottomDistance()) > 0)
        {
            _floorDistance.gameObject.SetActive(true);
            _floorDistance.text = FloorRiser.Instance.GetFloorToBottomDistance();
        }
        else
            _floorDistance.gameObject.SetActive(false);
    }
}
