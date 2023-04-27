using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _nextStage;
    [SerializeField] private Button _home;
    // Start is called before the first frame update
    void Start()
    {
        _nextStage = GameObject.Find("btn_retry").GetComponent<Button>();
        _nextStage.onClick.AddListener(ResetScene);
        _home = GameObject.Find("btn_ads").GetComponent<Button>();
        _home.onClick.AddListener(BackToHome);
    }

    private void BackToHome()
    {
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
