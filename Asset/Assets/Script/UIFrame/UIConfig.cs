using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoMsg;

public enum UIPrefab
{
    
    Image,
    item,
    TestWindow,
    UserWindow,
}
public class UIConfig : Singleton<UIConfig >{

    private Dictionary<UIPrefab, string> uiDic = new Dictionary<UIPrefab, string>();
    public UIConfig() {

        
        uiDic.Add(UIPrefab.Image,@"UIPrefab\Image");
        uiDic.Add(UIPrefab.item,@"UIPrefab\item");
        uiDic.Add(UIPrefab.TestWindow,@"UIPrefab\TestWindow");
        uiDic.Add(UIPrefab.UserWindow,@"UIPrefab\User\UserWindow");

    }

    public string GetLoadPath(UIPrefab ui) {
        if (uiDic.ContainsKey(ui))
        {
            return uiDic[ui];
        }
        else
        {
            Debug.Log("加载路径获取失败");
            return "";
        }
    }
}