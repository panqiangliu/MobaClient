using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileCom
{
    //获取文件的内容
    public static string GetFileText(string filePath)
    {
        string connect = string.Empty;
        if (!File.Exists(filePath))
        {
            return connect;
        }

        //通过读取流，读取文本中的所有的内容
        using (StreamReader sr = File.OpenText(filePath))
        {
            connect = sr.ReadToEnd();
        }
        return connect;
    }

    public static void CreatFile(string filePath)
    {
        string connect = string.Empty;
        DeletFile(filePath);
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }

    public static void DeletFile(string filePath)
    {

    }

}
