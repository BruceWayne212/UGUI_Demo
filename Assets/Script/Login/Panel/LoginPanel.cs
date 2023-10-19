using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    //ע�ᰴť
    public Button btnRegister;
    //ȷ����¼��ť
    public Button btnSure;
    //����� �˺ſؼ� ����ؼ�
    public InputField inputUN;
    public InputField inputPw;
    //��ס���� �Զ���¼
    public Toggle togPw;
    public Toggle togAuto;

    public override void Init()
    {
        //���ע��
        btnRegister.onClick.AddListener(() => {
            //��ʾע�����
            UIManager.Instance.ShowPanel<RegisterPanel>();
            //�����Լ�
            UIManager.Instance.HidePanel<LoginPanel>();
        });
        //���ȷ�� ��¼
        btnSure.onClick.AddListener(() => {
            //��֤�û����˺ź�����
            //�ж������˺����� �Ƿ����
            if (inputUN.text.Length <= 6 || inputPw.text.Length <= 6)
            {
                //��ʾ���Ϸ�
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                //�ı�����ϵ���ʾ����
                tipPanel.ChangeInfo("�˺����붼�������6λ");
                return;
            }
            //��֤ �û����������Ƿ�ͨ�� 
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPw.text))
            {
                //��¼�ɹ�

                //��¼����
                LoginMgr.Instance.LoginData.userName = inputUN.text;
                LoginMgr.Instance.LoginData.passWord = inputPw.text;
                LoginMgr.Instance.LoginData.autoLogin = togAuto.isOn;
                LoginMgr.Instance.LoginData.rememberPw = togPw.isOn;
                LoginMgr.Instance.SaveLoginData();
                //���ݷ�������Ϣ���ж�ѡ���ĸ����
                if (LoginMgr.Instance.LoginData.frontServerID <=0)
                {
                    //�������û��ѡ��������� idΪ-1ʱ Ӧ��ֱ�Ӵ� ѡ�����
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //�򿪷��������
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //�����Լ�
                UIManager.Instance.HidePanel<LoginPanel>();
                 
            }
            else
            {
                //��¼ʧ��
                UIManager.Instance.ShowPanel<TipPanel>().ChangeInfo("�˺Ż��������");
            }
        });

        //�����ס�����߼�
        togPw.onValueChanged.AddListener((isOn) => {
            //��û�й�ѡ��ס����ʱ �Զ���¼���ܱ���ѡ
            if (!isOn)
            {
                togAuto.isOn = false;
            }
        });

        //����Զ���¼�߼�
        togAuto.onValueChanged.AddListener((isOn) =>
        {
            //����ѡ�Զ���¼ʱ Ӧ��Ĭ�Ϲ�ѡ��ס����
            if (isOn)
            {
                togPw.isOn = true;
            }
        });

        
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ�Լ�ʱ �����Լ������� ��ʾ����ϵ�����
        LoginData loginData = LoginMgr.Instance.LoginData;

        //��ʼ�����
        //����������ѡ��
        togPw.isOn = loginData.rememberPw;
        togAuto.isOn = loginData.autoLogin;

        //�����˺�����
        inputUN.text = loginData.userName;
        //�ж��ϴ��Ƿ�ѡ������ ����ס�˺�
        if (togPw.isOn)
            inputPw.text = loginData.passWord;
        //�ж��Ƿ�ѡ�Զ���¼
        if(togAuto.isOn)
        {
            //�Զ���¼��ʲô
            if (LoginMgr.Instance.CheckInfo(inputUN.text, inputPw.text))
            {
                //���ݷ�������Ϣ���ж�ѡ���ĸ����
                if (LoginMgr.Instance.LoginData.frontServerID <= 0)
                {
                    //�������û��ѡ��������� idΪ-1ʱ Ӧ��ֱ�Ӵ� ѡ�����
                    UIManager.Instance.ShowPanel<ChooseServerPanel>();
                }
                else
                {
                    //�򿪷��������
                    UIManager.Instance.ShowPanel<ServerPanel>();
                }

                //�����Լ�
                UIManager.Instance.HidePanel<LoginPanel>(false);
            }
            else
            {
                TipPanel panel = UIManager.Instance.ShowPanel<TipPanel>();
                panel.ChangeInfo("�˺��������");
            }
        }    
    }

    /// <summary>
    /// �ṩ���ⲿ ������õ��û���������
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="passWord"></param>
    public void SetInfo(string userName,string passWord)
    {
        inputUN.text = userName;
        inputPw.text = passWord;
    }

}
