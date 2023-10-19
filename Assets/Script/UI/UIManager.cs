using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance=new UIManager();
    public static UIManager Instance => instance;
    //存储面板的容器
    Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    //应该从一开始就获取到我们的canvas对象
    private Transform canvasTrans;
    private UIManager()
    {
        //得到场景中对象
        canvasTrans = GameObject.Find("Canvas").transform;
        //让canvas对象过场景不移除
        //通过动态 创建/删除 来 显示/隐藏面板 所以不删除影响不大
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }
    
    //显示面板
    public T ShowPanel<T>()where T : BasePanel
    {
        //需要保证 泛型T的类型 和面板名一致 定一个这样的规则方便我们的使用
        string panelName = typeof(T).Name;

        //是否已经有显示着的该面板了 如果有直接返回 不用再创建了
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //显示面板 动态创建预设体 设置父对象
        //根据得到的类名 就是预设体 面板名 直接动态创建即可
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        //获取面板上的脚本
        T panel = panelObj.GetComponent<T>();
        //将面板脚本存储到对应的容器中
        panelDic.Add(panelName, panel);
        //调用出显示自己的逻辑
        panel.ShowMe();
        return panel;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade">如果希望淡出 默认传true
    /// 如果希望直接隐藏（删除）面板 传flase
    /// </param>
    public void HidePanel<T>(bool isFade=true)
    {
        //根据泛型类型 得到面板名字
        string panelName = typeof(T).Name;
        //判断当前显示的面板 有没有该名字的面板
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(()=> {
                    //面板淡出后 删除面板
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //从面板容器中移除面板
                    panelDic.Remove(panelName);
                
                });
            }
            else
            {
                //直接删除面板
                GameObject.Destroy(panelDic[panelName].gameObject);
                //从面板容器中移除面板
                panelDic.Remove(panelName);
            }
        }
    }
    //获得面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;
    }

}
