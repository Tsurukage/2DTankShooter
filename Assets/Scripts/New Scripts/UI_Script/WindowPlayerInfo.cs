using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowPlayerInfo : MonoBehaviour
{
    [SerializeField] private Button btn_close;
    [SerializeField] private Text text_uid;
    [SerializeField] private Button btn_edit;
    [SerializeField] private InputField field_input;
    [SerializeField] private Button btn_female;
    [SerializeField] private Button btn_male;
    private Player Player => Game.World.Player;
    public static event Action OnUpdate;
    void Start()
    {
        InitUI();
        if(btn_close != null)
            btn_close.onClick.AddListener(CloseWindow);
        if (btn_edit != null)
            btn_edit.onClick.AddListener(EditName);
    }
    void InitUI()
    {
        SetUid(Player.Uid);
        SetName(Player.Name);
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
    private void OnEnable()
    {
        field_input.text = Player.Name;
    }
}
