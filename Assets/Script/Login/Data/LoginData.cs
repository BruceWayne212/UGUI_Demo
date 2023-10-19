using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录界面可能要记录玩家相关操作数据
/// </summary>
public class LoginData 
{
    //用户名
    public string userName;
    //密码
    public string passWord;
    //是否记住密码
    public bool rememberPw;
    //是否自动登录
    public bool autoLogin;

    //服务器相关
    public int frontServerID=-1;
}
