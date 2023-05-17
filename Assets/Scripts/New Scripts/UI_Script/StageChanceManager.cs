using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Models;

public class StageChanceManager : MonoBehaviour
{
    private float value_time = 0;
    [SerializeField] private Text text_timer;
    [SerializeField] private Button btn_ads;
    [SerializeField] private Button btn_diamond;
    bool clicked = false;
    bool starTimer = false;
    void Awake()
    {
        GameManager.OnStateChange += OnSetActive;
    }
    void OnDestroy()
    {
        GameManager.OnStateChange -= OnSetActive;
    }
    public void OnSetActive(GameState state, float delay = 0)
    {
        gameObject.SetActive(state == GameState.StageChancesUI);
        transform.localScale = Vector3.zero;
        if (state == GameState.StageChancesUI)
            StartCoroutine(SetDelay(delay));
    }
    IEnumerator SetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        starTimer = true;
        transform.localScale = Vector3.one;
    }
    void Start()
    {
        value_time = SimpleGame.Instance.CountDown;
        text_timer.text = value_time.ToString("00");
        btn_ads.onClick.AddListener(OnAdsClickAction);
        btn_diamond.onClick.AddListener(OnDiamondClickAction);
        starTimer = false;
    }

    private void OnDiamondClickAction()
    {
        SimpleGame.Instance.SetBool();
        var player = new Player();
        var diamond = player.Diamond;
        if (diamond > 0)
        {
            player.AddDiamond(-1);
            SetInteraction(false);
            clicked = true;
        }
        else
            print("Player has no diamond");
    }

    private void OnAdsClickAction()
    {
        SimpleGame.Instance.SetBool();
        clicked = true;
        SetInteraction(!clicked);
        SimpleGame.Instance.IncreaseShootingCount(1);
    }
    private void SetInteraction(bool interactable)
    {
        btn_ads.interactable = interactable;
        btn_diamond.interactable = interactable;
    }

    void Update()
    {
        if (starTimer)
        {
            if (value_time > 0)
            {
                value_time -= Time.deltaTime;
            }
            if (value_time < 0)
            {
                if (clicked)
                    GameManager.Instance.UpdateGameState(GameState.StageInProgress);
                else
                    GameManager.Instance.UpdateGameState(GameState.StageFailUI);
            }
            text_timer.text = value_time.ToString("00");
        }
    }
}
