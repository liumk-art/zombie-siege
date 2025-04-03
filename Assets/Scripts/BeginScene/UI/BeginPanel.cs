using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnQuit;

    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            // 之后会在这里 隐藏自己 显示选角面板
        });

        btnSetting.onClick.AddListener(() =>
        {
            //之后会在这里 显示设置界面
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btnAbout.onClick.AddListener(() =>
        {
            //你可以自己制作一个关于面板 之后在这里显示
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}