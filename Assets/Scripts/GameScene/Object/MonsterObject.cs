using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    // 动画相关
    private Animator animator;

    // 位移相关 寻路组件
    private NavMeshAgent agent;

    // 一些不变的基础数据
    private MonsterInfo monsterInfo;

    // 当前血量
    private int hp;

    // 怪物是否死亡
    public bool isDead = false;

    // 上一次攻击的时间
    private float frontTime = 0;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    // 初始化
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        // 状态机加载
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        // 要变的当前血量
        hp = info.hp;
        // 速度和加速度赋值
        agent.speed = agent.angularSpeed = info.moveSpeed;
        // 旋转速度
        agent.angularSpeed = info.roundSpeed;
    }

    // 受伤
    public void Wound(int dmg)
    {
        // 减少血量
        hp -= dmg;
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            // 死亡
            Dead();
        }
        else
        {
            // 播放音效
        }
    }

    // 死亡
    public void Dead()
    {
        isDead = true;
        // 停止移动
        agent.isStopped = true;
        // 播放死亡动画
        animator.SetBool("Dead", true);
        // 播放音效

        // 加钱
    }

    // 死亡动画播放完毕后调用的方法
    public void DeadEvent()
    {
        // 死亡动画播放完毕后移除对象
        GameLevelMgr.Instance.ChangeMonsterNum(-1);
        Destroy(this.gameObject);
        
        // 怪物死亡时 检测游戏是否胜利
        if (GameLevelMgr.Instance.CheckOver())
        {
            // 显示结束界面
            GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(GameLevelMgr.Instance.player.money, true);
        }

        
    }

    // 出生过后再移动
    // 移动-寻路组件
    public void BornOver()
    {
        // 出生结束后 再让怪物朝目标点移动
        agent.SetDestination(MainTowerObject.Instance.transform.position);

        // 播放移动动画
        animator.SetBool("Run", true);
    }

    void Update()
    {
        // 攻击-伤害检测
        if (isDead)
            return;

        // 根据速度 来决定动画播放什么
        animator.SetBool("Run", agent.velocity != Vector3.zero);
        // 检测和目标点达到移动条件时 就攻击
        if (Vector3.Distance(this.transform.position, MainTowerObject.Instance.transform.position) < 5 &&
            Time.time - frontTime > monsterInfo.atkOffset)
        {
            // 记录这一次攻击的时间
            frontTime = Time.time;
            // 攻击
            animator.SetTrigger("Atk");
        }
    }

    // 伤害检测
    public void AtkEvent()
    {
        // 范围检测 进行伤害判断
        Collider[] colliders = Physics.OverlapSphere(
            this.transform.position + this.transform.forward + this.transform.up, 1,
            1 << LayerMask.NameToLayer("MainTower"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if (MainTowerObject.Instance.gameObject == colliders[i].gameObject)
            {
                // 让保护区域受到伤害
                MainTowerObject.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}