using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    private static GameLevelMgr instance = new GameLevelMgr();

    public static GameLevelMgr Instance => instance;

    public PlayerObject player;

    // 所有的出怪点
    private List<MonsterPoint> points = new List<MonsterPoint>();

    // 记录当前怪物波数
    private int nowWaveNum = 0;

    // 记录一共有多少波怪物
    private int maxWaveNum = 0;

    // // 记录当前场景的怪物数量
    // private int nowMonsterNum = 0;
    
    // 记录当前场景上 怪物的列表
    private List<MonsterObject> monsterList = new List<MonsterObject>();

    private GameLevelMgr()
    {
    }

    // 1.切换游戏场景是时 动态创建玩家
    public void InitInfo(SceneInfo info)
    {
        // 显示游戏界面
        UIManager.Instance.ShowPanel<GamePanel>();
        // 玩家的创建
        RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
        // 首先获取到场景当中 玩家的出生位置
        Transform heroPos = GameObject.Find("HeroBornPos").transform;
        // 创建玩家
        GameObject heroObj =
            GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res), heroPos.position, heroPos.rotation);
        // 对玩家对象进行初始化
        player = heroObj.GetComponent<PlayerObject>();
        // 初始化玩家的基础属性
        player.InitPlayerInfo(roleInfo.defaultWeapon, info.money);

        // 让摄像机看到动态生成的玩家
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);

        // 初始化中央保护区的血量
        MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);
    }

    // 2.通过游戏管理器判断游戏是否胜利
    // 用于记录出怪点的方法
    public void AddMonsterPoint(MonsterPoint point)
    {
        points.Add(point);
    }

    /// <summary>
    /// 更新一共有多少波怪
    /// </summary>
    /// <param name="num"></param>
    public void UpdategeMaxNum(int num)
    {
        maxWaveNum += num;
        nowWaveNum = maxWaveNum;
        // 更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    public void ChangeNowWaveNum(int num)
    {
        nowWaveNum -= num;
        // 更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }

    /// <summary>
    /// 检测是否胜利
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!points[i].CheckOver())
                return false;
        }

        if (monsterList.Count > 0)
            return false;

        Debug.Log("游戏胜利");
        return true;
    }

    // /// <summary>
    // /// 改变当前场景上记录的怪物数量
    // /// </summary>
    // /// <param name="num"></param>
    // public void ChangeMonsterNum(int num)
    // {
    //     nowMonsterNum += num;
    // }

    /// <summary>
    /// 记录怪物到列表中
    /// </summary>
    /// <param name="obj"></param>
    public void AddMonster(MonsterObject obj)
    {
        monsterList.Add(obj);
    }
    
    /// <summary>
    /// 用于将怪物从列表中移除 死亡时使用
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveMonster(MonsterObject obj)
    {
        monsterList.Remove(obj);
    }

    /// <summary>
    /// 寻找满足距离条件的单体怪物
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public MonsterObject FindMonster(Vector3 pos, int range)
    {
        // 在怪物列表中找到满足距离的怪物返回出去
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (!monsterList[i].isDead && Vector3.Distance(monsterList[i].transform.position, pos) < range)
                return monsterList[i];
        }

        return null;
    }

    /// <summary>
    /// 寻找满足条件的所有怪物
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public List<MonsterObject> FindMonsters(Vector3 pos, int range)
    {
        // 去寻找满足条件的所有怪物 并且把他们记录在一个列表中
        List<MonsterObject> list = new List<MonsterObject>();
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (!monsterList[i].isDead && Vector3.Distance(monsterList[i].transform.position, pos) < range)
            {
                list.Add(monsterList[i]);
            }
        }
        return list;
    }
    
    /// <summary>
    /// 清空当前关卡数据 以防影响下个关卡数据
    /// </summary>
    public void ClearInfo()
    {
        points.Clear();
        monsterList.Clear();
        nowWaveNum = maxWaveNum = 0;
    }
    
}