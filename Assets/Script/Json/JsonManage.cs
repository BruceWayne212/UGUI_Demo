using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public enum JsonType
{
    JsonUtlity,
    LitJson
}
public class JsonManage 
{
    private static JsonManage instance=new JsonManage();
    public static JsonManage Instance => instance;

    private JsonManage() { }

    public void SaveDate(object data,string fileName,JsonType jsonType=JsonType.LitJson)
    {
        //ȷ�ϴ洢��·��
        string path = Application.persistentDataPath +"/"+ fileName+ ".json";
        //���л� Json�ַ���
        string jsonstr = "";
        switch (jsonType)
        {
            case JsonType.JsonUtlity:
                jsonstr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonstr = JsonMapper.ToJson(data);
                break;
        }
        //�����л����json�洢��ָ��·����
        File.WriteAllText(path, jsonstr);
    }
    public T LoadData<T>(string fileName,JsonType jsonType=JsonType.LitJson) where T:new()
    {
        //ȷ��Ĭ�������ļ��� ·������û���ļ�
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //��������ھ�ȥ��д·����Ѱ��
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //�����д�ļ����ж���û�� �ͷ���һ��Ĭ�϶���
        if(!File.Exists(path))
            return new T();
        //�����л�
        string jsonstr = File.ReadAllText(path);
        //���ݶ���
        T data = default(T);
        switch (jsonType)
        {
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonstr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonstr);
                break;

        }
        //�������ݶ���
        return data;

    }
}
