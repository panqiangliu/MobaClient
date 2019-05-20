using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPrefab
{   
  a,
  b,
  C,
}
public class UIConfig : SingletonBase<UIConfig>{

    private Dictionary<UIPrefab, string> uiDic = new Dictionary<UIPrefab, string>();
    public UIConfig() {        
    uiDic.Add(UIPrefab.a,@"UIPrefab\a");
    uiDic.Add(UIPrefab.b,@"UIPrefab\b");
    uiDic.Add(UIPrefab.C,@"UIPrefab\New Folder\C");  
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