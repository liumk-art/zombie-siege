using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Animator animator;

    // 1.玩家属性的初始化
    // 玩家攻击力
    private int atk;

    // 玩家拥有的钱
    public int money;

    // 旋转的速度
    public float roundSpeed = 50;

    // 持枪对象才有的开火点
    public Transform gunPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 初始化玩家基础属性
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="money"></param>
    public void InitPlayerInfo(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        
        // 更新界面上钱的数量
        UpdateMoney();
    }

    void Update()
    {
        // 2.移动变化 动作变化
        // 移动动作的变换 由于动作有位移 直接使用动画系统的位移来处理
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        // 旋转
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * roundSpeed * Time.deltaTime, 0);

        // 蹲下
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetLayerWeight(1, 0);
        }
        
        // 打滚
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger("Roll");
        }
        
        // 攻击
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Fire");
        }
    }

    // 3.攻击动作的不同处理
    /// <summary>
    /// 专门用于处理刀武器攻击动作的伤害监测事件
    /// </summary>
    public void KnifeEvent()
    {
        // 进行伤害监测
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1,
            1 << LayerMask.NameToLayer("Monster"));

        // 对应怪物脚本逻辑
        for (int i = 0; i < colliders.Length; i++)
        {
            // 得到怪物碰撞体 让其受伤
            MonsterObject monster = colliders[i].GetComponent<MonsterObject>();
            if (monster!= null)
                monster.Wound(atk);
        }
    }

    /// <summary>
    /// 专门用于处理枪武器攻击动作的伤害监测事件
    /// </summary>
    public void ShootEvent()
    {
        // 进行射线检测
        RaycastHit[] hits = Physics.RaycastAll(new Ray(gunPoint.position, gunPoint.forward), 1000, 1 << LayerMask.NameToLayer("Monster"));
        
        for (int i = 0; i < hits.Length; i++)
        {
            // 获取到碰撞体
            MonsterObject monster = hits[i].collider.GetComponent<MonsterObject>();
            // 对应怪物脚本逻辑
            if (monster != null)
            {
                monster.Wound(atk);
                break;
            }
               
        }
    }
    
    // 4.钱变化的逻辑
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// 提供给外部加钱的方法
    /// </summary>
    /// <param name="money"></param>
    public void AddMoney(int money)
    {
        // 加钱
        this.money += money;
        UpdateMoney();
    }
}