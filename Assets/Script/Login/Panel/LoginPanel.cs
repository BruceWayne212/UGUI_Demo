using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    //注册按钮
    public Button btnRegister;
    //确定登录按钮
    public Button btnSure;
    //输入的 账号控件 密码控件
    public InputField inputUN;
    public InputField inputPw;
    //记住密码 自动登录
    public Toggle togPw;
    public Toggle togAuto;

    public override void Init()
    {
        //点击注册
        btnRegister.onClick.AddListener(() => {
            //显示注册面板
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //隐藏自己
            UIManager.Instance.HidePanel<LoginPanel>();
        });
        //点击确定 登录
        btnSure.onClick.AddListener(() => {
            //验证用户的账号和密码
            //判断输入账号密码 是否合理
            if (inputUN.text.Length <= 6 || inputPw.text.Length <= 6)
            {
                //提示不合法
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变面板上的提示内容
                tipPanel.ChangeInfo("账号密码都必须大于6位");
                return;
            }
            //验证 用户名和密码是否通过 
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPw.text))
            {
                //登录成功

                //记录数据
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.passWord = inputPw.text;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.LoginData.rememberPw = togPw.isOn;
                LoginMgr.Instance.SaveLoginData();
                //根据服务器信息来判断选择哪个面板
                if (LoginMgr.Instance.LoginData.frontServerID <=0)
                {
                    //如果从来没有选择过服务器 id为-1时 应该直接打开 选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>();
                 
            }
            else
            {
                //登录失败
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("账号或密码错误");
            }
        });

        //点击记住密码逻辑
        togPw.onValueChanged.AddListener((isOn) => {
            //当没有勾选记住密码时 自动登录不能被勾选
            if (!isOn)
            {
                togAuto.isOn = false;
            }
        });

        //点击自动登录逻辑
        togAuto.onValueChanged.AddListener((isOn) =>
        {
            //当勾选自动登录时 应该默认勾选记住密码
            if (isOn)
            {
                togPw.isOn = true;
            }
        });

        
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己时 根据自己的数据 显示面板上的内容
        LoginData loginData = LoginMgr.Instance.LoginData;

        //初始化面板
        //更新两个多选框
        togPw.isOn = loginData.rememberPw;
        togAuto.isOn = loginData.autoLogin;

        //更新账号密码
        inputUN.text = loginData.userName;
        //判断上次是否勾选了密码 来记住账号
        if (togPw.isOn)
            inputPw.text = loginData.passWord;
        //判断是否勾选自动登录
        if(togAuto.isOn)
        {
            //自动登录做什么
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPw.text))
            {
                //根据服务器信息来判断选择哪个面板
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //如果从来没有选择过服务器 id为-1时 应该直接打开 选服面板
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //打开服务器面板
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //隐藏自己
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("账号密码错误");
            }
        }    
    }

    /// <summary>
    /// 提供给外部 快捷设置的用户名和密码
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    public void SetInfo(string userName,string passWord)
    {
        inputUN.text = userName;
        inputPw.text = passWord;
    }

}
