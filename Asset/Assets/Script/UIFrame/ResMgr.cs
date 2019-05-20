using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResMgr : SingletonBase<ResMgr> {

    //加载精灵
    //加载纹理
    //加载音效
    //加载特效

    /// <summary> 克隆单个预制件 </summary>
    public GameObject LoadPrefab(string path)
    {
        //确定是否通过AB加载
        if (ConfigMgr.Instance.CheckIsABLoad())
        {
            //TODO!
        }
        else
        {
            GameObject go = Resources.Load<GameObject>(path);
            if (go != null)
            {
                var obj = GameObject.Instantiate(go);
                return obj;
            }
        }
        return null;
    }

    
    /// <summary> 提供给克隆多个的业务 </summary>
    public List<GameObject> LoadPrefabs(string path, int count)
    {
        List<GameObject> goList = new List<GameObject>();
        if (ConfigMgr.Instance.CheckIsABLoad())
        {
            //TODO!
        }
        else
        {
            GameObject go = Resources.Load<GameObject>(path);
            if (go != null)
            {
                for (int i = 0; i < count; i++)
                {
                    var obj = GameObject.Instantiate(go);
                    goList.Add(obj);
                }
                return goList;
            }
        }
        return null;
    }
}
