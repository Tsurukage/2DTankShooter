using System;
using UnityEngine;
using UnityEngine.UI;

public class Top_UI_Manager : MonoBehaviour
{
    [SerializeField] private Transform _tankCountUi;
    [SerializeField] private Transform _shootCountUi;
    [SerializeField] private Transform _badgeCountUi;
    [SerializeField] private Transform _animalCountUi;
    [SerializeField] private Button btn_setting;

    public static event Action OnClick;
    void Awake()
    {
        SimpleGame.Top_UI += SetTankCount;
    }
    void OnDestroy()
    {
        SimpleGame.Top_UI -= SetTankCount;
    }
    void Start()
    {
        if (btn_setting != null)
            btn_setting.onClick.AddListener(SetSettingActive);
    }

    public void SetSettingActive()
    {
        OnClick?.Invoke();
        SoundEffectManager.Instance.OnClickSound();
    }
    public void SetTankCount(int tank, int shoot, int badge, int animal)
    {
        if (_tankCountUi != null)
        {
            var text = _tankCountUi.GetComponentInChildren<Text>();
            text.text = tank.ToString();
        }
        if (_shootCountUi != null)
        {
            var text = _shootCountUi.GetComponentInChildren<Text>();
            text.text = shoot.ToString();
        }
        if (_badgeCountUi != null)
        {
            var text = _badgeCountUi.GetComponentInChildren<Text>();
            text.text = badge.ToString();
        }
        if(_animalCountUi != null)
        {
            var text = _animalCountUi.GetComponentInChildren<Text>();
            text.text = animal.ToString();
        }
    }
}
