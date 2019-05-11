using System;
using System.Collections;
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
        StringBuilder uiEnum = new StringBuilder();   //枚举成员
        StringBuilder uiDic =new StringBuilder();     //字典添加的代码
        //获取所有的 .prefab 的文件
        String path = Application.dataPath+"Resources/UIPrefab/";
        List<FileInfo> fileList=FileCom.GetFile(path,".prefab");
        for (int i = 0; i < fileList.Count; i++)     
        {
            //把路径里的 / 替换成 \
            String res =(Application.dataPath+"/Resources/").Replace('/','\\');
            uiEnum.Append('\n');     //添加换行符
             uiEnum.Append("  "+fileList[i].Name.Replace(".prefab","")+",");

            //通过访问枚举， 去掉 .prefab 的 UI预制件名称
            String key = "UIPrefab."+fileList[i].Name.Replace(".prefab","");
            //加载的短路径
            String value =fileList[i].FullName.Replace(res,"").Replace(".prefab","");
            //给字典添加内容的模板
            
        }
    }
}
