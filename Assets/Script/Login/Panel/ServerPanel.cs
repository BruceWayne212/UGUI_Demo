using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ServerPanel : BasePanel
{
    //进入游戏 点击换区 返回
    public Button btnStart;
    public Button btnChange;
    public Button btnBack;
    //服务器名称
    public Text txtName;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();
            //隐藏自己 
            UIManager.Instance.HidePanel<ServerPanel>();           
        });
        btnStart.onClick.AddListener(() =>
        {
            //进入游戏
            //由于过场景 canvas对象不会被移除 所以下面的面板 应该也要被隐藏
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
            //隐藏登录背景图面板
            UIManager.Instance.HidePanel<LoginBKPanel>();
            //存储数据的目的 是为了 存储到它当前的服务器id
            LoginMgr.Instance.SaveLoginData();
            //切换场景
            SceneManager.LoadScene("GameScene");
        });
        btnChange.onClick.AddListener(() => {
            //显示服务器面板
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<ServerPanel>();
        });

    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己的时候 更新 当前服务器选择的名字
        //记录上一次登录的服务器ID 来更新内容
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
        {
            txtName.text = "无";
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "区  " + info.name;
        }

    }
}
