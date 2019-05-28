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

