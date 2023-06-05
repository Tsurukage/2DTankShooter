using Models;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Window_AvatarLoader : MonoBehaviour
{
    private Player Player => Game.World.Player;
    [SerializeField] private Button btn_close;
    [SerializeField] private GameObject prefab_avatar;
    [SerializeField] private Transform scroll_content;

    private Sprite[] sprite_avatars;
    public static event Action OnAvatarUpdate;
    void Start()
    {
        if (btn_close != null)
            btn_close.onClick.AddListener(CloseWindow);
        sprite_avatars = Resources.LoadAll<Sprite>("profile_pictures");
        Spawn();
        if (sprite_avatars.Length > 0)
            OnAvatarSelect(Player.Avatar);
    }

    private void Spawn()
    {
        for(var i = 0; i < sprite_avatars.Length; i++)
        {
            var index = i;
            GameObject avatar = Instantiate(prefab_avatar, scroll_content);

            Image image = avatar.GetComponent<Image>();
            image.sprite = sprite_avatars[i];

            Button btn = avatar.GetComponent<Button>();
            btn.onClick.AddListener(() => OnAvatarSelect(index));
        }
    }

    private void OnAvatarSelect(int index)
    {
        SoundEffectManager.Instance.OnClickSound();
        print(index);
        for(var i = 0; i < sprite_avatars.Length; i++)
        {
            var btn = scroll_content.GetChild(i);
            var isSelected = i == index;
            btn.Find("img_selected").gameObject.SetActive(isSelected);
        }
        Player.Avatar = index;
        OnAvatarUpdate?.Invoke();
    }

    private void CloseWindow()
    {
        SoundEffectManager.Instance.OnClickSound();
        gameObject.SetActive(false);
    }
}
