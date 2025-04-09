using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHP;
    public Text txtHP;
    
    public Text txtWave;
    public Text txtMoney;

    // hp的初始宽
    public float hpW = 550;

    public Button btnQuit;

    // 下方造塔组合控件的父对象 主要用于控制
    public Transform botTrans;
    
    // 管理3个复合控件
    public List<TowerBtn> towerBtnList = new();
    
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            // 隐藏游戏界面
            UIManager.Instance.HidePanel<GamePanel>();
            // 返回开始界面
            SceneManager.LoadScene("BeginScene");
            // 其他
        });
        
        // 一开始隐藏下方的按钮组合
        botTrans.gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新安全区域血量
    /// </summary>
    /// <param name="hp">当前血量</param>
    /// <param name="maxHP">最大血量</param>
    public void UpdateTowerHP(int hp, int maxHP)
    {
        txtHP.text = hp + "/" + maxHP;
        ((RectTransform)imgHP.transform).sizeDelta = new Vector2((float)hp / maxHP * hpW, 40);
    }

    /// <summary>
    /// 更新剩余波数
    /// </summary>
    /// <param name="nowNum">当前波数</param>
    /// <param name="maxNum">最大波数</param>
    public void UpdateWaveNum(int nowNum, int maxNum)
    {
        txtWave.text = nowNum + "/" + maxNum;
    }

    /// <summary>
    /// 更新金币数量
    /// </summary>
    /// <param name="money">当前获得的金币</param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }
    
}
