using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    //确定按钮
    public Button btnSure;
    //提示内容
    public Text textInfo;
    public override void Init()
    {
        //初始化按钮监听事件
        btnSure.onClick.AddListener(() =>
        {
            //隐藏面板
            UIManager.Instance.HidePanel<TipPanel>();

        });
    }
    /// <summary>
    /// 提示内容改变 供外部使用
    /// </summary>
    /// <param name="info">提示内容</param>
    public void ChangeInfo(string info)
    {
        textInfo.text = info;
    }
}
