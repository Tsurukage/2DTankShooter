using System;
using System.Collections;
using System.Collections.Generic;
using TapTap.Common;
using TapTap.Login;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapTapLogin : MonoBehaviour
{
    [SerializeField] private bool IsTapTapPlatform;
    // Start is called before the first frame update
    void Start()
    {
        if (IsTapTapPlatform)
        {
            TapLogin.Init("tcrgx5ddx7gmsatfvg");
            TTLogin();
        }
        else
        {
            //This is for other authentication
        }
    }

    public async void TTLogin()
    {
        try
        {
            var accesstoken = await TapLogin.GetAccessToken();
            Debug.Log("已登录");
            // 直接进入游戏
            LoadToGame();
        }
        catch (Exception e)
        {
            Debug.Log("当前未登录");
            // 开始登录

            try
            {
                // 在 iOS、Android 系统下，会唤起 TapTap 客户端或以 WebView 方式进行登录
                // 在 Windows、macOS 系统下显示二维码（默认）和跳转链接（需配置）
                var accessToken = await TapLogin.Login();
                Debug.Log($"TapTap 登录成功 accessToken: {accessToken.ToJson()}");
                LoadToGame();
            }
            catch (Exception ex)
            {
                if (ex is TapException tapError)  // using TapTap.Common
                {
                    Debug.Log($"encounter exception:{tapError.code} message:{tapError.message}");
                    Debug.Log("登录取消");
                }
            }
        }
    }
    private void LoadToGame()
    {
        SceneManager.LoadScene("home");
    }
}
