using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    // 用于控制面板透明度
    private CanvasGroup canvasGroup;

    // 淡入淡出的速度
    private float alphaSpeed = 10;
    public bool isShow = false;

    // 隐藏面板的回调
    private UnityAction hideCallBack = null;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // 如果没有canvasGroup组件，则添加一个
        if (canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 显示面板的逻辑
    /// </summary>
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }

    /// <summary>
    /// 隐藏面板的逻辑
    /// </summary>
    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        
        hideCallBack = callBack;
    }

    /// <summary>
    /// 注册控件事件的方法 所有子面板都需要重写此方法
    /// </summary>
    public abstract void Init();

    void Update()
    {
        // 显示面板 淡入
        if (isShow && canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        // 隐藏面板 淡出
        else if (!isShow && canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                // 面板淡出完成后再去执行的逻辑
                hideCallBack?.Invoke();
            }
        }
    }
}