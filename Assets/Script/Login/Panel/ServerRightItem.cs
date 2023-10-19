using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerRightItem : MonoBehaviour
{
    //��ť����
    public Button btnSelf;
    //�Ƿ��·�
    public Image isNew;
    //������״̬
    public Image imgState;
    //����������
    public Text txtName;
    //��ǰ��ť �����ĸ������� ֮�� ��ʹ�����е�����
    public ServerInfo nowServerInfo;

    // Start is called before the first frame update
    void Start()
    {
        btnSelf.onClick.AddListener(() =>
        {
            //��¼��ǰ��ѡ��ķ�����
            LoginMgr.Instance.LoginData.frontServerID = nowServerInfo.id;
            //�������
            UIManager.Instance.HidePanel<ChooseServerPanel>();
            //��ʾ������ѡ�����
            UIManager.Instance.ShowPanel<ServerPanel>();
        });
        
    }
    /// <summary>
    /// ��ʼ������ ���� ���°�ť��ʾ���
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(ServerInfo info)
    {
        //��¼������
        nowServerInfo = info;
        //���°�ť����ʾ����Ϣ
        txtName.text = info.id + "��  " + info.name;
        //�Ƿ��·�
        isNew.gameObject.SetActive(info.isNew);
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
}
