using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 组合控件 主要方便 造塔相关的 UI 逻辑
/// </summary>
public class TowerBtn : MonoBehaviour
{
    public Image imgPic;
    
    public Text txtTip;
    
    public Text txtMoney;

    public void InitInfo(int id, string inputStr)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        imgPic.sprite = Resources.Load<Sprite>(info.imgRes);
        txtMoney.text = "￥" + info.money;
        txtTip.text = inputStr;

        if (info.money > GameLevelMgr.Instance.player.money)
        {
            txtMoney.text = "金钱不足";
        }
    }
}
