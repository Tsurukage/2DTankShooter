using Models;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowPlayerInfo : MonoBehaviour
{
    [SerializeField] private Button btn_close;
    [SerializeField] private Button btn_profile;
    [SerializeField] private Text text_uid;
    [SerializeField] private Button btn_edit;
    [SerializeField] private InputField field_input;
    [SerializeField] private ToggleGroup toggle_gender;
    private Player Player => Game.World.Player;
    public static event Action OnUpdate;
    public UnityEvent OnClickAction = new UnityEvent();

    private Sprite[] sprite_avatars;
    private void Awake()
    {
        Window_AvatarLoader.OnAvatarUpdate += InitUI;
    }
    private void OnDestroy()
    {
        Window_AvatarLoader.OnAvatarUpdate -= InitUI;
    }
    void Start()
    {
        sprite_avatars = Resources.LoadAll<Sprite>("profile_pictures");
        if(btn_close != null)
            btn_close.onClick.AddListener(CloseWindow);
        if (btn_edit != null)
            btn_edit.onClick.AddListener(EditName);
        if (btn_profile != null)
            btn_profile.onClick.AddListener(ProfilePicSelector);
        foreach(var toggle in toggle_gender.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
        InitUI();
    }

    private void ProfilePicSelector()
    {
        SoundEffectManager.Instance.OnClickSound();
        OnClickAction?.Invoke();
    }

    void InitUI()
    {
        SetUid(Player.Uid);
        SetName(Player.Name);
        print((int)Player.Gender);
        SetAvatar(sprite_avatars[Player.Avatar]);
        SetGender((int)Player.Gender);
    }

    private void SetAvatar(Sprite sprite) => btn_profile.GetComponent<Image>().sprite = sprite;

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
        toggles[gender - 1].isOn = true;
    }
    private void OnToggleValueChanged(bool isOn)
    {
        SoundEffectManager.Instance.OnClickSound();
        if (isOn)
        {
            Toggle selected = toggle_gender.ActiveToggles().FirstOrDefault();
            if (selected != null)
            {
                int index = GetToggleIndex(selected);
                Player.Gender = (Gender)index;
                print(Player.Gender);
            }
        }
        else
        {
            Player.Gender = 0;
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
