using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    // 血量相关
    private int hp;
    private int maxHp;
    // 是否死亡
    private bool isDead;

    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    
    
    // 更新血量
    public void UpdateHp(int hp, int maxHP)
    {
        this.hp = hp;
        this.maxHp = maxHP;
        
        // 更新界面上血量的展示
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHP(hp, maxHP);
    }
    
    // 自己受到伤害
    public void Wound(int dmg)
    {
        // 保护区域死亡
        if (isDead) return;
        
        // 受到伤害
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            // 游戏结束
        }
        
        UpdateHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
