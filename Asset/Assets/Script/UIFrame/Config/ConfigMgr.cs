using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMgr : MonoSingleton<ConfigMgr> {

    /// <summary> 确认是否通过AB加载 </summary>
    public bool CheckIsABLoad() {
        return GameConfig.Instance.IsABLoad;
    }

    //网关服务器
    public string GetServerAddress() {
        return GameConfig.Instance.ServerAddress;
    }

    //网关服务器名称
    public string GetServerApplicationName()
    {
        return GameConfig.Instance.ServerApplicationName;
    }

    //战斗服务器
}
