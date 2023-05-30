using Models;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WindowPlayerInfo : MonoBehaviour
{
    [SerializeField] private Button btn_close;
    [SerializeField] private Text text_uid;
    [SerializeField] private Button btn_edit;
    [SerializeField] private InputField field_input;
    [SerializeField] private ToggleGroup toggle_gender;
    private Player Player => Game.World.Player;
    public static event Action OnUpdate;
    void Start()
    {
        InitUI();
        if(btn_close != null)
            btn_close.onClick.AddListener(CloseWindow);
        if (btn_edit != null)
            btn_edit.onClick.AddListener(EditName);
        foreach(var toggle in toggle_gender.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }
    void InitUI()
    {
        SetUid(Player.Uid);
        SetName(Player.Name);
        SetGender((int)Player.Gender);
    }
    public void SetWindowActive()
    {
        gameObject.SetActive(true);
        SoundEffectManager.Instance.OnClickSound();
    }
    private void CloseWindow()
    {
        gameObject.SetActive(false);
        SoundEffectManager.Instance.OnClickSound();
    }
    private void SetName(string name) => field_input.textComponent.text = name;
    private void SetUid(string text) => text_uid.text = text;
    private void EditName()
    {
        SoundEffectManager.Instance.OnClickSound();
        Player.Name = field_input.textComponent.text;
        Game.Save();
        OnUpdate?.Invoke();
    }
    private void SetGender(int gender)
    {
        Toggle[] toggles = toggle_gender.GetComponentsInChildren<Toggle>();
        toggles[gender].isOn = true;
    }
    private void OnToggleValueChanged(bool isOn)
    {
        SoundEffectManager.Instance.OnClickSound();
        if (isOn)
        {
            Toggle selected = toggle_gender.ActiveToggles().FirstOrDefault();
            if(selected != null)
            {
                int index = GetToggleIndex(selected);
                print(index);
                Player.Gender = (Gender)index;
            }
            else
            {
                Player.Gender = 0;
            }
        }
    }
    private int GetToggleIndex(Toggle toggle)
    {
        Toggle[] toggles = toggle_gender.GetComponentsInChildren<Toggle>();
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] == toggle)
            {
                return i + 1;
            }
        }
        return 0;
    }
    private void OnEnable()
    {
        field_input.text = Player.Name;
    }
}
