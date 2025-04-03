using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    
    private AudioSource bkSource;

    private void Awake()
    {
        instance = this;
        bkSource = GetComponent<AudioSource>();
        
        // 通过数据 来设置 音乐的大小和开关
        MusicData data = GameDataMgr.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }

    // 开关背景音乐的方法
    public void SetIsOpen(bool isOpen)
    {
        bkSource.mute = !isOpen;
    }
    
    // 调整背景音乐大小的方法
    public void ChangeValue(float v)
    {
        bkSource.volume = v;
    }
}
