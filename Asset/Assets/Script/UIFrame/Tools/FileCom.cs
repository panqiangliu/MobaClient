using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using UnityEngine;

public class FileCom
{
    /// <summary> 检查文件是否存在 </summary>
    public static bool CheckFileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    /// <summary> 获取文件内容 </summary>
    public static string GetFileText(string filePath)
    {
        string content = string.Empty;

        //判断文件是否存在
        if (!File.Exists(filePath))
        {
            return content;
        }

        //通过读取流 读取文本的所有内容
        using (StreamReader sr = File.OpenText(filePath))
        {
            content = sr.ReadToEnd();
        }
        return content;
    }

    /// <summary> 创建文件 </summary>
    public static void CreateFile(string filePath)
    {
        //string content = string.Empty;
        DeleteFile(filePath);
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
            //ClearFileText(filePath);
        }
    }

    /// <summary> 追加文件内容 </summary>
    public static void AppendFileText(string filePath, string contents)
    {
        //string content = string.Empty;

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

    /// <summary> 清空文件内容 </summary>
    public static void ClearFileText(string filePath)
    {
        //string content = string.Empty;

        //if (!File.Exists(localPath + filePath))
        //{

        //}
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
    }

    /// <summary> 创建文件 </summary>
    public static void CreateFile(string filePath, string content)
    {
        DeleteFile(filePath);

        //通过文件流创建与写入内容
        using (FileStream fs = File.Create(filePath))
        {
            using (StreamWriter sw = new StreamWriter(fs,Encoding.UTF8))
            {
                sw.Write(content.ToString());
            }
        }
    }

    /// <summary> 删除文件 </summary>
    public static void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    /// <summary> 拷贝文件夹 </summary>
    public static void CopyDirectory(string sourceDirName, string destDirName)
    {
        try
        {
            //如果目标文件夹不存在
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));

            }
            //进行文件夹名称的判断 结尾是不是:目录分隔符
            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            //通过文件夹的名称 获取所有的文件
            string[] files = Directory.GetFiles(sourceDirName);
            //获取到每个文件
            foreach (string file in files)
            {
                //判断 目标路径+拷贝的文件 
                if (File.Exists(destDirName + Path.GetFileName(file)))
                    continue;

                //文件以.meta结尾 做版本控制用的文件
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                //复制 源数据 目标数据 true表示是否重写里面的内容
                File.Copy(file, destDirName + Path.GetFileName(file), true);
                //设置属性 与原来拷贝的文件保持一致
                File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
            }
            //通过原来的文件夹  获取到它下面所有的子文件夹
            string[] dirs = Directory.GetDirectories(sourceDirName);
            //通过回调 将子文件全部拷贝到目标路径去
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary> 移动文件夹 </summary>
    public static void MoveDirectory(string sourceDirName, string destDirName)
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
    public static List<FileInfo> GetFile(string path, string extName)
    {
        try
        {
            List<FileInfo> lst = new List<FileInfo>();
            string[] dir = Directory.GetDirectories(path); //文件夹列表   
            DirectoryInfo fdir = new DirectoryInfo(path);
            FileInfo[] file = fdir.GetFiles();
            //FileInfo[] file = Directory.GetFiles(path); //文件列表   
            if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
            {
                foreach (FileInfo f in file) //显示当前目录所有文件   
                {
                    if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                    {
                        lst.Add(f);
                    }
                }
                //获取子文件夹下面 扩展名对应的文件
                foreach (string d in dir)
                {
                    lst.AddRange(GetFile(d, extName));//递归   
                }
            }
            return lst;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// </summary>
    /// <param name="path">Excel服务器路径</param>
    /// <param name="tableName">Excel表名称</param>
    /// <returns></returns>
    public static DataSet ExcelConnection(string path)//, string tableName)
    {
        
        string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + path + ";Extended Properties='Excel 12.0; HDR=YES; IMEX=1'"; //此連接可以操作.xls與.xlsx文件
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等    
        DataTable dtSheetName = conn.GetOleDbSchemaTable(
        OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });

        //包含excel中表名的字符串数组  
        string[] strTableNames = new string[dtSheetName.Rows.Count];
        for (int k = 0; k < dtSheetName.Rows.Count; k++)
        {
            strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
        }
        for (int i = 0; i < strTableNames.Length; i++)
        {
            OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + strTableNames[i].ToString() + "]", conn); //("select * from [Sheet1$]", conn);
            odda.Fill(ds, strTableNames[i]);// "[" + tableName + "$]");
        }

        conn.Close();
        return ds;
    }


}
