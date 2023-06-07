using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCalculator : MonoBehaviour
{
    private void Awake()
    {
        // 在这里模拟登录成功的情况
        Login();
    }

    private void Login()
    {
        // 检查是否有退出时间记录
        if (PlayerPrefs.HasKey("LogoutTime"))
        {
            // 获取退出时间
            string logoutTimeString = PlayerPrefs.GetString("LogoutTime");
            print(logoutTimeString);
            DateTime logoutTime = DateTime.Parse(logoutTimeString);

            // 计算玩家没有玩游戏的时间差
            TimeSpan timeDifference = DateTime.Now - logoutTime;

            // 计算包含的30分钟数量
            int count = (int)(timeDifference.TotalMinutes / 30);

            // 打印玩家没有玩游戏的时间和包含的30分钟数量
            Debug.Log("玩家没有玩游戏的时间：" + timeDifference);
            Debug.Log("包含的30分钟数量：" + count);

            // 保存包含的30分钟数量到 PlayerPrefs
            PlayerPrefs.SetInt("Count", count);
            PlayerPrefs.Save();
        }

        // 保存当前登录时间到 PlayerPrefs
        PlayerPrefs.SetString("LoginTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        // 保存退出时间到 PlayerPrefs
        PlayerPrefs.SetString("LogoutTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }
}
