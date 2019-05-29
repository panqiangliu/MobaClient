using System;
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

        //如果文件的路径为空
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

    //filePath 文件路径
    public static void CreateFile(string filePath)
    {
        string connect = string.Empty;
        DeletFile(filePath);
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
    }

    public static void CreateFile(string filePath, string content)
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

    //拷贝文件夹
    //sourceName 要拷贝的文件夹
    //destDirName 目标文件夹
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
            //进行文件夹名称的判断，判断结尾：目录分隔符
            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            //通过文件夹的名称获取所有的文件
            string[] files = Directory.GetFiles(destDirName);
            foreach (string file in files)
            {
                if (File.Exists(destDirName + Path.GetFileName(file)))
                    continue;
                //文件以.meta做结尾的 做版本控制用的文件
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))  //使用区域敏感排序规则、当前区域来比较字符串，同时忽略被比较字符串的大小写
                    continue;

                //复制 源数据   目标数据
                File.Copy(file, destDirName + Path.GetFileName(file), true);
                //设置属性，保持和原来的文件一致
                File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
            }

            //通过原来的文件夹获得其中的子文件夹
            string[] dirs = Directory.GetDirectories(sourceName);
            //通过回调将子文件夹全部拷到目标路径
            foreach (var item in dirs)
            {
                CopyDirectory(item, destDirName + Path.GetFileName(item));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //移动文件夹
    public static void MoveDirectory(String sourceDirName, string destDirName)
    {
        if (Directory.Exists(sourceDirName) && !Directory.Exists(destDirName))
        {
            Directory.Move(sourceDirName, destDirName);
        }
    }

    /// <summary>
    /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="extName">扩展名可以多个 例如 .mp3 .prefab</param>
    /// <returns>List<FileInfo></returns>
    public static List<FileInfo> GetFile(String path, String extName)
    {
        try
        {
            List<FileInfo> ls = new List<FileInfo>();
            //string[] test = Directory.GetDirectories(@"E:\GitHub\MobaClient\Asset\Assets\Resources\");
            string[] dir = Directory.GetDirectories(path); //文件夹列表
            DirectoryInfo fdir = new DirectoryInfo(path);
            FileInfo[] file = fdir.GetFiles();
            if (file.Length != 0 || dir.Length != 0)   //当前目录或者文件夹的目录不为空
            {
                foreach (var item in file)    //显示当前目录的所有的文件
                {
                    if (extName.ToLower().IndexOf(item.Extension.ToLower()) >= 0)
                    {
                        ls.Add(item);
                    }
                }
                foreach (string d in dir)
                {
                    ls.AddRange(GetFile(d, extName));
                }
            }

            return ls;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
