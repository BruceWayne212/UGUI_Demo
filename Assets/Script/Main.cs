using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // //显示提示面板 测试
        //TipPanel tipPanel= UIManager.Instance.ShowPanel<TipPanel>();
        // //修改提示内容
        // tipPanel.ChangeInfo("滴滴滴滴滴滴滴");

        //一进游戏就显示登录面板
        UIManager.Instance.ShowPanel<LoginBKPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
