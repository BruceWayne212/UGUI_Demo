using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //控制整体淡入淡出的画布组 组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float AlphSpeed = 10f;
    //是否 显示/隐藏自己
    private bool isShow;
    //当自己淡出成功时 要执行的函数
    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        //一开始获取面板上挂载的组件 如果没有 我们通过代码为它添加一个
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }
    protected virtual void Start()
    {
        Init();
    }
     public abstract void Init();
    
    /// <summary>
    /// 隐藏自己时 做的事
    /// </summary>
    public virtual void HideMe(UnityAction callback)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        //记录 传入 当淡出成功后会执行的函数
        hideCallBack = callback;
    }
    /// <summary>
    /// 显示自己时 做的事
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    private void Update()
    {
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += Time.deltaTime * AlphSpeed;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow)
        {
            canvasGroup.alpha -= Time.deltaTime * AlphSpeed;
            if (canvasGroup.alpha <=0)
            {
                canvasGroup.alpha = 0;
                //让应用管理器删掉自己
                hideCallBack?.Invoke();
            }
        }

    }
}
