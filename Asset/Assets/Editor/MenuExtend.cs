using UnityEditor;
using UnityEngine;

public class MenuExtend  {

    [MenuItem("UI工具/创建UIConfig")]
    public static void GetUIConfig()
    {
        //存储string文本
        StringBuilder uiEnum = new StringBuilder();//枚举成员
        StringBuilder uiDic = new StringBuilder();//字典添加的代码
        //读取的路径
        string path = Application.dataPath + "/Resources/UIPrefab/";
        //获取到所有.prefab的文件
		List<FileInfo> fileList = FileCom.GetFile(path, ".prefab");
	}
}