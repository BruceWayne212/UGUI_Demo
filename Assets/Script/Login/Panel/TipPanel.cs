using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    //ȷ����ť
    public Button btnSure;
    //��ʾ����
    public Text textInfo;
    public override void Init()
    {
        //��ʼ����ť�����¼�
        btnSure.onClick.AddListener(() =>
        {
            //�������
            UIManager.Instance.HidePanel<TipPanel>();

        });
    }
    /// <summary>
    /// ��ʾ���ݸı� ���ⲿʹ��
    /// </summary>
    /// <param name="info">��ʾ����</param>
    public void ChangeInfo(string info)
    {
        textInfo.text = info;
    }
}
