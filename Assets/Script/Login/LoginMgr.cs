using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr 
{
    private static LoginMgr instance=new LoginMgr();
    public static LoginMgr Instance => instance;
    //静态变量不用到外面去new 
    private  LoginData loginData;
    //公共属性 方便外面获取
    public  LoginData LoginData=>loginData;

    //注册数据
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;

    //服务器数据
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData => serverData;

    private LoginMgr()
    {
        //通过json管理器来获取对应的数据
       loginData= JsonManage.Instance.LoadData<LoginData>("LoginData");
        //读取注册数据
        registerData = JsonManage.Instance.LoadData<RegisterData>("RegisterData");
        //读取服务器数据
        serverData = JsonManage.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region 登录数据
    //存储登录数据相关
    public void SaveLoginData()
    {
        JsonManage.Instance.SaveDate(loginData, "LoginData");
    }
    //主要用于注册成功后清理登录数据
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.rememberPw = false;
        loginData.autoLogin = false;
    }

    #endregion
    #region 注册数据
    //存储注册数据
    public void SaveRegisterData()
    {
        JsonManage.Instance.SaveDate(registerData, "RegisterData");
    }
    //注册方法
    public bool RegisterUser(string userName,string passWord)
    {
        //判断是否已经存在用户
        if (registerData.registerInfo.ContainsKey(userName))
            return false;
        //如果不存在证明可以注册 存储账号密码
        registerData.registerInfo.Add(userName, passWord);
        //存储到本地
        SaveRegisterData();
        //注册成功
        return true;
    }
    //验证用户名 和密码
    public bool CheckInfo(string userName,string passWord)
    {
        //判断用户是否存在
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //密码相同 证明 登录成功
            if (registerData.registerInfo[userName] == passWord)
            {
                return true;
            }
        }
        //用户名和密码不合法
        return false;
    }
    #endregion
}
