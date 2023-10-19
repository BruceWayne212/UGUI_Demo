using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLeftItem : MonoBehaviour
{
    //��ť�Լ�
    public Button btnSelf;
    //��ʾ��������
    public Text txtInfo;

    //���䷶Χ
    private int beginIndex;
    private int endIndex;

    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //֪ͨѡ����� �ı��Ҳ���ʾ������
            ChooseServerPanel panel = UIManager.Instance.GetPanel<ChooseServerPanel>();
            panel.UpdatePanel(beginIndex, endIndex);
        });
    }

    public void InitInfo(int beginIndex,int endIndex)
    {
        //��¼��ǰ���䰴ť������ֵ
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;

        //��������ʾ�����ݸ�����
        txtInfo.text = beginIndex + "-" + endIndex + "��";

    }
}
