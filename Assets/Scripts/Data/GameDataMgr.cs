using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 专门用来管理数据的类
/// </summary>
public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;
    
    // 记录当前选择的数据 用于在之后的游戏场景中创建
    public RoleInfo nowSelRole;
    
    // 音效相关数据
    public MusicData musicData;
    
    // 角色相关数据
    public List<RoleInfo> roleInfoList;
    
    // 玩家相关数据
    public PlayerData playerData;

    // 所有场景数据
    public List<SceneInfo> sceneInfoList;
    
    private GameDataMgr()
    {
        // 初始化默认数据
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        // 读取角色数据
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        // 获取初始化玩家数据
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
    }
    
    /// <summary>
    /// 存储音效数据
    /// </summary>
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }
    
    /// <summary>
    /// 存储玩家数据
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
    
}
