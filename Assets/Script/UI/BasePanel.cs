using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //�������嵭�뵭���Ļ����� ���
    private CanvasGroup canvasGroup;
    //���뵭�����ٶ�
    private float AlphSpeed = 10f;
    //�Ƿ� ��ʾ/�����Լ�
    private bool isShow;
    //���Լ������ɹ�ʱ Ҫִ�еĺ���
    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        //һ��ʼ��ȡ����Ϲ��ص���� ���û�� ����ͨ������Ϊ�����һ��
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }
    protected virtual void Start()
    {
        Init();
    }
     public abstract void Init();
    
    /// <summary>
    /// �����Լ�ʱ ������
    /// </summary>
    public virtual void HideMe(UnityAction callback)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        //��¼ ���� �������ɹ����ִ�еĺ���
        hideCallBack = callback;
    }
    /// <summary>
    /// ��ʾ�Լ�ʱ ������
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    private void Update()
    {
        //����
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += Time.deltaTime * AlphSpeed;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //����
        else if (!isShow)
        {
            canvasGroup.alpha -= Time.deltaTime * AlphSpeed;
            if (canvasGroup.alpha <=0)
            {
                canvasGroup.alpha = 0;
                //��Ӧ�ù�����ɾ���Լ�
                hideCallBack?.Invoke();
            }
        }

    }
}
