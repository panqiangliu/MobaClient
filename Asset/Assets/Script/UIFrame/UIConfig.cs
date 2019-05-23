using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPrefab
{
    TestA,
    TestB,
}
public class UIConfig : SingletonBase<UIConfig>
{

    private Dictionary<UIPrefab, string> uiDic = new Dictionary<UIPrefab, string>();
    public UIConfig()
    {
        uiDic.Add(UIPrefab.TestA, @"UIPrefab\TestA");
        uiDic.Add(UIPrefab.TestB, @"UIPrefab\TestB");
    }

    public string GetLoadPath(UIPrefab ui)
    {
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