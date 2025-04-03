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
            // 隐藏自己 显示选角面板
        });
        
        btnSetting.onClick.AddListener(() =>
        {
            // 显示设置面板
        });
        
        btnAbout.onClick.AddListener(() =>
        {
            // 显示关于面板
        });
        
        btnQuit.onClick.AddListener(() =>
        {
            // 退出游戏
            Application.Quit();
        });
    }
    
}
