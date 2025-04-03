using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;
    
    public override void Init()
    {
        // 初始换面板显示内容
        MusicData data = GameDataMgr.Instance.musicData;
        // 初始化开关控件的状态
        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;
        // 初始化滑块控件的值
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;
        
        btnClose.onClick.AddListener(() =>
        {
            // 隐藏设置面板
            UIManager.Instance.HidePanel<SettingPanel>();
            
            // 只有设置完成后 即关闭面板时才会保存数据
            GameDataMgr.Instance.SaveMusicData();
        });

        togMusic.onValueChanged.AddListener((v) =>
        {
            // 设置背景音乐开关
            BKMusic.Instance.SetIsOpen(v);
            // 记录音乐开关的数据
            GameDataMgr.Instance.musicData.musicOpen = v;
        });
        
        togSound.onValueChanged.AddListener((v) =>
        {
            // 记录音效开关的数据
            GameDataMgr.Instance.musicData.soundOpen = v;
        });
        
        sliderMusic.onValueChanged.AddListener((v) =>
        {
            // 设置背景音乐大小
            BKMusic.Instance.ChangeValue(v);
            // 记录音乐大小数据
            GameDataMgr.Instance.musicData.musicValue = v;
        });
        
        sliderSound.onValueChanged.AddListener((v) =>
        {
            // 记录音效大小数据
            GameDataMgr.Instance.musicData.soundValue = v;
        });
    }
}
