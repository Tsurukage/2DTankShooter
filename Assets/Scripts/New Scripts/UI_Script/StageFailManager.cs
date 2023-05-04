using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageFailManager : MonoBehaviour
{
    [SerializeField] private Button _nextStage;
    void Awake()
    {
        GameManager.OnStateChange += OnSetActive;
    }
    void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
    }

    private void OnSetActive(GameState state)
    {
        gameObject.SetActive(state == GameState.StageFailUI);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_nextStage != null)
            _nextStage.onClick.AddListener(GetNextLevel);
    }

    private void GetNextLevel()
    {
        GameManager.Instance.NextStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
