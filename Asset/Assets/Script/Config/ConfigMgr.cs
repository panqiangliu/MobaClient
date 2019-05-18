using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigMgr : MonoSingleton<ConfigMgr>
{
    public bool CheckIsABLoad()
    {
        return GameConfig.Instance.IsABLoad;
    }
}
