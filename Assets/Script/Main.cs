using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // //��ʾ��ʾ��� ����
        //TipPanel tipPanel= UIManager.Instance.ShowPanel<TipPanel>();
        // //�޸���ʾ����
        // tipPanel.ChangeInfo("�εεεεεε�");

        //һ����Ϸ����ʾ��¼���
        UIManager.Instance.ShowPanel<LoginBKPanel>();
        UIManager.Instance.ShowPanel<LoginPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
