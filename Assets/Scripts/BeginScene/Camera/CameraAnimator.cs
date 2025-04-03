using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{

    private Animator animator;

    // 用于记录动画播放完成时调用的事件
    private UnityAction overAction;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // 左转
    public void TurnLeft(UnityAction action)
    {
        animator.SetTrigger("Left");
        overAction = action;
    }
    
    // 右转
    public void TurnRight(UnityAction action)
    {
        animator.SetTrigger("Right");
        overAction = action;
    }
    
    
    // 当动画播放完成时调用的事件
    public void PlayerOver()
    {
        overAction?.Invoke();
        overAction = null;
    }
    
}
