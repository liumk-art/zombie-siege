using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 摄像机要看向的目标位置
    public Transform target;
    // 摄像机相对目标对象 在xyz上的偏移位置
    public Vector3 offsetPos;

    // 看向位置的y偏移值
    public float bodyHeight;

    // 移动和旋转速度
    public float moveSpeed;
    public float rotationSpeed;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (target == null)
            return;
        // 根据目标对象 来计算 摄像机当前位置和角度
        
        // 位置的计算
        // 向后偏移Z坐标
        Vector3 targetPos = target.position + target.forward * offsetPos.z;
        // 向上偏移Y坐标
        targetPos += Vector3.up * offsetPos.y;
        // 左右偏移X坐标
        targetPos += target.right * offsetPos.x;
        
        // 插值运算 让摄像机 不停向目标靠拢
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, moveSpeed * Time.deltaTime);
        
        // 旋转的计算
        Quaternion targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight - this.transform.position);
        // 让摄像机不停的向目标角度靠拢
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 设置摄像机看向的目标对象
    /// </summary>
    /// <param name="player"></param>
    public void SetTarget(Transform player)
    {
        target = player;
    }
    
}
