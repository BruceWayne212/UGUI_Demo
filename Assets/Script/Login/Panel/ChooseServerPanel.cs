using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class ChooseServerPanel : BasePanel
{
    //左右滚动视图
    public ScrollRect svLeft;
    public ScrollRect svRight;
    //上一次登录的服务器信息
    public Text txtName;
    public Image imgState;
    //当前选择的区间范围
    public Text txtRange;
    //用于存储右侧按钮们
    private List<GameObject> itemList = new List<GameObject>();
    public override void Init()
    {
        //动态的创建左侧按钮

        //获取服务器列表数据
        List<ServerInfo> infoList = LoginMgr.Instance.ServerData;
        //得到一共要循环创建多少个区间按钮
        //由向下取整 所以要 +1 平均分成了num个按钮
        int num = infoList.Count / 5 + 1;
        //循环创建 一个一个的 区间按钮
        for(int i = 0; i < num; i++)
        {
            //动态创建预设体对象
            GameObject item = Instantiate(Resources.Load<GameObject>("UI/ServerLeftItem"));
            item.transform.SetParent(svLeft.content, false);
            //初始化
            ServerLeftItem serverLeft = item.GetComponent<ServerLeftItem>();
            int beginIndex = i * 5 + 1;
            int endIndex = 5 * (i + 1);
            //判断最大 是不超过 服务器总数
            if (endIndex > infoList.Count)
                endIndex = infoList.Count;
            //初始化 区间按钮
            serverLeft.InitInfo(beginIndex, endIndex);

        }

    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己
        //初始化时 更新面板 显示上次也选择的服务器
        
        int id = LoginMgr.Instance.LoginData.frontServerID;
        if (id <= 0)
        {
            txtName.text = "无";
            imgState.gameObject.SetActive(false);

        }
        else
        {
            //获取上一次服务器ID 信息 用于界面数据更新
            ServerInfo info = LoginMgr.Instance.ServerData[id - 1];
            //拼接显示上一次登录的信息
            txtName.text = info.id + "区    " + info.name;
            //一开让状态图 显示
            imgState.gameObject.SetActive(true);
            //状态
            //加载图集
            SpriteAtlas sa = Resources.Load<SpriteAtlas>("Login");
            switch (info.state)
            {
                case 0:
                    imgState.gameObject.SetActive(false);
                    break;
                case 1://流畅
                    imgState.sprite = sa.GetSprite("ui_DL_liuchang_01");
                    break;
                case 2://繁忙
                    imgState.sprite = sa.GetSprite("ui_DL_fanhua_01");
                    break;
                case 3://火爆
                    imgState.sprite = sa.GetSprite("ui_DL_huobao_01");
                    break;
                case 4://维护
                    imgState.sprite = sa.GetSprite("ui_DL_weihu_01");
                    break;
            }
        }
        //更新当前选择的区间
        UpdatePanel(1, 5 > LoginMgr.Instance.ServerData.Count ? LoginMgr.Instance.ServerData.Count : 5);
    }
    /// <summary>
    /// 提供给其他地方 用于更新 当前选择区间的右侧按钮
    /// </summary>
    /// <param name="beginIndex"></param>
    /// <param name="endIndex"></param>
    public void UpdatePanel(int beginIndex,int endIndex)
    {
        //更新服务器显示
        txtRange.text = "服务器 " + beginIndex + "-" + endIndex;
        //第一步;删除之前的按钮
        for(int i = 0; i < itemList.Count; i++)
        {
            //删除之前的对象
            Destroy(itemList[i]);
        }
        //清空列表 删除
        itemList.Clear();
        //创建新的按钮
        for(int i = beginIndex; i <= endIndex; i++)
        {
            //首先获取 服务器信息
            ServerInfo nowInfo = LoginMgr.Instance.ServerData[i-1];
            //动态地创建预设体
            GameObject serverItem = Instantiate(Resources.Load<GameObject>("UI/ServerRightItem"));
            serverItem.transform.SetParent(svRight.content,false);
            //根据信息 更新按钮数据
            ServerRightItem rightItem = serverItem.GetComponent<ServerRightItem>();
            rightItem.InitInfo(nowInfo);
            //记录当前信息
            itemList.Add(serverItem);

        }
    }
}
