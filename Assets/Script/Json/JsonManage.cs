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
        //确认存储的路径
        string path = Application.persistentDataPath +"/"+ fileName+ ".json";
        //序列化 Json字符串
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
        //将序列化后的json存储到指定路径中
        File.WriteAllText(path, jsonstr);
    }
    public T LoadData<T>(string fileName,JsonType jsonType=JsonType.LitJson) where T:new()
    {
        //确认默认数据文件夹 路径下有没有文件
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //如果不存在就去读写路径下寻找
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //如果读写文件夹中都还没有 就返回一个默认对象
        if(!File.Exists(path))
            return new T();
        //反序列化
        string jsonstr = File.ReadAllText(path);
        //数据对象
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
        //返回数据对象
        return data;

    }
}
