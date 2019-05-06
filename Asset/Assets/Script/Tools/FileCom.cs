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

        //通过读取流，读取文本中的所有的内容//using 语法，代码完成释放资源，析构？
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

    public static void CreatFile(string filePath, string content)
    {
        DeletFile(filePath);
        using (FileStream fs = File.Create(filePath))
        {
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.Write(content.ToString());
            }
        }
    }


    //追加文件内容
    public static void AppendFileText(string filePath, string contents)
    {
        string content = string.Empty;
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        if (File.Exists(filePath))
        {
            //追加文本
            File.AppendAllText(filePath, contents);
        }
    }

    //清空文本内容
    public static void ClearFileText(string filePath)
    {
        string content = string.Empty;
        if (!File.Exists(filePath))
        {

        }
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
    }


    //删除文件
    public static void DeletFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    public static void CopyDirectory(string sourceName, string destDirName)
    {
        try
        {
            //如果目标文件夹不存在
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceName));
            }
        }
        catch{
            
        }
    }
}
