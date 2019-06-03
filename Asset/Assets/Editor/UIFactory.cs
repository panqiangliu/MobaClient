using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using System.Data;


public class UIFactory : MonoBehaviour
{
    //模板
    static string UIWindoeModule = Application.dataPath+"/Editor Default Resources/uiwindow.txt";
    static string UIControl = Application.dataPath + "/Editor Default Resources/uicontrol.txt";

    //ui预制件字典 key:名称 value:加载路径
    static Dictionary<string, string> UIPre = new Dictionary<string, string>();
     //gameobject字典 key:物体  value:物体中所有的按钮集合
    static Dictionary<GameObject, Button[]> GoList = new Dictionary<GameObject, Button[]>();

    //ui窗体资源路径
    static string ResPath = Application.dataPath+"/Resources/UIPrefab/";

    [MenuItem("UI工具/创建UIWindow")]
    public static void CreatUIModule()
    {
        var UIWindowText = FileCom.GetFileText(UIWindoeModule);   //获得里边的文本内容
        List<FileInfo> fileList = FileCom.GetFile(ResPath,".prefab");

        //UIPrefab初始化：获取物体与加载路径
        for (int i = 0; i < fileList.Count; i++)
        {
            string res = (Application.dataPath+"/Resources/").Replace("/","\\");
            string key =fileList[i].Name.Replace(".prefab","");
            string value =fileList[i].FullName.Replace(res,"").Replace(".prefab","");
            UIPre.Add(key,value);
        }

        //GoList初始化：获取物体与物体上的所有按钮
        foreach (var key in UIPre.Keys)
        {
            var go = Resources.Load(UIPre[key]) as GameObject;
            Debug.Log("物体："+go.name);   //预制体的名称
            Button[] btns=go.GetComponents<Button>();
            GoList.Add(go,btns);
        }

        //创建每个物体对应的Window.cs  文件
        foreach (var gkey in GoList.Keys)
        {
            string alltext;
            var key = gkey;

            //承载变量声明的代码
            StringBuilder variable = new StringBuilder();
            //承载变量初始化的代码
            StringBuilder variableInit = new StringBuilder();

            //动态物体字典  dynamic 键：物体名称；值：加载路径
            Dictionary<string,string> dynDic = new Dictionary<string, string>();
            //动态物体初始化字典 键：物体名称 值：初始化的代码
            Dictionary<string,string> dynInitDic = new Dictionary<string, string>();

            //所有的子物体包括隐藏的
            Transform[] allChild = key.GetComponentsInChildren<Transform>(true);

            //对于动态物体的逻辑“_dyn”
            for (int i = 0; i < allChild.Length; i++)
            {
                if(allChild[i].name.Contains("_dyn"))
                {
                    string loadPath = "";
                    var c =allChild[i];      //当前的子物体
                    while(c.parent != null)
                    {
                        loadPath="/"+c.name+loadPath;
                        c=c.parent;
                    }

                    //加入动态物体管理的数据结构 key: 物体名称  value:加载路径
                    if(dynDic.ContainsKey(allChild[i].name))
                    {
                        Debug.LogError("动态五日名称重复，请手动添加业务前缀");
                        Debug.LogError("已有路径是："+dynDic[allChild[i].name]);
                        Debug.LogError("c冲突路径是："+loadPath.Remove(0,1));
                        dynDic.Add(allChild[i].name,loadPath.Remove(0,1));
                    }
                }
            }
        }
    }



    [MenuItem("UI工具/创建UIConfig")]
    public static void GetUIConfig()
    {
        StringBuilder uiEnum = new StringBuilder();    //枚举成员
        StringBuilder uiDic = new StringBuilder();     //字典添加的代码

        List<FileInfo> fileList = FileCom.GetFile(ResPath,".prefab");
        for (int i = 0; i < fileList.Count; i++)
        {
            //把所有的“/”换成“\”
            string res =(Application.dataPath+"/Resources/").Replace('/','\\');
            uiEnum.Append('\n');
            uiEnum.Append(" "+fileList[i].Name.Replace(".prefab","")+",");
            
            //通过枚举访问 去掉 .prefab 的UI预制件名称
            string key = "UIPrefab."+fileList[i].Name.Replace(".prefab","");
            //加载短路径
            string value =fileList[i].FullName.Replace(".prefab","");
            //给字典添加内容的模板
            string dicTemplate = "      "+"uiDic.Add(key,@value)";
            uiDic.Append("\n");
            uiDic.Append(dicTemplate.Replace("key",key).Replace("value","\""+value+"\""));
        }
        Debug.Log(uiEnum);
        Debug.Log(uiDic);

        //读取模板，替换模板内容 替换为uiEnum 和uiDic
        string configTemplatePath = Application.dataPath+"Editor Default Resources/uiconfig.txt";
        var allText = FileCom.GetFileText(configTemplatePath);
        allText = allText.Replace("//枚举成员", uiEnum.ToString()).Replace("//配置初始化", uiDic.ToString());
        Debug.Log(allText);
        //写入的目标路径
        var destFile = Application.dataPath + "/Script/UIFrame/UIConfig.cs";
        //创建和写入代码
        FileCom.CreateFile(destFile, allText);
        //刷新编辑器
        AssetDatabase.Refresh();
    }


