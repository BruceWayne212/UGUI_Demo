using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class ChooseServerPanel : BasePanel
{
    //���ҹ�����ͼ
    public ScrollRect svLeft;
    public ScrollRect svRight;
    //��һ�ε�¼�ķ�������Ϣ
    public Text txtName;
    public Image imgState;
    //��ǰѡ������䷶Χ
    public Text txtRange;
    //���ڴ洢�Ҳఴť��
    private List<GameObject> itemList = new List<GameObject>();
    public override void Init()
    {
        //��̬�Ĵ�����ఴť

        //��ȡ�������б�����
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        //�õ�һ��Ҫѭ���������ٸ����䰴ť
        //������ȡ�� ����Ҫ +1 ƽ���ֳ���num����ť
        int num = infoList.Count / 5 + 1;
        //ѭ������ һ��һ���� ���䰴ť
        for(int i = 0; i < num; i++)
        {
            //��̬����Ԥ�������
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));
            item.transform.SetParent(svLeft.content, false);
            //��ʼ��
            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            //�ж���� �ǲ����� ����������
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            //��ʼ�� ���䰴ť
            serverLeft.InitInfo(beginIndex, endIndex);

        }

    }
    public override void ShowMe()
    {
        base.ShowMe();
        //��ʾ�Լ�
        //��ʼ��ʱ ������� ��ʾ�ϴ�Ҳѡ��ķ�����
        
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
        {
            txtName.text = "��";
            imgState.gameObject.SetActive(false);

        }
        else
        {
            //��ȡ��һ�η�����ID ��Ϣ ���ڽ������ݸ���
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            //ƴ����ʾ��һ�ε�¼����Ϣ
            txtName.text = info.id + "��    " + info.name;
            //һ����״̬ͼ ��ʾ
            imgState.gameObject.SetActive(true);
            //״̬
            //����ͼ��
            SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
            switch (info.state)
            {
                case 0:
                    imgState.gameObject.SetActive(false);
                    break;
                case 1://����
                    imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                    break;
                case 2://��æ
                    imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                    break;
                case 3://��
                    imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                    break;
                case 4://ά��
                    imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        //���µ�ǰѡ�������
        UpdatePanel(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
    }
    /// <summary>
    /// �ṩ�������ط� ���ڸ��� ��ǰѡ��������Ҳఴť
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex,int endIndex)
    {
        //���·�������ʾ
        txtRange.text = "������ " + beginIndex + "-" + endIndex;
        //��һ��;ɾ��֮ǰ�İ�ť
        for(int i = 0; i < itemList.Count; i++)
        {
            //ɾ��֮ǰ�Ķ���
            Destroy(itemList[i]);
        }
        //����б� ɾ��
        itemList.Clear();
        //�����µİ�ť
        for(int i = beginIndex; i <= endIndex; i++)
        {
            //���Ȼ�ȡ ��������Ϣ
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i-1];
            //��̬�ش���Ԥ����
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(svRight.content,false);
            //������Ϣ ���°�ť����
            ServerRightItem rightItem = serverItem.GetComponent<ServerRightItem>();
            rightItem.InitInfo(nowInfo);
            //��¼��ǰ��Ϣ
            itemList.Add(serverItem);

        }
    }
}
