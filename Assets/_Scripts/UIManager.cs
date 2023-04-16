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
        _floorDistance.gameObject.SetActive(!FloorRiser.Instance.IsFloorOnScreen());
        _floorDistance.text = GetBlokyFloorDistance();
    }
    private string GetBlokyFloorDistance() => (
        PlayerController.Instance.transform.position.y - PlayerController.Instance.transform.localScale.y / 2 - 
        FloorRiser.Instance.transform.position.y - FloorRiser.Instance.transform.localScale.y / 2)
        .ToString("F2");
}