    [MenuItem("GameObject/添加Window后缀",priority=49)]
    public static void UpdateNameAddWindow()
    {
        Transform[] transforms=Selection.transforms;
        if (transforms.Length > 0)
        {
            for(int i = 0; i < transforms.Length; i++)
            {
                if (transforms[i].name.Contains("Window"))
                {
                    Debug.Log(transforms[i].name+"：名字已经包含Window");
                }
                else
                {
                    transforms[i].gameObject.name=transforms[i].name+"Window";
                }
            }
        }
    }

    [MenuItem("GameObject/添加_dyn(动态)后缀",priority = 49)]
    public static void UpdateNameAddDYN()
    {
        Transform[] objs = Selection.transforms;
        if(objs.Length > 0)
        {
            for (int i = 0;i < objs.Length; i++)
            {
                if(objs[i].name.Contains("_dyn"))
                {
                    Debug.Log(objs[i].name+":已经存在_dyn后缀");
                }
                else
                {
                    objs[i].transform.name= objs[i].name+"_dyn";
                }
            }
        }
    }



    //将图片改为Sprite 2D And UI
    [MenuItem("Assets/图片改成2D AND UI", priority = 49)]
    public static void SetTextureType()
    {
        var t = Selection.objects;
        if (t.Length > 0)
        {
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] is Texture2D)
                {
                    string path = AssetDatabase.GetAssetPath(t[i]);
                    TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter;
                    ti.mipmapEnabled = false;
                    ti.spriteBorder = new Vector2(1920, 1280);         //图片的包围盒
                    ti.textureType = TextureImporterType.Sprite;     //将图片设置为2D and UI
                    ti.SaveAndReimport();
                }
            }
        }
    }

    [MenuItem("Assets/创建物体:图片", priority = 49)]
    public static void CreatImg()
    {
        UnityEngine.Object[] objs = Selection.objects;
        Transform parentTran = GameObject.Find("UIRoot").transform;
        if (objs.Length > 0)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] is Texture2D)
                {
                    string path = AssetDatabase.GetAssetPath(objs[i]);
                    TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter;
                    if (ti.textureType == TextureImporterType.Sprite)
                    {
                        GameObject go = new GameObject(objs[i].name, new Type[] { typeof(Image) });
                        go.transform.SetParent(parentTran);
                        var image = go.GetComponent<Image>();
                        image.type = Image.Type.Simple;
                        UnityEngine.Object newImg = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                        Undo.RecordObject(image, "Change Image");
                        image.sprite = newImg as Sprite;
                        image.SetNativeSize();   //这是为原来的尺寸的大小

                    }
                }
            }
        }
    }

    
    [MenuItem("Assets/创建物体:图片和按钮", priority = 49)]
    public static void CreateImageHaveBtn()
    {
        UnityEngine.Object[] objs= Selection.objects;
        Transform parent = GameObject.Find("UIRoot").transform;
        if (objs.Length>0)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if(objs[i] is Texture2D)
                {
                    string path = AssetDatabase.GetAssetPath(objs[i]);
                    TextureImporter ti=TextureImporter.GetAtPath(path) as TextureImporter;
                    if (ti.textureType==TextureImporterType.Sprite)
                    {
                        GameObject go =new GameObject(objs[i].name,new Type[] {typeof(Image),typeof(Button)});
                        go.transform.SetParent(parent,false);
                        var img=go.GetComponent<Image>();
                        img.type=Image.Type.Simple;
                        UnityEngine.Object image=AssetDatabase.LoadAssetAtPath(path,typeof(Sprite));
                        img.sprite=image as Sprite;
                        Undo.RecordObject(img,"Change Image");
                        img.SetNativeSize();
                    }
                }
            }
        }
    }


}

