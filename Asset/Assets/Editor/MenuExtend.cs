using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MenuExtend
{

    [MenuItem("UI工具/创建UIConfig")]
    public static void GetUIConfig()
    {
        //存储string文本
        StringBuilder uiEnum = new StringBuilder();//枚举成员
        StringBuilder uiDic = new StringBuilder();//字典添加的代码
        //读取的路径
        string path = (Application.dataPath + "/Resources/UIPrefab/").Replace("/", "\\");
        //获取到所有.prefab的文件
        List<FileInfo> fileList = FileCom.GetFile(path, ".prefab");
        for (int i = 0; i < fileList.Count; i++)
        {
            //把文件路径里的/全部换成\
            string res = (Application.dataPath + "/Resources/").Replace("/", "\\");
            uiEnum.Append("\n");   //添加换行操作
            uiEnum.Append("  " + fileList[i].Name.Replace(".prefab", "") + ",");

            //通过枚举访问 去点 .prefabde ui预制件名称
            string key = "UIPrefab." + fileList[i].Name.Replace(".prefab", "");
            //加载短路径
            string value = fileList[i].FullName.Replace(res, "").Replace(".prefab", "");
            //给字典添加模板内容
            string dicTempLate = "    " + "uiDic.Add(key,@value);";
            uiDic.Append("\n");
            uiDic.Append(dicTempLate.Replace("key", key).Replace("value", "\"" + value + "\""));
        }

        Debug.Log(uiEnum);
        Debug.Log(uiDic);

        //读取到模板 替换模板内标记的内容 替换为我们上面uiEnum uiDic
        string configTempLatePath = Application.dataPath + "/Editor Default Resources/uiconfig.txt";
        var allText = FileCom.GetFileText(configTempLatePath);
        allText = allText.Replace("//枚举成员", uiEnum.ToString()).Replace("//配置初始化", uiDic.ToString());
        Debug.Log(allText);
        //写入的路径
        var destFile = Application.dataPath + "/Script/UIFrame/UIConfig.cs";
        //创建和写入代码
        FileCom.CreateFile(destFile, allText);
        AssetDatabase.Refresh();
    }
}