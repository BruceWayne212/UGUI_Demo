using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    //ȷ�� ȡ����ť
    public Button btnSure;
    public Button btnCancel;
    //���� �˺� ���� �ؼ�
    public InputField inputUN;
    public InputField inputPw;

    public override void Init()
    {
        btnCancel.onClick.AddListener(() => {
            //�����Լ�
            UIManager.Instance.HidePanel<RegisterPanel>();
            //��ʾ��¼���
            UIManager.Instance.ShowPanel<LoginPanel>();

        });
        btnSure.onClick.AddListener(() =>
        {
        //�ж������˺����� �Ƿ����
        if (inputUN.text.Length <= 6 || inputPw.text.Length <= 6)
            {
                //��ʾ���Ϸ�
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                //�ı�����ϵ���ʾ����
                tipPanel.ChangeInfo("�˺����붼�������6λ");
                return;
            }
            //ȥע���˺�����
            if (LoginMgr.Instance.RegisterUser(inputUN.text, inputPw.text))
            {
                //�������� ������ע���˺ŵ��������� ��Ȼ�������һ���˺ŵ��������
                LoginMgr.Instance.ClearLoginData();
                //ע��ɹ� ���µ�¼����ϵ� �û���������
                LoginPanel loginPanel = UIManager.Instance.ShowPanel<LoginPanel>();
                //��������� �û���������
                loginPanel.SetInfo(inputUN.text, inputPw.text);

                //�����Լ�
                UIManager.Instance.HidePanel<RegisterPanel>();
            }
            else
            {
                //��ʾ��ʾ��� ��ʾ�����û����Ѵ���
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.ChangeInfo("�û����Ѵ���");
                //���������������
                inputUN.text = "";
                inputPw.text = "";
            }




        });
    }
}
