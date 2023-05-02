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
        _nextStage.onClick.AddListener(ResetScene);
        //_home.onClick.AddListener(BackToHome);
    }

    private void BackToHome()
    {
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
