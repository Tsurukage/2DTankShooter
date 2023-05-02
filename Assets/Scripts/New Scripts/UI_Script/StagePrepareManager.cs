using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePrepareManager : MonoBehaviour
{
    [SerializeField] private float _time = 9;
    [SerializeField] private Text _timerText;
    // Update is called once per frame
    private void Start()
    {
        _timerText = GameObject.Find("text_cd").GetComponent<Text>();
        _timerText.text = _time.ToString();
    }
    void Update()
    {
        if(_time > 0)
        {
            _time -= Time.deltaTime;
        }
        if(_time < 0)
        {
            GameManager.Instance.UpdateGameState(GameState.StageInProgress);
        }
        _timerText.text = _time.ToString("00");
    }
}
