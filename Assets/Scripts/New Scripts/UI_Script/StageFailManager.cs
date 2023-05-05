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
        transform.localScale = Vector3.zero;
        if(state == GameState.StageFailUI)
            StartCoroutine(SetDelay());
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
    
    IEnumerator SetDelay()
    {
        yield return new WaitForSeconds(3f);
        transform.localScale = Vector3.one;
    }
}
