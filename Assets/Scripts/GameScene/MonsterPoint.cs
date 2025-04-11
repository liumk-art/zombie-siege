using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    // 怪物有多少波
    public int maxWave;

    // 每波怪物有多少只
    public int monsterNumOneWave;

    // 当前波数还有多少只怪物没有创建
    private int nowNum;

    // 怪物ID
    public List<int> monsterIDs;

    // 当前波数 要创建什么ID的怪物
    private int nowID;

    // 单只怪物创建间隔时间
    public float createOffsetTime;

    // 波于波之间间隔时间
    public float delayTime;

    // 第一波怪物创建的间隔时间
    public float firstDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateWave", firstDelayTime);
        
        // 记录出怪点
        GameLevelMgr.Instance.AddMonsterPoint(this);
        // 更新最大波数
        GameLevelMgr.Instance.UpdategeMaxNum(maxWave);
    }

    /// <summary>
    /// 开始创建一波怪物
    /// </summary>
    private void CreateWave()
    {
        // 得到当前波数怪物的ID
        nowID = monsterIDs[Random.Range(0, monsterIDs.Count)];
        // 当前波数有多少只怪物
        nowNum = monsterNumOneWave;
        // 创建怪物
        CreateMonster();

        // 减少波数
        maxWave--;
        // 通知管卡管理器出了一波怪
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }

    /// <summary>
    /// 创建怪物
    /// </summary>
    /// <returns></returns>
    private void CreateMonster()
    {
        // 直接创建怪物
        // 取出怪物数据
        MonsterInfo info = GameDataMgr.Instance.monsterInfoList[nowID - 1];

        // 创建怪物预设体
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position,
            Quaternion.identity);
        // 添加怪物脚本 并初始化
        MonsterObject monster = obj.AddComponent<MonsterObject>();
        monster.InitInfo(info);

        // 通知管卡管理器出了一只怪
        GameLevelMgr.Instance.ChangeMonsterNum(1);
        // 创建一只怪物后 减少怪物数量
        nowNum--;
        if (nowNum == 0)
        {
            if (maxWave > 0)
            {
                Invoke("CreateWave", delayTime);
            }
        }
        else
        {
            Invoke("CreateMonster", createOffsetTime);
        }
    }

    /// <summary>
    /// 出怪点是否出怪结束
    /// </summary>
    /// <returns></returns>
    public bool CheckOver()
    {
        return maxWave == 0 && nowNum == 0;
    }
}