using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr 
{
    private static LoginMgr instance=new LoginMgr();
    public static LoginMgr Instance => instance;
    //��̬�������õ�����ȥnew 
    private  LoginData loginData;
    //�������� ���������ȡ
    public  LoginData LoginData=>loginData;

    //ע������
    private RegisterData registerData;
    public RegisterData RegisterData => registerData;

    //����������
    private List<ServerInfo> serverData;
    public List<ServerInfo> ServerData => serverData;

    private LoginMgr()
    {
        //ͨ��json����������ȡ��Ӧ������
       loginData= JsonManage.Instance.LoadData<LoginData>("LoginData");
        //��ȡע������
        registerData = JsonManage.Instance.LoadData<RegisterData>("RegisterData");
        //��ȡ����������
        serverData = JsonManage.Instance.LoadData<List<ServerInfo>>("ServerInfo");
    }

    #region ��¼����
    //�洢��¼�������
    public void SaveLoginData()
    {
        JsonManage.Instance.SaveDate(loginData, "LoginData");
    }
    //��Ҫ����ע��ɹ��������¼����
    public void ClearLoginData()
    {
        loginData.frontServerID = 0;
        loginData.rememberPw = false;
        loginData.autoLogin = false;
    }

    #endregion
    #region ע������
    //�洢ע������
    public void SaveRegisterData()
    {
        JsonManage.Instance.SaveDate(registerData, "RegisterData");
    }
    //ע�᷽��
    public bool RegisterUser(string userName,string passWord)
    {
        //�ж��Ƿ��Ѿ������û�
        if (registerData.registerInfo.ContainsKey(userName))
            return false;
        //���������֤������ע�� �洢�˺�����
        registerData.registerInfo.Add(userName, passWord);
        //�洢������
        SaveRegisterData();
        //ע��ɹ�
        return true;
    }
    //��֤�û��� ������
    public bool CheckInfo(string userName,string passWord)
    {
        //�ж��û��Ƿ����
        if (registerData.registerInfo.ContainsKey(userName))
        {
            //������ͬ ֤�� ��¼�ɹ�
            if (registerData.registerInfo[userName] == passWord)
            {
                return true;
            }
        }
        //�û��������벻�Ϸ�
        return false;
    }
    #endregion
}
