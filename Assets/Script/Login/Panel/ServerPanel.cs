using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ServerPanel : BasePanel
{
    //������Ϸ ������� ����
    public Button btnStart;
    public Button btnChange;
    public Button btnBack;
    //����������
    public Text txtName;
    public override void Init()
    {
        btnBack.onClick.AddListener(() =>
        {
            if (LoginMgr.Instance.LoginData.autoLogin)
                LoginMgr.Instance.LoginData.autoLogin = false;
            //��ʾ��¼���
            UIManager.Instance.ShowPanel<LoginPanel>();
            //�����Լ� 
            UIManager.Instance.HidePanel<ServerPanel>();           
        });
        btnStart.onClick.AddListener(() =>
        {
            //������Ϸ
            //���ڹ����� canvas���󲻻ᱻ�Ƴ� ������������ Ӧ��ҲҪ������
            //�����Լ�
            UIManager.Instance.HidePanel<ServerPanel>();
            //���ص�¼����ͼ���
            UIManager.Instance.HidePanel<LoginBKPanel>();
            //�洢���ݵ�Ŀ�� ��Ϊ�� �洢������ǰ�ķ�����id
            LoginMgr.Instance.SaveLoginData();
            //�л�����
            SceneManager.LoadScene("GameScene");
        });
        btnChange.onClick.AddListener(() => {
            //��ʾ���������
            UIManager.Instance.ShowPanel<ChooseServerPanel>();
            //�����Լ�
            UIManager.Instance.HidePanel<ServerPanel>();
        });

    }
    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ�Լ���ʱ�� ���� ��ǰ������ѡ�������
        //��¼��һ�ε�¼�ķ�����ID ����������
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
        {
            txtName.text = "��";
        }
        else
        {
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            txtName.text = info.id + "��  " + info.name;
        }

    }
}
