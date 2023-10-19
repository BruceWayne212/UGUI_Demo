using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance=new UIManager();
    public static UIManager Instance => instance;
    //�洢��������
    Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    //Ӧ�ô�һ��ʼ�ͻ�ȡ�����ǵ�canvas����
    private Transform canvasTrans;
    private UIManager()
    {
        //�õ������ж���
        canvasTrans = GameObject.Find("Canvas").transform;
        //��canvas������������Ƴ�
        //ͨ����̬ ����/ɾ�� �� ��ʾ/������� ���Բ�ɾ��Ӱ�첻��
        GameObject.DontDestroyOnLoad(canvasTrans.gameObject);
    }
    
    //��ʾ���
    public T ShowPanel<T>()where T : BasePanel
    {
        //��Ҫ��֤ ����T������ �������һ�� ��һ�������Ĺ��򷽱����ǵ�ʹ��
        string panelName = typeof(T).Name;

        //�Ƿ��Ѿ�����ʾ�ŵĸ������ �����ֱ�ӷ��� �����ٴ�����
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //��ʾ��� ��̬����Ԥ���� ���ø�����
        //���ݵõ������� ����Ԥ���� ����� ֱ�Ӷ�̬��������
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        //��ȡ����ϵĽű�
        T panel = panelObj.GetComponent<T>();
        //�����ű��洢����Ӧ��������
        panelDic.Add(panelName, panel);
        //���ó���ʾ�Լ����߼�
        panel.ShowMe();
        return panel;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade">���ϣ������ Ĭ�ϴ�true
    /// ���ϣ��ֱ�����أ�ɾ������� ��flase
    /// </param>
    public void HidePanel<T>(bool isFade=true)
    {
        //���ݷ������� �õ��������
        string panelName = typeof(T).Name;
        //�жϵ�ǰ��ʾ����� ��û�и����ֵ����
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(()=> {
                    //��嵭���� ɾ�����
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //������������Ƴ����
                    panelDic.Remove(panelName);
                
                });
            }
            else
            {
                //ֱ��ɾ�����
                GameObject.Destroy(panelDic[panelName].gameObject);
                //������������Ƴ����
                panelDic.Remove(panelName);
            }
        }
    }
    //������
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;
    }

}
