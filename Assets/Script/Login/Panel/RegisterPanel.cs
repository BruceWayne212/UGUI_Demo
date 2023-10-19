using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    //确定 取消按钮
    public Button btnSure;
    public Button btnCancel;
    //输入 账号 密码 控件
    public InputField inputUN;
    public InputField inputPw;

    public override void Init()
    {
        btnCancel.onClick.AddListener(() => {
            //隐藏自己
            UIManager.Instance.HidePanel<RegisterPanel>();
            //显示登录面板
            UIManager.Instance.ShowPanel<LoginPanel>();

        });
        btnSure.onClick.AddListener(() =>
        {
        //判断输入账号密码 是否合理
        if (inputUN.text.Length <= 6 || inputPw.text.Length <= 6)
            {
                //提示不合法
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                //改变面板上的提示内容
                tipPanel.ChangeInfo("账号密码都必须大于6位");
                return;
            }
            //去注册账号密码
            if (LoginMgr.Instance.RegisterUser(inputUN.text, inputPw.text))
            {
                //清理数据 用于新注册账号的数据重置 不然会残留上一个账号的相关数据
                LoginMgr.Instance.ClearLoginData();
                //注册成功 更新登录面板上的 用户名和密码
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                //更新面板上 用户名和密码
                loginPanel.SetInfo(inputUN.text, inputPw.text);

                //隐藏自己
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //显示提示面板 提示别人用户名已存在
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("用户名已存在");
                //方便别人重新输入
                inputUN.text = "";
                inputPw.text = "";
            }




        });
    }
}
